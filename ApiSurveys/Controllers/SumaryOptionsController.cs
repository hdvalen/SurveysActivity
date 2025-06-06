using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace ApiSurveys.Controllers;

public class SummaryOptionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public SummaryOptionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SummaryOptions>>> Get()
    {
        var summaryOptions = await _unitOfWork.SummaryOptions.GetAllAsync();
        return Ok(summaryOptions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SummaryOptions>> Get(int id)
    {
        var summaryOption = await _unitOfWork.SummaryOptions.GetByIdAsync(id);
        if (summaryOption == null)
            return NotFound($"SummaryOption with id {id} was not found");
        return Ok(summaryOption);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SummaryOptions>> Post(SummaryOptions summaryOption)
    {
        _unitOfWork.SummaryOptions.Add(summaryOption);
        await _unitOfWork.SaveAsync();
        if (summaryOption == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = summaryOption.Id }, summaryOption);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] SummaryOptions summaryOption)
    {
        if (summaryOption == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != summaryOption.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingSummaryOption = await
            _unitOfWork.SummaryOptions.GetByIdAsync(id);
        if (existingSummaryOption == null)
            return NotFound($"No se encontró la opción de resumen con id {id}");

        existingSummaryOption.Survey_Id = summaryOption.Survey_Id;
        existingSummaryOption.Question_Id = summaryOption.Question_Id;
        existingSummaryOption.Code_Number = summaryOption.Code_Number;

        _unitOfWork.SummaryOptions.Update(existingSummaryOption);
        await _unitOfWork.SaveAsync();

        return Ok(existingSummaryOption);
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