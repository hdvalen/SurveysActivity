using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Controllers;

public class SurveysController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SurveysController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SurveyDto>>> Get()
    {
        var surveys = await _unitOfWork.Survey.GetAllAsync();
        return Ok(_mapper.Map<List<SurveyDto>>(surveys));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var survey = await _unitOfWork.Survey.GetByIdAsync(id);
        if (survey == null)
            return NotFound($"Survey with id {id} was not found");
        return Ok(_mapper.Map<SurveyDto>(survey));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SurveyDto>> Post(SurveyDto surveyDto)
    {
        if (surveyDto == null)
            return BadRequest();

        var survey = _mapper.Map<Survey>(surveyDto);
        _unitOfWork.Survey.Add(survey);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Get), new { id = survey.Id }, _mapper.Map<SurveyDto>(survey));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] SurveyDto surveyDto)
    {
        if (surveyDto == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        if (id != surveyDto.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingSurvey = await _unitOfWork.Survey.GetByIdAsync(id);
        if (existingSurvey == null)
            return NotFound($"No se encontro Survey con el id {id}.");

        _mapper.Map(surveyDto, existingSurvey);

        _unitOfWork.Survey.Update(existingSurvey);
        await _unitOfWork.SaveAsync();

        return Ok(_mapper.Map<SurveyDto>(existingSurvey));
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