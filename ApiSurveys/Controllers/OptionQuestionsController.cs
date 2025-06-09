using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Controllers;

public class OptionQuestionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OptionQuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OptionsQuestionsDto>>> Get()
    {
        var optionQuestions = await _unitOfWork.OptionQuestion.GetAllAsync();
        return _mapper.Map<List<OptionsQuestionsDto>>(optionQuestions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OptionsQuestionsDto>> Get(int id)
    {
        var optionQuestion = await _unitOfWork.OptionQuestion.GetByIdAsync(id);
        if (optionQuestion == null)
            return NotFound($"Option Question with id {id} was not found");
        return _mapper.Map<OptionsQuestionsDto>(optionQuestion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<OptionQuestion>> Post(OptionsQuestionsDto optionQuestionDto)
    {
        var OptionQuestion = _mapper.Map<OptionQuestion>(optionQuestionDto);
        _unitOfWork.OptionQuestion.Add(OptionQuestion);
        await _unitOfWork.SaveAsync();
        if (optionQuestionDto == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = optionQuestionDto.Id }, optionQuestionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] OptionsQuestionsDto optionQuestionDto)
    {
        if (optionQuestionDto == null)
            return BadRequest("El cuerpo de la solicitud está vacío.");

        var OptionQuestion = _mapper.Map<OptionQuestion>(optionQuestionDto);
        _unitOfWork.OptionQuestion.Update(OptionQuestion);
        await _unitOfWork.SaveAsync();

        return Ok(optionQuestionDto);
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