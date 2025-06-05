
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSurveys.Controllers;

public class OptionsResponseController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public OptionsResponseController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<OptionsResponse>>>
    Get()
    {
        var optionResponse = await
        _unitOfWork.OptionsResponse.GetAllAsync();
        return Ok(optionResponse);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var optionResponse = await
        _unitOfWork.OptionsResponse.GetByIdAsync(id);
        if (optionResponse == null)
        {
            return NotFound($"Option Response with id {id} was not found");
        }
        return Ok(optionResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OptionsResponse>> Post(OptionsResponse optionsResponse)
    {
        _unitOfWork.OptionsResponse.Add(optionsResponse);
        await _unitOfWork.SaveAsync();
        if (optionsResponse == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = optionsResponse.Id }, optionsResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] OptionsResponse optionsResponse)
    {
        if (optionsResponse == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        if (id != optionsResponse.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingOptionsResponse= await
            _unitOfWork.OptionsResponse.GetByIdAsync(id);
        if (existingOptionsResponse== null)
            return NotFound($"No se encontro el Opciones de respuestacon el id {id}.");
        existingOptionsResponse.OptionText = optionsResponse.OptionText;
        _unitOfWork.OptionsResponse.Update(existingOptionsResponse);

        await _unitOfWork.SaveAsync();

        return Ok(existingOptionsResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var optionsResponse = await _unitOfWork.OptionsResponse.GetByIdAsync(id);
        if (optionsResponse == null)
            return NotFound();

        _unitOfWork.OptionsResponse.Remove(optionsResponse);
        await _unitOfWork.SaveAsync();

        return NoContent();
}

}