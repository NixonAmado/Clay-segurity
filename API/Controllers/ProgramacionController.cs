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
public class ProgramacionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProgramacionController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_ProgramacionDto>>> Get()
    {
        var Programaciones = await _unitOfWork.Programaciones.GetAllAsync();
        return _mapper.Map<List<G_ProgramacionDto>>(Programaciones);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_ProgramacionDto>>> Get([FromQuery] Params ProgramacionParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Programaciones.GetAllAsync(ProgramacionParams.PageIndex,ProgramacionParams.PageSize,ProgramacionParams.Search);
        var listaProv = _mapper.Map<List<G_ProgramacionDto>>(registros);
        return new Pager<G_ProgramacionDto>(listaProv,totalRegistros,ProgramacionParams.PageIndex,ProgramacionParams.PageSize,ProgramacionParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Programacion Programacion)
    {
         if (Programacion == null)
        {
            return BadRequest();
        }
        _unitOfWork.Programaciones.Add(Programacion);
        await _unitOfWork.SaveAsync();
       
        return "Programacion Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Programacion Programacion)
    {
        if (Programacion == null|| id != Programacion.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Programaciones.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Programacion, proveedExiste);
        _unitOfWork.Programaciones.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Programacion Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Programacion = await _unitOfWork.Programaciones.GetByIdAsync(id);
        if (Programacion == null)
        {
            return NotFound();
        }
        _unitOfWork.Programaciones.Remove(Programacion);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Programacion {Programacion.Id} se eliminó con éxito." });
    }
}