using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Purchase.Domain.Models.UserEntities.Permissions;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class MenuController : ControllerBase
    {
        [HttpGet("menuItems")]

        public IActionResult MenuItems()
        {
            try
            {

                var list = Menu.MenuItems;
                list = ProcessUserPermission(list);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }

        private List<MenuItem> ProcessUserPermission(List<MenuItem> list)
        {
            var permissions = User.Claims.Where(s => s.Type == CustomClaimTypes.Permission).Select(s => s.Value).ToList();
            var processed = new List<MenuItem>();
            foreach (var item in list.ToArray())
            {
                item.HasPermission = item.CanAccess(permissions);
                if (item.SubItems != null && item.SubItems.Count > 0)
                {
                    foreach (var subItem in item.SubItems.ToArray())
                    {
                        subItem.HasPermission = subItem.CanAccess(permissions);
                    }
                }
            }
            var main = list.Where(s => s.HasPermission).ToList();
            foreach (var item in main)
            {
                if (item.SubItems != null && item.SubItems.Count > 0)
                {
                    item.SubItems = item.SubItems.Where(s => s.HasPermission).ToList(); ;
                }

            }
            return main;
        }
    }
}
