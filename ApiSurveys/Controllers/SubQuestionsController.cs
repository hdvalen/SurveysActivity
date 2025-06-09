using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Controllers;

public class SubQuestionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubQuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SubQuestionDto>>> Get()
    {
        var subQuestions = await _unitOfWork.SubQuestion.GetAllAsync();
        return Ok(_mapper.Map<List<SubQuestionDto>>(subQuestions));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubQuestionDto>> Get(int id)
    {
        var subQuestion = await _unitOfWork.SubQuestion.GetByIdAsync(id);
        if (subQuestion == null)
            return NotFound($"SubQuestion with id {id} was not found");
        return Ok(_mapper.Map<SubQuestionDto>(subQuestion));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubQuestionDto>> Post(SubQuestionDto subQuestionDto)
    {
        if (subQuestionDto == null)
            return BadRequest();

        var subQuestion = _mapper.Map<SubQuestion>(subQuestionDto);
        _unitOfWork.SubQuestion.Add(subQuestion);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Get), new { id = subQuestion.Id }, _mapper.Map<SubQuestionDto>(subQuestion));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] SubQuestionDto subQuestionDto)
    {
        if (subQuestionDto == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        if (id != subQuestionDto.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingSubQuestion = await _unitOfWork.SubQuestion.GetByIdAsync(id);
        if (existingSubQuestion == null)
            return NotFound($"SubQuestion with id {id} was not found");

        _mapper.Map(subQuestionDto, existingSubQuestion);

        _unitOfWork.SubQuestion.Update(existingSubQuestion);
        await _unitOfWork.SaveAsync();

        return Ok(_mapper.Map<SubQuestionDto>(existingSubQuestion));
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