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
public class EstadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EstadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_EstadoDto>>> Get()
    {
        var Estados = await _unitOfWork.Estados.GetAllAsync();
        return _mapper.Map<List<G_EstadoDto>>(Estados);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_EstadoDto>>> Get([FromQuery] Params EstadoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Estados.GetAllAsync(EstadoParams.PageIndex,EstadoParams.PageSize,EstadoParams.Search);
        var listaProv = _mapper.Map<List<G_EstadoDto>>(registros);
        return new Pager<G_EstadoDto>(listaProv,totalRegistros,EstadoParams.PageIndex,EstadoParams.PageSize,EstadoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Estado Estado)
    {
         if (Estado == null)
        {
            return BadRequest();
        }
        _unitOfWork.Estados.Add(Estado);
        await _unitOfWork.SaveAsync();
       
        return "Estado Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Estado Estado)
    {
        if (Estado == null|| id != Estado.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Estados.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Estado, proveedExiste);
        _unitOfWork.Estados.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Estado Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Estado = await _unitOfWork.Estados.GetByIdAsync(id);
        if (Estado == null)
        {
            return NotFound();
        }
        _unitOfWork.Estados.Remove(Estado);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Estado {Estado.Id} se eliminó con éxito." });
    }
}