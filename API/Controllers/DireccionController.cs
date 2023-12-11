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
public class DireccionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DireccionController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_DireccionDto>>> Get()
    {
        var Direcciones = await _unitOfWork.Direcciones.GetAllAsync();
        return _mapper.Map<List<G_DireccionDto>>(Direcciones);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_DireccionDto>>> Get([FromQuery] Params DireccionParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Direcciones.GetAllAsync(DireccionParams.PageIndex,DireccionParams.PageSize,DireccionParams.Search);
        var listaProv = _mapper.Map<List<G_DireccionDto>>(registros);
        return new Pager<G_DireccionDto>(listaProv,totalRegistros,DireccionParams.PageIndex,DireccionParams.PageSize,DireccionParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Direccion Direccion)
    {
         if (Direccion == null)
        {
            return BadRequest();
        }
        _unitOfWork.Direcciones.Add(Direccion);
        await _unitOfWork.SaveAsync();
       
        return "Direccion Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Direccion Direccion)
    {
        if (Direccion == null|| id != Direccion.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Direcciones.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Direccion, proveedExiste);
        _unitOfWork.Direcciones.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Direccion Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Direccion = await _unitOfWork.Direcciones.GetByIdAsync(id);
        if (Direccion == null)
        {
            return NotFound();
        }
        _unitOfWork.Direcciones.Remove(Direccion);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Direccion {Direccion.Id} se eliminó con éxito." });
    }
}