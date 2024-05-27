using Purchase.Domain.Models.UserEntities;

namespace Purchase.Domain.Menus
{
    public class Menu
    {
        public static List<MenuItem> MenuItems
        {
            get
            {
                var list = new List<MenuItem>();
                list.Add(new MenuItem { IsTitle = true, Label = "Main" });

                list.Add(new MenuItem { Label = "Dashboard", Icon = "home", Link = "/" });
                list.Add(new MenuItem { IsTitle = true, Label = "Operations" });
                //taks
                var taskMenu = new MenuItem
                {
                    Id = 5,
                    Label = "Task Management",
                    Icon = "briefcase",
                    Permissions = new List<string> { Permissions.Menus.AppTask }
                };
                taskMenu.SubItems = new List<MenuItem>
                {
                     new MenuItem
                    {
                        Label = "Task List",
                        Link = "/ops-task/task",
                        ParentId = 5,
                        Permissions = new List<string> { Permissions.AppTaskPermision.View }
                    },
                };
                list.Add(taskMenu);

                var assessmentMenu = new MenuItem
                {
                    Id = 5,
                    Label = "Assessment",
                    Icon = "briefcase",
                    Permissions = new List<string> { Permissions.Menus.AppTask }
                };
                assessmentMenu.SubItems = new List<MenuItem>
                {
                     new MenuItem
                    {
                        Label = "Assessment List",
                        Link = "/ops-asmt/assessment",
                        ParentId = 5,
                        Permissions = new List<string> { Permissions.AppTaskPermision.View }
                    },


                };
                list.Add(assessmentMenu);


                //feeNote
                var feeNoteMenu = new MenuItem
                {
                    Id = 5,
                    Label = "Invoicing",
                    Icon = "briefcase",
                    Permissions = new List<string> { Permissions.Menus.Invoice }
                };
                feeNoteMenu.SubItems = new List<MenuItem>
                {
                     new MenuItem
                    {
                        Label = "Fee Notes",
                        Link = "/ops-invoice/feenote",
                        ParentId = 5,
                        Permissions = new List<string> { Permissions.Invoicing.View }
                    },


                };
                list.Add(feeNoteMenu);



                var reportMenu = new MenuItem
                {
                    Id = 96,
                    Label = "Reports",
                    Icon = "clipboard",
                    Link = "/report/dashboard",
                    Permissions = new List<string> { Permissions.Menus.Report, Permissions.Report.Dashboard }
                };
                //reportMenu.SubItems = new List<MenuItem>
                //{
                //    new MenuItem
                //    {
                //        Label = "Dashboard",
                //        Link = "/report/dashboard",
                //        ParentId = 96,
                //        Permissions = new List<string> { Permissions.Report.Dashboard }
                //    }
                //};
                list.Add(reportMenu);

                list.Add(new MenuItem { IsTitle = true, Label = "Admin", Permissions = new List<string> { Permissions.Menus.Setting, Permissions.Menus.UserManagement, Permissions.Menus.MasterData } });
                //master data
                var mdMenu = new MenuItem
                {
                    Id = 99,
                    Label = "Master Data",
                    Icon = "globe",
                    Permissions = new List<string> { Permissions.Menus.MasterData }
                };
                mdMenu.SubItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Label = "Clients",
                        Link = "/masterdata/client",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.AccountMasterData.View }
                    },
                     new MenuItem
                    {
                        Label = "Assessors",
                        Link = "/masterdata/asessor",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.AccountMasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Areas",
                        Link = "/masterdata/area",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.MasterData.View }
                    },                 
                     new MenuItem
                    {
                        Label = "Taxes",
                        Link = "/masterdata/tax",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.MasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Charges",
                        Link = "/masterdata/charge",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.MasterData.View }
                    },
                    
                    new MenuItem
                    {
                        Label = "Vehicle Makes",
                        Link = "/masterdata/vehicle-make",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Vehicle Categories",
                        Link = "/masterdata/vehicle-category",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Vehicle Colors",
                        Link = "/masterdata/vehicle-color",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },
                     new MenuItem
                    {
                        Label = "Part Categories",
                        Link = "/masterdata/part-category",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Parts",
                        Link = "/masterdata/part",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Part Conditions",
                        Link = "/masterdata/part-condition",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },
                    new MenuItem
                    {
                        Label = "Vehicle Model Parts",
                        Link = "/masterdata/vehicle-model-part",
                        ParentId = 99,
                        Permissions = new List<string> { Permissions.VehicleMasterData.View }
                    },


                };
                list.Add(mdMenu);

                //user management
                var umMenu = new MenuItem
                {
                    Label = "User Manangement",
                    Icon = "users",
                    Id = 98,
                    Permissions = new List<string> { Permissions.Menus.UserManagement }
                };
                umMenu.SubItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Label = "Users",
                        Link = "/um/user",
                        ParentId = 98,
                        Permissions = new List<string> { Permissions.Users.View }
                    },
                    new MenuItem
                    {
                        Label = "Roles",
                        Link = "/um/role",
                        ParentId = 98,
                        Permissions = new List<string> { Permissions.Users.Roles }
                    }
                };
                list.Add(umMenu);

                //settings
                var seMenu = new MenuItem
                {
                    Label = "Setting",
                    Icon = "settings",
                    Id = 97,
                    Permissions = new List<string> { Permissions.Menus.Setting }
                };
                seMenu.SubItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Label = "Report Groups",
                        Link = "/settings/reportgroup",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },
                    new MenuItem
                    {
                        Label = "Upload Types",
                        Link = "/settings/upload-type",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },
                    new MenuItem
                    {
                        Label = "Upload Configuration",
                        Link = "/settings/upload-config",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },
                    new MenuItem
                    {
                        Label = "Configuration Options",
                        Link = "/settings/config-options",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },


                    new MenuItem
                    {
                        Label = "Document Templates",
                        Link = "/settings/document-template",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },
                    new MenuItem
                    {
                        Label = "Email Templates",
                        Link = "/settings/email-template",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },
                    new MenuItem
                    {
                        Label = "Notifications",
                        Link = "/settings/notification",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },
                    new MenuItem
                    {
                        Label = "Group Contacts",
                        Link = "/settings/group-contact",
                        ParentId = 97,
                        Permissions = new List<string> { Permissions.Setting.Update }
                    },


                };
                list.Add(seMenu);

                return list;
            }
        }
    }
}
