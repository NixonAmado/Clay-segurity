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
public class PaisController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PaisController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_PaisDto>>> Get()
    {
        var Paises = await _unitOfWork.Paises.GetAllAsync();
        return _mapper.Map<List<G_PaisDto>>(Paises);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_PaisDto>>> Get([FromQuery] Params PaisParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Paises.GetAllAsync(PaisParams.PageIndex,PaisParams.PageSize,PaisParams.Search);
        var listaProv = _mapper.Map<List<G_PaisDto>>(registros);
        return new Pager<G_PaisDto>(listaProv,totalRegistros,PaisParams.PageIndex,PaisParams.PageSize,PaisParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Pais Pais)
    {
         if (Pais == null)
        {
            return BadRequest();
        }
        _unitOfWork.Paises.Add(Pais);
        await _unitOfWork.SaveAsync();
       
        return "Pais Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Pais Pais)
    {
        if (Pais == null|| id != Pais.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Paises.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Pais, proveedExiste);
        _unitOfWork.Paises.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Pais Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Pais = await _unitOfWork.Paises.GetByIdAsync(id);
        if (Pais == null)
        {
            return NotFound();
        }
        _unitOfWork.Paises.Remove(Pais);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Pais {Pais.Id} se eliminó con éxito." });
    }
}