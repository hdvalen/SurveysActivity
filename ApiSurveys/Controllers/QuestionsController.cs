using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace ApiSurveys.Controllers;

public class QuestionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Question>>> Get()
    {
        var questions = await _unitOfWork.Question.GetAllAsync();
        return Ok(questions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Question>> Get(int id)
    {
        var question = await _unitOfWork.Question.GetByIdAsync(id);
        if (question == null)
            return NotFound($"Question with id {id} was not found");
        return Ok(question);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Question>> Post(Question question)
    {
        _unitOfWork.Question.Add(question);
        await _unitOfWork.SaveAsync();
        if (question == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = question.Id }, question);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] Question question)
    {
        if (question == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != question.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingQuestion = await
            _unitOfWork.Question.GetByIdAsync(id);
        if (existingQuestion == null)
            return NotFound($"No se encontró la pregunta con id {id}");

        existingQuestion.Question_Text = question.Question_Text;
        existingQuestion.ChapterId = question.ChapterId;
        existingQuestion.Updated_At = DateTime.UtcNow;
        existingQuestion.Created_At = DateTime.UtcNow;

        _unitOfWork.Question.Update(existingQuestion);
        await _unitOfWork.SaveAsync();

        return Ok(existingQuestion);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var question = await _unitOfWork.Question.GetByIdAsync(id);
        if (question == null)
            return NotFound($"Question with id {id} was not found");

        _unitOfWork.Question.Remove(question);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}