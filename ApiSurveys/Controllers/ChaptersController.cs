using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;


namespace ApiSurveys.Controllers;

public class ChaptersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public ChaptersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Chapter>>>
    Get()
    {
        var chapters = await _unitOfWork.Chapter.GetAllAsync();
        return Ok(chapters);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Chapter>> Get(int id)
    {
        var chapter = await _unitOfWork.Chapter.GetByIdAsync(id);
        if (chapter == null)
            return NotFound($"Chapter with id {id} was not found");
        return Ok(chapter);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Chapter>> Post(Chapter chapter)
    {
        _unitOfWork.Chapter.Add(chapter);
        await _unitOfWork.SaveAsync();
        if (chapter == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = chapter.Id }, chapter);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] Chapter chapter)
    {
        if (chapter == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != chapter.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingChapter = await
            _unitOfWork.Chapter.GetByIdAsync(id);
        if (existingChapter == null)
            return NotFound($"No se encontró chapter con id {id}");

        existingChapter.Survey_Id = chapter.Survey_Id;
        existingChapter.Created_At = DateTime.UtcNow;
        existingChapter.Updated_At = DateTime.UtcNow;

        _unitOfWork.Chapter.Update(existingChapter);
        await _unitOfWork.SaveAsync();

        return Ok(existingChapter);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var chapter = await _unitOfWork.Chapter.GetByIdAsync(id);
        if (chapter == null)
            return NotFound();

        _unitOfWork.Chapter.Remove(chapter);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}