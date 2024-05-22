using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs.UserDTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
            Roles = new List<UserRoleDTO>();
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public List<UserRoleDTO> Roles { get; set; } = new List<UserRoleDTO>();


        public Guid? OrganizationId { get; set; }

        public List<AccountDTO> Accounts { get; set; } = new List<AccountDTO>();



    }
}
