using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiSurveys.Helpers;
using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ApiSurveys.Services;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Member> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
        IPasswordHasher<Member> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var usuario = new Member
        {
            Id = registerDto.Id,
            Name = registerDto.Name,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Username = registerDto.Username
        };

        usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password);

        var usuarioExiste = _unitOfWork.Member
                                    .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                                    .FirstOrDefault();

        if (usuarioExiste == null)
        {
                var rolPredeterminado = _unitOfWork.Rol
                            .Find(u => u.Name == UserAuthorization.rol_predeterminado.ToString())
                            .First();
            try
            {
                //var rolAsociar = new Rol { Id = rolPredeterminado.Id };
                // usuario.Rols.Add(rolPredeterminado);
                // _unitOfWork.Rols.Attach(rolAsociar);
                //usuario.Rols.Add(rolAsociar);
                var relacion = new MemberRols
                {
                    MemberId = usuario.Id,
                    RolId = rolPredeterminado.Id
                };
                _unitOfWork.Member.Add(usuario);
                _unitOfWork.MemberRols.Add(relacion);
                await _unitOfWork.SaveAsync();

                return $"El usuario  {registerDto.Username } ha sido registrado exitosamente";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"El usuario con {registerDto.Username } ya se encuentra registrado.";
        }
    }
    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto dataUserDto = new DataUserDto();
        var usuario = await _unitOfWork.Member
                    .GetByUsernameAsync(model.Username);

        if (usuario == null)
        {
            dataUserDto.EstaAutenticado = false;
            dataUserDto.Mensaje = $"No existe ningún usuario con el username {model.Username}.";
            return dataUserDto;
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            dataUserDto.EstaAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            dataUserDto.Email = usuario.Email;
            dataUserDto.UserName = usuario.Username;
            dataUserDto.Rols = usuario.MemberRols
                                            .Select(ur => ur.Rol.Name)
                                            .ToList();
            if (usuario.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = usuario.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                dataUserDto.RefreshToken = activeRefreshToken.Token;
                dataUserDto.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                dataUserDto.RefreshToken = refreshToken.Token;
                dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                usuario.RefreshTokens.Add(refreshToken);
                _unitOfWork.Member.Update(usuario);
                await _unitOfWork.SaveAsync();
            }

            return dataUserDto;
        }
        dataUserDto.EstaAutenticado = false;
        dataUserDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
        return dataUserDto;
    }
    public async Task<string> AddRoleAsync(AddRoleDto model)
    {

        var usuario = await _unitOfWork.Member
                    .GetByUsernameAsync(model.Username);

        if (usuario == null)
        {
            return $"No existe algún usuario registrado con la cuenta {model.Username}.";
        }


        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {


            var rolExiste = _unitOfWork.Rol
                                        .Find(u => u.Name.ToLower() == model.Role.ToLower())
                                        .FirstOrDefault();

            if (rolExiste != null)
            {
                var usuarioTieneRol = usuario.MemberRols
                                            .Any(u => u.RolId == rolExiste.Id);

                if (usuarioTieneRol == false)
                {
                    var relacion = new MemberRols
                    {
                        MemberId = usuario.Id,
                        RolId = rolExiste.Id
                    };
                    _unitOfWork.MemberRols.Add(relacion);
                    await _unitOfWork.SaveAsync();
                }

                return $"Rol {model.Role} agregado a la cuenta {model.Username} de forma exitosa.";
            }

            return $"Rol {model.Role} no encontrado.";
        }
        return $"Credenciales incorrectas para el usuario {usuario.Username}.";
    }
    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }
   public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var datosUsuarioDto = new DataUserDto();

        var usuario = await _unitOfWork.Member
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"El token no pertenece a ningún usuario.";
            return datosUsuarioDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"El token no está activo.";
            return datosUsuarioDto;
        }
        //Revocamos el Refresh Token actual y
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generamos un nuevo Refresh Token y lo guardamos en la Base de Datos
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Member.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generamos un nuevo Json Web Token 😊
        datosUsuarioDto.EstaAutenticado = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        datosUsuarioDto.Email = usuario.Email;
        datosUsuarioDto.UserName = usuario.Username;
        datosUsuarioDto.Rols = usuario.MemberRols
                                            .Select(ur => ur.Rol.Name)
                                            .ToList();
        datosUsuarioDto.RefreshToken = newRefreshToken.Token;
        datosUsuarioDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return datosUsuarioDto;
    }
    private JwtSecurityToken CreateJwtToken(Member usuario)
    {
        var roles = usuario.MemberRols
            .Select(mr => mr.Rol)
            .ToList();
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Name));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
  
}