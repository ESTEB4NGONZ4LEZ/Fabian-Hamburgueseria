
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class CategoriaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoriaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<CategoriaDto>> Get()
    {
        var lista = await _unitOfWork.Categoria.GetAllAsync();
        return _mapper.Map<List<CategoriaDto>>(lista);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> GetById(int id)
    {
        var registro = await _unitOfWork.Categoria.GetByIdAsync(id);
        if(registro == null) return NotFound();
        return _mapper.Map<CategoriaDto>(registro);
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Post(CategoriaDto data)
    {
        if(data == null) return BadRequest();
        var addRegistro = _mapper.Map<Categoria>(data);
        _unitOfWork.Categoria.Add(addRegistro);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Post), new {id = addRegistro.Id}, addRegistro);  
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody] CategoriaDto dataUpdate)
    {
        if(dataUpdate == null) return NotFound();
        var registro = _mapper.Map<Categoria>(dataUpdate);
        registro.Id = id;
        _unitOfWork.Categoria.Update(registro);
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
        var registro = await _unitOfWork.Categoria.GetByIdAsync(id);
        if(registro == null) return NotFound(); 
        _unitOfWork.Categoria.Remove(registro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("paginacion")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaDto>>> Pager([FromQuery] Params pagerParams)
    {
        var registros = await _unitOfWork.Categoria.GetAllAsync
        (
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
        var lista = _mapper.Map<List<CategoriaDto>>(registros.registros);
        return new Pager<CategoriaDto>
        (
            lista,
            registros.totalRegistros,
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
    }

    [HttpGet("gourmet")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<CategoriaDto>> GetCategoria()
    {
        var lista = await _unitOfWork.Categoria.GetCategoriaGormet();
        return _mapper.Map<List<CategoriaDto>>(lista);
    }

    
}
