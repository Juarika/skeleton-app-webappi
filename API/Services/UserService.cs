using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class UserService : IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Persona> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Persona> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt.Value;
        _passwordHasher = passwordHasher;
    }
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var persona = new Persona
        {
            Id = registerDto.Id,
            Nombre = registerDto.Nombre,
            ApellidoMaterno = registerDto.ApellidoMaterno,
            ApellidoPaterno = registerDto.ApellidoPaterno,
            Email = registerDto.Email,
            Username = registerDto.Username,
            IdGeneroFk = registerDto.IdGeneroFk,
            IdCiudadFk = registerDto.IdCiudadFk,
            IdTipoPerFk = registerDto.IdTipoPerFk
        };
        persona.Password = _passwordHasher.HashPassword(persona, registerDto.Password);
        var usuarioExiste = _unitOfWork.Personas
                                        .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                                        .FirstOrDefault();
        if (usuarioExiste == null)
        {
            var rolPredeterminado = _unitOfWork.Roles
                                        .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                        .FirstOrDefault();
            try 
            {
                persona.Roles.Add(rolPredeterminado);
                _unitOfWork.Personas.Add(persona);
                await _unitOfWork.SaveAsync();
                return $"El usuario {registerDto.Username} ha sido registrado exitosamente.";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else 
        {
            return $"El usuario {registerDto.Username} ya se encuentra registrado.";
        }
    }

    public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    {
        DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
        var persona = await _unitOfWork.Personas
                                        .GetByUsernameAsync(model.Username);
        
        if (persona == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"No existe ningun usuario con el username {model.Username}.";
            return datosUsuarioDto;
        }

        var resultado = _passwordHasher.VerifyHashedPassword(persona, persona.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            datosUsuarioDto.EstaAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(persona);
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.Email = persona.Email;
            datosUsuarioDto.UserName = persona.Username;
            datosUsuarioDto.Roles = persona.Roles
                                            .Select(u => u.Nombre)
                                            .ToList();
            return datosUsuarioDto;
        }

        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {persona.Username}";
        return datosUsuarioDto;
    }

    public async Task<string> AddRoleAsync(AddRoleDto model)
    {
        var persona = await _unitOfWork.Personas
                                        .GetByUsernameAsync(model.Username);
        if (persona == null)
        {
            return $"No existe algun usuario registrado con la cuenta {model.Username}.";
        }                                        

        var resultado =  _passwordHasher.VerifyHashedPassword(persona, persona.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            var rolExiste = _unitOfWork.Roles
                                        .Find(u => u.Nombre.ToLower() == model.Role.ToLower())
                                        .FirstOrDefault();
            if (rolExiste != null)
            {
                var personaTieneRol = persona.Roles
                                                .Any(u => u.Id == rolExiste.Id);
                
                if (personaTieneRol == false)
                {
                    persona.Roles.Add(rolExiste);
                    _unitOfWork.Personas.Update(persona);
                    await _unitOfWork.SaveAsync();
                }

                return $"Rol {model.Role} agregado a la cuenta de {persona.Username} de forma exitosa.";
            }

            return $"Rol {model.Role} no encontrado.";
        }

        return $"Credenciales incorrectas para el usuario {persona.Username}";
    }

    private JwtSecurityToken CreateJwtToken(Persona persona)
    {
        var roles = persona.Roles;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Nombre));
        }
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, persona.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, persona.Email),
            new Claim("uid", persona.Id.ToString())
        }.Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.HasKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials
        );
        return jwtSecurityToken;
    }
}