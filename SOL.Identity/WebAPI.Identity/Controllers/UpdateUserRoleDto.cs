using System;

namespace WebAPI.Identity.Controllers
{
    public class UpdateUserRoleDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public Boolean Delete { get; set; }
    }
}
