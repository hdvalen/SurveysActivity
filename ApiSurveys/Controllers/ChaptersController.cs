using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;


namespace ApiSurveys.Controllers;

public class ChaptersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChaptersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ChaptersDto>>>
    Get()
    {
        var chapters = await _unitOfWork.Chapter.GetAllAsync();
        return _mapper.Map<List<ChaptersDto>>(chapters);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChaptersDto>> Get(int id)
    {
        var chapter = await _unitOfWork.Chapter.GetByIdAsync(id);
        if (chapter == null)
            return NotFound($"Chapter with id {id} was not found");
        return _mapper.Map<ChaptersDto>(chapter);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Chapter>> Post(ChaptersDto chapterDto)
    {
        var Chapter = _mapper.Map<Chapter>(chapterDto);
        _unitOfWork.Chapter.Add(Chapter);
        await _unitOfWork.SaveAsync();
        if (chapterDto == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = chapterDto.Id }, chapterDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] ChaptersDto chapterDto)
    {
        if (chapterDto == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        var chapter = _mapper.Map<Chapter>(chapterDto);
        _unitOfWork.Chapter.Update(chapter);
        await _unitOfWork.SaveAsync();

        return Ok(chapterDto);
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