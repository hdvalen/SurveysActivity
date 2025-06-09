
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Controllers;

public class OptionsResponseController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OptionsResponseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<OptionsResponseDto>>>
    Get()
    {
        var optionResponse = await
        _unitOfWork.OptionsResponse.GetAllAsync();
        return _mapper.Map<List<OptionsResponseDto>>(optionResponse);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OptionsResponseDto>> Get(int id)
    {
        var optionResponse = await
        _unitOfWork.OptionsResponse.GetByIdAsync(id);
        if (optionResponse == null)
        {
            return NotFound($"Option Response with id {id} was not found");
        }
        return _mapper.Map<OptionsResponseDto>(optionResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OptionsResponse>> Post(OptionsResponseDto optionsResponseDto)
    {
        var OptionsResponse = _mapper.Map<OptionsResponse>(optionsResponseDto);
        _unitOfWork.OptionsResponse.Add(OptionsResponse);
        await _unitOfWork.SaveAsync();
        if (optionsResponseDto == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = optionsResponseDto.Id }, optionsResponseDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] OptionsResponseDto optionsResponseDto)
    {
        if (optionsResponseDto == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        var optionsResponse= _mapper.Map<OptionsResponse>(optionsResponseDto);
        _unitOfWork.OptionsResponse.Add(optionsResponse);
        await _unitOfWork.SaveAsync();

        return Ok(optionsResponseDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var optionsResponse = await _unitOfWork.OptionsResponse.GetByIdAsync(id);
        if (optionsResponse == null)
            return NotFound();

        _unitOfWork.OptionsResponse.Remove(optionsResponse);
        await _unitOfWork.SaveAsync();

        return NoContent();
}

}