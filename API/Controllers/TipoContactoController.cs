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
public class TipoContactoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoContactoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_TipoContactoDto>>> Get()
    {
        var TipoContactos = await _unitOfWork.TipoContactos.GetAllAsync();
        return _mapper.Map<List<G_TipoContactoDto>>(TipoContactos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_TipoContactoDto>>> Get([FromQuery] Params TipoContactoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoContactos.GetAllAsync(TipoContactoParams.PageIndex,TipoContactoParams.PageSize,TipoContactoParams.Search);
        var listaProv = _mapper.Map<List<G_TipoContactoDto>>(registros);
        return new Pager<G_TipoContactoDto>(listaProv,totalRegistros,TipoContactoParams.PageIndex,TipoContactoParams.PageSize,TipoContactoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoContacto TipoContacto)
    {
         if (TipoContacto == null)
        {
            return BadRequest();
        }
        _unitOfWork.TipoContactos.Add(TipoContacto);
        await _unitOfWork.SaveAsync();
       
        return "TipoContacto Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoContacto TipoContacto)
    {
        if (TipoContacto == null|| id != TipoContacto.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.TipoContactos.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(TipoContacto, proveedExiste);
        _unitOfWork.TipoContactos.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "TipoContacto Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var TipoContacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (TipoContacto == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoContactos.Remove(TipoContacto);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El TipoContacto {TipoContacto.Id} se eliminó con éxito." });
    }
}