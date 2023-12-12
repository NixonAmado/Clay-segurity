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
public class ContratoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ContratoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_contratoDto>>> Get()
    {
        var Contratos = await _unitOfWork.Contratos.GetAllAsync();
        return _mapper.Map<List<G_contratoDto>>(Contratos);
    }
    
    [HttpGet("GetContractByStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContratoEstadoDto>>> GetContractByStatus()
    {
        var Contratos = await _unitOfWork.Contratos.GetContractByStatus();
        return _mapper.Map<List<ContratoEstadoDto>>(Contratos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_contratoDto>>> Get([FromQuery] Params ContratoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Contratos.GetAllAsync(ContratoParams.PageIndex,ContratoParams.PageSize,ContratoParams.Search);
        var listaProv = _mapper.Map<List<G_contratoDto>>(registros);
        return new Pager<G_contratoDto>(listaProv,totalRegistros,ContratoParams.PageIndex,ContratoParams.PageSize,ContratoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Contrato Contrato)
    {
         if (Contrato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Contratos.Add(Contrato);
        await _unitOfWork.SaveAsync();
       
        return "Contrato Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Contrato Contrato)
    {
        if (Contrato == null|| id != Contrato.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Contratos.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Contrato, proveedExiste);
        _unitOfWork.Contratos.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Contrato Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Contrato = await _unitOfWork.Contratos.GetByIdAsync(id);
        if (Contrato == null)
        {
            return NotFound();
        }
        _unitOfWork.Contratos.Remove(Contrato);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Contrato {Contrato.Id} se eliminó con éxito." });
    }
}