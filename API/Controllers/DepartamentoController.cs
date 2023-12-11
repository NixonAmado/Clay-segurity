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
public class DepartamentoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartamentoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_DepartamentoDto>>> Get()
    {
        var Departamentos = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<G_DepartamentoDto>>(Departamentos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_DepartamentoDto>>> Get([FromQuery] Params DepartamentoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Departamentos.GetAllAsync(DepartamentoParams.PageIndex,DepartamentoParams.PageSize,DepartamentoParams.Search);
        var listaProv = _mapper.Map<List<G_DepartamentoDto>>(registros);
        return new Pager<G_DepartamentoDto>(listaProv,totalRegistros,DepartamentoParams.PageIndex,DepartamentoParams.PageSize,DepartamentoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Departamento Departamento)
    {
         if (Departamento == null)
        {
            return BadRequest();
        }
        _unitOfWork.Departamentos.Add(Departamento);
        await _unitOfWork.SaveAsync();
       
        return "Departamento Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Departamento Departamento)
    {
        if (Departamento == null|| id != Departamento.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Departamentos.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Departamento, proveedExiste);
        _unitOfWork.Departamentos.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Departamento Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (Departamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(Departamento);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Departamento {Departamento.Id} se eliminó con éxito." });
    }
}