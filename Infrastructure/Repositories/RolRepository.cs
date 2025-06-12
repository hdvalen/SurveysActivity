using Application.Interfaces;
using Domain.entities; 
using Infrastructure.Data;


namespace Infrastructure.Repositories; 
public class RolRepository : GenericRepository<Rol>, IRolRepository { 
    private readonly SurveyContext _context; 
    public RolRepository(SurveyContext context) : base(context) {}

    public void Attach(Rol rol)
    {
        if (rol == null) throw new ArgumentNullException(nameof(rol));
        _context.Rols.Attach(rol);
    }
} 