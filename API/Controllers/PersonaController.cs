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
public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_PersonaDto>>> Get()
    {
        var Personas = await _unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<G_PersonaDto>>(Personas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_PersonaDto>>> Get([FromQuery] Params PersonaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Personas.GetAllAsync(PersonaParams.PageIndex,PersonaParams.PageSize,PersonaParams.Search);
        var listaProv = _mapper.Map<List<G_PersonaDto>>(registros);
        return new Pager<G_PersonaDto>(listaProv,totalRegistros,PersonaParams.PageIndex,PersonaParams.PageSize,PersonaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Persona Persona)
    {
         if (Persona == null)
        {
            return BadRequest();
        }
        _unitOfWork.Personas.Add(Persona);
        await _unitOfWork.SaveAsync();
       
        return "Persona Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Persona Persona)
    {
        if (Persona == null|| id != Persona.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Personas.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Persona, proveedExiste);
        _unitOfWork.Personas.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Persona Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (Persona == null)
        {
            return NotFound();
        }
        _unitOfWork.Personas.Remove(Persona);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Persona {Persona.Id} se eliminó con éxito." });
    }
}