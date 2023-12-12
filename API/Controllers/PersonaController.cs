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


    [HttpGet("GetCustomersByantiquity/{quantity}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TPersonaDto>>> GetCustomersByantiquity(int quantity)
    {
        var Personas = await _unitOfWork.Personas.GetCustomersByantiquity(quantity);
        return _mapper.Map<List<TPersonaDto>>(Personas);
    }


    [HttpGet("GetCustomersByDirection/{direction1}/{direction2}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TPersonaDto>>> GetCustomersByDirection(string direction1, string direction2)
    {
        var Personas = await _unitOfWork.Personas.GetCustomersByDirection(direction1, direction2);
        return _mapper.Map<List<TPersonaDto>>(Personas);
    }

    [HttpGet("GetCustomersByCity/{city}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TPersonaDto>>> GetCustomersByCity(string city)
    {
        var Personas = await _unitOfWork.Personas.GetCustomersByCity(city);
        return _mapper.Map<List<TPersonaDto>>(Personas);
    }

    [HttpGet("GetAllPhoneNumEmployeeByCategory/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaNumerosDto>>> GetAllPhoneNumEmployeeByCategory(string category)
    {
        var Personas = await _unitOfWork.Personas.GetAllPhoneNumEmployeeByCategory(category);
        return _mapper.Map<List<PersonaNumerosDto>>(Personas);
    }

    [HttpGet("GetAllEmployeebyCategory/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TPersonaDto>>> GetAllEmployeebyCategory(string category)
    {
        var Personas = await _unitOfWork.Personas.GetAllEmployeebyCategory(category);
        return _mapper.Map<List<TPersonaDto>>(Personas);
    }

    [HttpGet("GetAllEmployeeFromCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TPersonaDto>>> GetAllEmployeeFromCompany()
    {
        var Personas = await _unitOfWork.Personas.GetAllEmployeeFromCompany();
        return _mapper.Map<List<TPersonaDto>>(Personas);
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