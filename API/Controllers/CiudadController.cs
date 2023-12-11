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
public class CiudadController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CiudadController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_CiudadDto>>> Get()
    {
        var Ciudades = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<G_CiudadDto>>(Ciudades);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_CiudadDto>>> Get([FromQuery] Params CiudadParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Ciudades.GetAllAsync(CiudadParams.PageIndex,CiudadParams.PageSize,CiudadParams.Search);
        var listaProv = _mapper.Map<List<G_CiudadDto>>(registros);
        return new Pager<G_CiudadDto>(listaProv,totalRegistros,CiudadParams.PageIndex,CiudadParams.PageSize,CiudadParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Ciudad Ciudad)
    {
         if (Ciudad == null)
        {
            return BadRequest();
        }
        _unitOfWork.Ciudades.Add(Ciudad);
        await _unitOfWork.SaveAsync();
       
        return "Ciudad Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Ciudad Ciudad)
    {
        if (Ciudad == null|| id != Ciudad.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Ciudades.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Ciudad, proveedExiste);
        _unitOfWork.Ciudades.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Ciudad Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (Ciudad == null)
        {
            return NotFound();
        }
        _unitOfWork.Ciudades.Remove(Ciudad);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Ciudad {Ciudad.Id} se eliminó con éxito." });
    }
}