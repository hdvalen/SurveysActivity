using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Controllers;

public class SummaryOptionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SummaryOptionsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SumaryOptionDto>>> Get()
    {
        var summaryOptions = await _unitOfWork.SummaryOptions.GetAllAsync();
        return Ok(_mapper.Map<List<SumaryOptionDto>>(summaryOptions));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SumaryOptionDto>> Get(int id)
    {
        var summaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
        if (summaryOption == null)
            return NotFound($"SummaryOption with id {id} was not found");
        return Ok(_mapper.Map<SumaryOptionDto>(summaryOption));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SumaryOptionDto>> Post(SumaryOptionDto summaryOptionDto)
    {
        if (summaryOptionDto == null)
            return BadRequest();

        var summaryOption = _mapper.Map<SummaryOptions>(summaryOptionDto);
        _unitOfWork.SummaryOptions.Add(summaryOption);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Get), new { id = summaryOption.Id }, _mapper.Map<SumaryOptionDto>(summaryOption));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] SumaryOptionDto summaryOptionDto)
    {
        if (summaryOptionDto == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");
            
        var existingSummaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
        if (existingSummaryOption == null)
            return NotFound($"No se encontró la opción de resumen con id {id}");

        _mapper.Map(summaryOptionDto, existingSummaryOption);

        _unitOfWork.SummaryOptions.Update(existingSummaryOption);
        await _unitOfWork.SaveAsync();

        return Ok(_mapper.Map<SumaryOptionDto>(existingSummaryOption));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var summaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
        if (summaryOption == null)
            return NotFound();

        _unitOfWork.SummaryOptions.Remove(summaryOption);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}