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
public class ContactoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ContactoPersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_contactoPersonaDto>>> Get()
    {
        var Contactos = await _unitOfWork.Contactos.GetAllAsync();
        return _mapper.Map<List<G_contactoPersonaDto>>(Contactos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_contactoPersonaDto>>> Get([FromQuery] Params ContactoPersonaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Contactos.GetAllAsync(ContactoPersonaParams.PageIndex,ContactoPersonaParams.PageSize,ContactoPersonaParams.Search);
        var listaProv = _mapper.Map<List<G_contactoPersonaDto>>(registros);
        return new Pager<G_contactoPersonaDto>(listaProv,totalRegistros,ContactoPersonaParams.PageIndex,ContactoPersonaParams.PageSize,ContactoPersonaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ContactoPersona ContactoPersona)
    {
         if (ContactoPersona == null)
        {
            return BadRequest();
        }
        _unitOfWork.Contactos.Add(ContactoPersona);
        await _unitOfWork.SaveAsync();
       
        return "ContactoPersona Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] ContactoPersona ContactoPersona)
    {
        if (ContactoPersona == null|| id != ContactoPersona.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Contactos.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(ContactoPersona, proveedExiste);
        _unitOfWork.Contactos.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "ContactoPersona Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var ContactoPersona = await _unitOfWork.Contactos.GetByIdAsync(id);
        if (ContactoPersona == null)
        {
            return NotFound();
        }
        _unitOfWork.Contactos.Remove(ContactoPersona);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El ContactoPersona {ContactoPersona.Id} se eliminó con éxito." });
    }
}