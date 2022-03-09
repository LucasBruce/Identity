using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Domain;
using WebAPI.Identity.Dto;

namespace WebAPI.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            return Ok(new
            {
                role = new RoleDto(),
                updateUserRole = new UpdateUserRoleDto()
            });
        }

        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "Admin, Gerente")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            try
            {
                var retorno = await _roleManager.CreateAsync(new Role { Name = roleDto.Name });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"ERROR {ex.Message}");
            }
        }

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRoleDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (model.Delete)
                        await _userManager.RemoveFromRoleAsync(user, model.Role);
                    else
                        await _userManager.AddToRoleAsync(user, model.Role);
                }
                else
                {
                    return Ok("Usuário não encontrado");
                }
                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
    }
}
