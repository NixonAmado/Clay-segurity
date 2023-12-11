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
public class CategoriaPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoriaPersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_CategoriaPersonaDto>>> Get()
    {
        var CategoriaPersonas = await _unitOfWork.CategoriaPersonas.GetAllAsync();
        return _mapper.Map<List<G_CategoriaPersonaDto>>(CategoriaPersonas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_CategoriaPersonaDto>>> Get([FromQuery] Params CategoriaPersonaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.CategoriaPersonas.GetAllAsync(CategoriaPersonaParams.PageIndex,CategoriaPersonaParams.PageSize,CategoriaPersonaParams.Search);
        var listaProv = _mapper.Map<List<G_CategoriaPersonaDto>>(registros);
        return new Pager<G_CategoriaPersonaDto>(listaProv,totalRegistros,CategoriaPersonaParams.PageIndex,CategoriaPersonaParams.PageSize,CategoriaPersonaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(CategoriaPersona CategoriaPersona)
    {
         if (CategoriaPersona == null)
        {
            return BadRequest();
        }
        _unitOfWork.CategoriaPersonas.Add(CategoriaPersona);
        await _unitOfWork.SaveAsync();
       
        return "CategoriaPersona Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] CategoriaPersona CategoriaPersona)
    {
        if (CategoriaPersona == null|| id != CategoriaPersona.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.CategoriaPersonas.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(CategoriaPersona, proveedExiste);
        _unitOfWork.CategoriaPersonas.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "CategoriaPersona Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var CategoriaPersona = await _unitOfWork.CategoriaPersonas.GetByIdAsync(id);
        if (CategoriaPersona == null)
        {
            return NotFound();
        }
        _unitOfWork.CategoriaPersonas.Remove(CategoriaPersona);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El CategoriaPersona {CategoriaPersona.Id} se eliminó con éxito." });
    }
}