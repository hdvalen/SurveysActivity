using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace ApiSurveys.Controllers;

public class SubQuestionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public SubQuestionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SubQuestion>>>
    Get()
    {
        var subQuestions = await _unitOfWork.SubQuestion.GetAllAsync();
        return Ok(subQuestions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubQuestion>> Get(int id)
    {
        var subQuestion = await _unitOfWork.SubQuestion.GetByIdAsync(id);
        if (subQuestion == null)
            return NotFound($"SubQuestion with id {id} was not found");
        return Ok(subQuestion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubQuestion>> Post(SubQuestion subQuestion)
    {
        _unitOfWork.SubQuestion.Add(subQuestion);
        await _unitOfWork.SaveAsync();
        if (subQuestion == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = subQuestion.Id }, subQuestion);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] SubQuestion subQuestion)
    {
        if (subQuestion == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != subQuestion.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingSubQuestion = await
            _unitOfWork.SubQuestion.GetByIdAsync(id);
        if (existingSubQuestion == null)
            return NotFound($"SubQuestion with id {id} was not found");

        existingSubQuestion.SubQuestion_Id = subQuestion.SubQuestion_Id;
        existingSubQuestion.Updated_At = DateTime.UtcNow;
        existingSubQuestion.Created_At = DateTime.UtcNow;

        _unitOfWork.SubQuestion.Update(existingSubQuestion);
        await _unitOfWork.SaveAsync();

        return Ok(existingSubQuestion);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var subQuestion = await _unitOfWork.SubQuestion.GetByIdAsync(id);
        if (subQuestion == null)
            return NotFound();

        _unitOfWork.SubQuestion.Remove(subQuestion);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}