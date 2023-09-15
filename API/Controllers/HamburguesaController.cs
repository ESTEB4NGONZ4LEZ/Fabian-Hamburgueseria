
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class HamburguesaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public HamburguesaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaDto>> Get()
    {
        var lista = await _unitOfWork.Hamburguesa.GetAllAsync();
        return _mapper.Map<List<HamburguesaDto>>(lista);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaDto>> GetById(int id)
    {
        var registro = await _unitOfWork.Hamburguesa.GetByIdAsync(id);
        if(registro == null) return NotFound();
        return _mapper.Map<HamburguesaDto>(registro);
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaDto>> Post(HamburguesaDto data)
    {
        if(data == null) return BadRequest();
        var addRegistro = _mapper.Map<Hamburguesa>(data);
        _unitOfWork.Hamburguesa.Add(addRegistro);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(Post), new {id = addRegistro.Id}, addRegistro);  
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaDto>> Put(int id, [FromBody] HamburguesaDto dataUpdate)
    {
        if(dataUpdate == null) return NotFound();
        var registro = _mapper.Map<Hamburguesa>(dataUpdate);
        registro.Id = id;
        _unitOfWork.Hamburguesa.Update(registro);
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
        var registro = await _unitOfWork.Hamburguesa.GetByIdAsync(id);
        if(registro == null) return NotFound(); 
        _unitOfWork.Hamburguesa.Remove(registro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("paginacion")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaDto>>> Pager([FromQuery] Params pagerParams)
    {
        var registros = await _unitOfWork.Hamburguesa.GetAllAsync
        (
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
        var lista = _mapper.Map<List<HamburguesaDto>>(registros.registros);
        return new Pager<HamburguesaDto>
        (
            lista,
            registros.totalRegistros,
            pagerParams.PageIndex,
            pagerParams.PageSize,
            pagerParams.Search
        );
    }

    [HttpGet("vegetariana")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaDto>> GetHamburguesaVegetariana()
    {
        var idCategoria = _unitOfWork.Categoria.GetIdCategoria();
        var lista = await _unitOfWork.Hamburguesa.GetHamburguesaVegetariana(idCategoria);
        return _mapper.Map<List<HamburguesaDto>>(lista);
    }

    [HttpGet("rangoPrecio")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaDto>> GetHamburguesaRangoPrecio()
    {
        var lista = await _unitOfWork.Hamburguesa.GetHamburguesaInRange();
        return _mapper.Map<List<HamburguesaDto>>(lista);
    }

    [HttpGet("precioAscendente")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaDto>> GetHamburguesaPrecioAscendente()
    {
        var lista = await _unitOfWork.Hamburguesa.GetHamburguesaPrecioAscendente();
        return _mapper.Map<List<HamburguesaDto>>(lista);
    }

    [HttpGet("porChef")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaDto>> GetHamburguesaxChef(string nombre)
    {
        var idChef = _unitOfWork.Chef.GetIdChef(nombre);
        var lista = await _unitOfWork.Hamburguesa.GetHamburguesaxChef(idChef);
        return _mapper.Map<List<HamburguesaDto>>(lista);
    }

    [HttpGet("panIntegral")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<List<HamburguesaDto>> GetHamburguesaPanIntegral()
    {
        var idIngrediente = _unitOfWork.Ingrediente.GetIdPanIntegral();
        var lstHamburguesas = _unitOfWork.HamburguesaIngrediente.GetIdHamburguesasPanIntegral(idIngrediente);
        List<Hamburguesa> resultado = new();
        foreach(var id in lstHamburguesas)
        {
            var hamburguesa = await _unitOfWork.Hamburguesa.GetHamburguesaById(id);
            resultado.Add(hamburguesa); 
        }
        return _mapper.Map<List<HamburguesaDto>>(resultado);
    }

    // [HttpGet("noQueso")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<List<HamburguesaDto>> GetHamburguesaNoQueso()
    // {
    //     var idIngrediente = _unitOfWork.Ingrediente.GetIdQueso();
    //     var lstHamburguesas = _unitOfWork.HamburguesaIngrediente.GetIdHamburguesaNoQueso(idIngrediente);
    //     List<Hamburguesa> resultado = new();
    //     foreach(var id in lstHamburguesas)
    //     {
    //         var hamburguesa = await _unitOfWork.Hamburguesa.GetHamburguesaById(id);
    //         resultado.Add(hamburguesa); 
    //     }
    //     return _mapper.Map<List<HamburguesaDto>>(resultado);
    // }
}
