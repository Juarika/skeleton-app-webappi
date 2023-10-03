using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using API.Services;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiVersion("1.1")]
public class UsuariosController : BaseApiController
{
    private readonly IUserService userService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UsuariosController(IUserService _userService, IUnitOfWork _unitOfWork, IMapper _mapper)
    {
        userService = _userService;
        unitOfWork = _unitOfWork;
        mapper = _mapper;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await userService.GetTokenAsync(model);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await userService.AddRoleAsync(model);
        return Ok(result);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PersonaDto>>> Get11([FromQuery] Params personaParams)
    {
        var persona = await unitOfWork.Personas.GetAllAsync(personaParams.PageIndex, personaParams.PageSize, personaParams.Search);
        var lstpersonasDto = mapper.Map<List<PersonaDto>>(persona.registros);
        return new Pager<PersonaDto>(lstpersonasDto, persona.totalRegistros, personaParams.PageIndex, personaParams.PageSize, personaParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Get(int id)
    {
        var persona = await unitOfWork.Personas.GetByIdAsync(id);
        return mapper.Map<PersonaDto>(persona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(PersonaDto personaDto)
    {
        var persona = mapper.Map<Persona>(personaDto);
        unitOfWork.Personas.Add(persona);
        await unitOfWork.SaveAsync();
        if (persona == null)
        {
            return BadRequest();
        }
        personaDto.Id = persona.Id;
        return CreatedAtAction(nameof(Post),new {id = personaDto.Id}, personaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody] PersonaDto personaDto)
    {
        if (personaDto == null) return NotFound();
        var personas = mapper.Map<Persona>(personaDto);
        unitOfWork.Personas.Update(personas);
        await unitOfWork.SaveAsync();
        return personaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var persona = await unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null) return NotFound();
        unitOfWork.Personas.Remove(persona);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}