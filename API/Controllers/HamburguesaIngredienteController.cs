
using System.Net.Sockets;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HamburguesaIngredienteController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public HamburguesaIngredienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaIngredienteDto>> Get()
    {
        var lista = await _unitOfWork.HamburguesaIngrediente.GetAllAsync();
        return _mapper.Map<List<HamburguesaIngredienteDto>>(lista);
    }

    [HttpGet("{HamburguesaId}/{IngredienteId}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaIngredienteDto>> GetById(int hamburguesaId, int IngredienteId)
    {
        var registro = await _unitOfWork.HamburguesaIngrediente.GetByIdAsync(hamburguesaId, IngredienteId);
        return _mapper.Map<HamburguesaIngredienteDto>(registro);
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaIngredienteDto>> Post(HamburguesaIngredienteDto data)
    {
        var addRegistro = _mapper.Map<HamburguesaIngrediente>(data);
        _unitOfWork.HamburguesaIngrediente.Add(addRegistro);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<HamburguesaIngredienteDto>(addRegistro); 
    }

    [HttpPut("{HamburguesaId}/{IngredienteId}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaIngredienteDto>> Put(int hamburguesaId, int IngredienteId, [FromBody] HamburguesaIngredienteDto dataUpdate)
    {
        var registro = _mapper.Map<HamburguesaIngrediente>(dataUpdate);
        registro.HamburguesaId = hamburguesaId;
        registro.IngredienteId = IngredienteId;
        _unitOfWork.HamburguesaIngrediente.Update(registro);
        await _unitOfWork.SaveAsync();
        return dataUpdate;
    }

    [HttpDelete("{HamburguesaId}/{IngredienteId}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> Delete(int hamburguesaId, int IngredienteId)
    {
        var registro = await _unitOfWork.HamburguesaIngrediente.GetByIdAsync(hamburguesaId, IngredienteId);
        _unitOfWork.HamburguesaIngrediente.Remove(registro);
        await _unitOfWork.SaveAsync();
        return "Registro eliminado correctamente";
    }

    [HttpGet("paginacion")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaIngredienteDto>>> Pager([FromQuery] Params pagerParams)
    {
        var registros = await _unitOfWork.HamburguesaIngrediente.GetAllAsync
        (
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
        var lista = _mapper.Map<List<HamburguesaIngredienteDto>>(registros.registros);
        return new Pager<HamburguesaIngredienteDto>
        (
            lista,
            registros.totalRegistros,
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
    }
}
