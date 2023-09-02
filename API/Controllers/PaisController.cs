using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class PaisController : BaseApiController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public PaisController(IUnitOfWork _unitOfWork, IMapper _mapper)
    {
        unitOfWork = _unitOfWork;
        mapper = _mapper;
    }

    // [HttpGet]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<IEnumerable<Pais>>> Get()
    // {
    //     var paises = await unitOfWork.Paises.GetAllAsync();
    //     return Ok(paises);
    // }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
    {
        var paises = await unitOfWork.Paises.GetAllAsync();
        return mapper.Map<List<PaisDto>>(paises);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PaisDto>>> Get11()
    {
        var paises = await unitOfWork.Paises.GetAllAsync();
        return mapper.Map<List<PaisDto>>(paises);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisDto>> Get(int id)
    {
        var pais = await unitOfWork.Paises.GetByIdAsync(id);
        return mapper.Map<PaisDto>(pais);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pais>> Post(PaisDto paisDto)
    {
        var pais = mapper.Map<Pais>(paisDto);
        unitOfWork.Paises.Add(pais);
        await unitOfWork.SaveAsync();
        if (pais == null)
        {
            return BadRequest();
        }
        paisDto.Id = pais.Id;
        return CreatedAtAction(nameof(Post),new {id = paisDto.Id}, paisDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisDto>> Put(int id, [FromBody] PaisDto paisDto)
    {
        if (paisDto == null) return NotFound();
        var paises = mapper.Map<Pais>(paisDto);
        unitOfWork.Paises.Update(paises);
        await unitOfWork.SaveAsync();
        return paisDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var pais = await unitOfWork.Paises.GetByIdAsync(id);
        if (pais == null) return NotFound();
        unitOfWork.Paises.Remove(pais);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}