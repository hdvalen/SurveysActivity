using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Controllers;

public class QuestionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<QuestionsDto>>> Get()
    {
        var questions = await _unitOfWork.Question.GetAllAsync();
        return Ok(_mapper.Map<List<QuestionsDto>>(questions));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuestionsDto>> Get(int id)
    {
        var question = await _unitOfWork.Question.GetByIdAsync(id);
        if (question == null)
            return NotFound($"Question with id {id} was not found");
        return Ok(_mapper.Map<QuestionsDto>(question));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<QuestionsDto>> Post(QuestionsDto questionDto)
    {
        if (questionDto == null)
            return BadRequest();

        var question = _mapper.Map<Question>(questionDto);
        _unitOfWork.Question.Add(question);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(Get), new { id = question.Id }, _mapper.Map<QuestionsDto>(question));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] QuestionsDto questionDto)
    {
        if (questionDto == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != questionDto.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingQuestion = await _unitOfWork.Question.GetByIdAsync(id);
        if (existingQuestion == null)
            return NotFound($"No se encontró la pregunta con id {id}");

        // Actualiza las propiedades necesarias
        _mapper.Map(questionDto, existingQuestion);
        existingQuestion.Updated_At = DateTime.UtcNow;

        _unitOfWork.Question.Update(existingQuestion);
        await _unitOfWork.SaveAsync();

        return Ok(_mapper.Map<QuestionsDto>(existingQuestion));
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