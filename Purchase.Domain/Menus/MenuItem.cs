using System.Text.Json.Serialization;

namespace Purchase.Domain.Menus
{
    public class MenuItem
    {
        public MenuItem()
        {

        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }
        public string Label { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsTitle { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ParentId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Icon { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Link { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MenuItem> SubItems { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public List<string> Permissions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool HasPermission { get; set; }

        public bool CanAccess(List<string> userPermsissions)
        {
            if (Permissions != null && Permissions.Any())
            {
                foreach (var permission in Permissions)
                {
                    if (userPermsissions.Contains(permission))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
