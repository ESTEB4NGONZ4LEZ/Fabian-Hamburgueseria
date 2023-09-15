
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class IngredienteController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public IngredienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<IngredienteDto>> Get()
    {
        var lista = await _unitOfWork.Ingrediente.GetAllAsync();
        return _mapper.Map<List<IngredienteDto>>(lista);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> GetById(int id)
    {
        var registro = await _unitOfWork.Ingrediente.GetByIdAsync(id);
        if(registro == null) return NotFound();
        return _mapper.Map<IngredienteDto>(registro);
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> Post(IngredienteDto data)
    {
        if(data == null) return BadRequest();
        var addRegistro = _mapper.Map<Ingrediente>(data);
        _unitOfWork.Ingrediente.Add(addRegistro);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Post), new {id = addRegistro.Id}, addRegistro);  
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> Put(int id, [FromBody] IngredienteDto dataUpdate)
    {
        if(dataUpdate == null) return NotFound();
        var registro = _mapper.Map<Ingrediente>(dataUpdate);
        registro.Id = id;
        _unitOfWork.Ingrediente.Update(registro);
        await _unitOfWork.SaveAsync();
        return dataUpdate;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var registro = await _unitOfWork.Ingrediente.GetByIdAsync(id);
        if(registro == null) return NotFound(); 
        _unitOfWork.Ingrediente.Remove(registro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("paginacion")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<IngredienteDto>>> Pager([FromQuery] Params pagerParams)
    {
        var registros = await _unitOfWork.Ingrediente.GetAllAsync
        (
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
        var lista = _mapper.Map<List<IngredienteDto>>(registros.registros);
        return new Pager<IngredienteDto>
        (
            lista,
            registros.totalRegistros,
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
    }

    [HttpGet("stock")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<IngredienteDto>> GetIngredientesStock()
    {
        var lista = await _unitOfWork.Ingrediente.GetIngredienteStock();
        return _mapper.Map<List<IngredienteDto>>(lista);
    }

    [HttpGet("precioMaximo")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> GetIngredienteMasCaro()
    {
        var registro = await _unitOfWork.Ingrediente.GetIngredienteMasCaro();
        if(registro == null) return NotFound();
        return _mapper.Map<IngredienteDto>(registro);
    }

    [HttpGet("rangoPrecio")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<IngredienteDto>> GetIngredientesRangoPrecio()
    {
        var lista = await _unitOfWork.Ingrediente.GetIngredienteRangoPrecio();
        return _mapper.Map<List<IngredienteDto>>(lista);
    }

    // [HttpPut("descripcionPan")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> PutPan()
    // {
    //     var data = _unitOfWork.Ingrediente.GetPan();
    //     if(data == null) return BadRequest("El ingrediente pan no existe");
    //     var registro = _mapper.Map<Ingrediente>(data);
    //     registro.Id = data.Id;
    //     registro.Descripcion = "Pan fresco y crujiente";
    //     _unitOfWork.Ingrediente.Update(registro);
    //     await _unitOfWork.SaveAsync();
    //     return Ok(registro);
    // }
}
