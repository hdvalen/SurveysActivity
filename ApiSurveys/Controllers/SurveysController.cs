
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace ApiSurveys.Controllers;

public class SurveysController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public SurveysController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<Survey>>>
    Get()
    {
        var survey = await
        _unitOfWork.Survey.GetAllAsync();
        return Ok(survey);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Get(int id)
    {
        var survey = await
        _unitOfWork.Survey.GetByIdAsync(id);
        if (survey == null)
        {
            return NotFound($"Categories Catalog with id {id} was not found");
        }
        return Ok(survey);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Survey>> Post(Survey survey)
    {
        _unitOfWork.Survey.Add(survey);
        await _unitOfWork.SaveAsync();
        if (survey == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = survey.Id }, survey);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] Survey survey)
    {
        if (survey == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        if (id != survey.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingSurvey = await
            _unitOfWork.Survey.GetByIdAsync(id);
        if (existingSurvey == null)
            return NotFound($"No se encontro Survey con el id {id}.");
        existingSurvey.Name = survey.Name;
        _unitOfWork.Survey.Update(existingSurvey);

        await _unitOfWork.SaveAsync();

        return Ok(existingSurvey);
    }
    

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var survey = await _unitOfWork.Survey.GetByIdAsync(id);
        if (survey == null)
            return NotFound();

        _unitOfWork.Survey.Remove(survey);
        await _unitOfWork.SaveAsync();

        return NoContent();
}

}