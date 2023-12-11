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
public class TurnoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TurnoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_TurnoDto>>> Get()
    {
        var Turnos = await _unitOfWork.Turnos.GetAllAsync();
        return _mapper.Map<List<G_TurnoDto>>(Turnos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_TurnoDto>>> Get([FromQuery] Params TurnoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Turnos.GetAllAsync(TurnoParams.PageIndex,TurnoParams.PageSize,TurnoParams.Search);
        var listaProv = _mapper.Map<List<G_TurnoDto>>(registros);
        return new Pager<G_TurnoDto>(listaProv,totalRegistros,TurnoParams.PageIndex,TurnoParams.PageSize,TurnoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Turno Turno)
    {
         if (Turno == null)
        {
            return BadRequest();
        }
        _unitOfWork.Turnos.Add(Turno);
        await _unitOfWork.SaveAsync();
       
        return "Turno Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Turno Turno)
    {
        if (Turno == null|| id != Turno.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Turnos.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Turno, proveedExiste);
        _unitOfWork.Turnos.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Turno Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Turno = await _unitOfWork.Turnos.GetByIdAsync(id);
        if (Turno == null)
        {
            return NotFound();
        }
        _unitOfWork.Turnos.Remove(Turno);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Turno {Turno.Id} se eliminó con éxito." });
    }
}