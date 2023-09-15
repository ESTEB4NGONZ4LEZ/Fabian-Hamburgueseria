
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class ChefController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ChefController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<ChefDto>> Get()
    {
        var lista = await _unitOfWork.Chef.GetAllAsync();
        return _mapper.Map<List<ChefDto>>(lista);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChefDto>> GetById(int id)
    {
        var registro = await _unitOfWork.Chef.GetByIdAsync(id);
        if(registro == null) return NotFound();
        return _mapper.Map<ChefDto>(registro);
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChefDto>> Post(ChefDto data)
    {
        if(data == null) return BadRequest();
        var addRegistro = _mapper.Map<Chef>(data);
        _unitOfWork.Chef.Add(addRegistro);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Post), new {id = addRegistro.Id}, addRegistro);  
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChefDto>> Put(int id, [FromBody] ChefDto dataUpdate)
    {
        if(dataUpdate == null) return NotFound();
        var registro = _mapper.Map<Chef>(dataUpdate);
        registro.Id = id;
        _unitOfWork.Chef.Update(registro);
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
        var registro = await _unitOfWork.Chef.GetByIdAsync(id);
        if(registro == null) return NotFound(); 
        _unitOfWork.Chef.Remove(registro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("paginacion")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ChefDto>>> Pager([FromQuery] Params pagerParams)
    {
        var registros = await _unitOfWork.Chef.GetAllAsync
        (
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
        var lista = _mapper.Map<List<ChefDto>>(registros.registros);
        return new Pager<ChefDto>
        (
            lista,
            registros.totalRegistros,
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
    }

    [HttpGet("especializadoCarnes")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<ChefDto>> GetChefEspecializado()
    {
        var lista = await _unitOfWork.Chef.GetChefEspecializado();
        return _mapper.Map<List<ChefDto>>(lista);
    }
}
