using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class TipoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoPersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_TipoPersonaDto>>> Get()
    {
        var TipoPersonas = await _unitOfWork.TipoPersonas.GetAllAsync();
        return _mapper.Map<List<G_TipoPersonaDto>>(TipoPersonas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_TipoPersonaDto>>> Get([FromQuery] Params TipoPersonaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoPersonas.GetAllAsync(TipoPersonaParams.PageIndex,TipoPersonaParams.PageSize,TipoPersonaParams.Search);
        var listaProv = _mapper.Map<List<G_TipoPersonaDto>>(registros);
        return new Pager<G_TipoPersonaDto>(listaProv,totalRegistros,TipoPersonaParams.PageIndex,TipoPersonaParams.PageSize,TipoPersonaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoPersona TipoPersona)
    {
         if (TipoPersona == null)
        {
            return BadRequest();
        }
        _unitOfWork.TipoPersonas.Add(TipoPersona);
        await _unitOfWork.SaveAsync();
       
        return "TipoPersona Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoPersona TipoPersona)
    {
        if (TipoPersona == null|| id != TipoPersona.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.TipoPersonas.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(TipoPersona, proveedExiste);
        _unitOfWork.TipoPersonas.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "TipoPersona Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var TipoPersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (TipoPersona == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoPersonas.Remove(TipoPersona);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El TipoPersona {TipoPersona.Id} se eliminó con éxito." });
    }
}