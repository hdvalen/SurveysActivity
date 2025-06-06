using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;


namespace ApiSurveys.Controllers;

public class OptionQuestionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public OptionQuestionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OptionQuestion>>> Get()
    {
        var optionQuestions = await _unitOfWork.OptionQuestion.GetAllAsync();
        return Ok(optionQuestions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OptionQuestion>> Get(int id)
    {
        var optionQuestion = await _unitOfWork.OptionQuestion.GetByIdAsync(id);
        if (optionQuestion == null)
            return NotFound($"Option Question with id {id} was not found");
        return Ok(optionQuestion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<OptionQuestion>> Post(OptionQuestion optionQuestion)
    {
        _unitOfWork.OptionQuestion.Add(optionQuestion);
        await _unitOfWork.SaveAsync();
        if (optionQuestion == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = optionQuestion.Id }, optionQuestion);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] OptionQuestion optionQuestion)
    {
        if (optionQuestion == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != optionQuestion.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingOptionQuestion = await
            _unitOfWork.OptionQuestion.GetByIdAsync(id);
        if (existingOptionQuestion == null)
            return NotFound($"No se encontró la pregunta de opción con id {id}");

        existingOptionQuestion.Option_Id = optionQuestion.Option_Id;
        existingOptionQuestion.SubQuestion_Id = optionQuestion.SubQuestion_Id;
        existingOptionQuestion.OptionCatalog_Id = optionQuestion.OptionCatalog_Id;
        existingOptionQuestion.OptionQuestion_Id = optionQuestion.OptionQuestion_Id;
        existingOptionQuestion.Updated_At = DateTime.UtcNow;
        existingOptionQuestion.Created_At = DateTime.UtcNow;

        _unitOfWork.OptionQuestion.Update(existingOptionQuestion);
        await _unitOfWork.SaveAsync();

        return Ok(existingOptionQuestion);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var optionQuestion = await _unitOfWork.OptionQuestion.GetByIdAsync(id);
        if (optionQuestion == null)
            return NotFound();

        _unitOfWork.OptionQuestion.Remove(optionQuestion);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}