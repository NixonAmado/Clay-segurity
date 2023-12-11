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
public class TipoDireccionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoDireccionController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_TipoDireccionDto>>> Get()
    {
        var TipoDirecciones = await _unitOfWork.TipoDirecciones.GetAllAsync();
        return _mapper.Map<List<G_TipoDireccionDto>>(TipoDirecciones);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_TipoDireccionDto>>> Get([FromQuery] Params TipoDireccionParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoDirecciones.GetAllAsync(TipoDireccionParams.PageIndex,TipoDireccionParams.PageSize,TipoDireccionParams.Search);
        var listaProv = _mapper.Map<List<G_TipoDireccionDto>>(registros);
        return new Pager<G_TipoDireccionDto>(listaProv,totalRegistros,TipoDireccionParams.PageIndex,TipoDireccionParams.PageSize,TipoDireccionParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoDireccion TipoDireccion)
    {
         if (TipoDireccion == null)
        {
            return BadRequest();
        }
        _unitOfWork.TipoDirecciones.Add(TipoDireccion);
        await _unitOfWork.SaveAsync();
       
        return "TipoDireccion Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoDireccion TipoDireccion)
    {
        if (TipoDireccion == null|| id != TipoDireccion.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.TipoDirecciones.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(TipoDireccion, proveedExiste);
        _unitOfWork.TipoDirecciones.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "TipoDireccion Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var TipoDireccion = await _unitOfWork.TipoDirecciones.GetByIdAsync(id);
        if (TipoDireccion == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoDirecciones.Remove(TipoDireccion);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El TipoDireccion {TipoDireccion.Id} se eliminó con éxito." });
    }
}