using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.UserEntities
{
    public class RolePermissions { 
    public static List<RoleClaimPermission> ClaimPermissions
    {
        get
        {
            var permissions = new List<RoleClaimPermission>();
            //users
            permissions.Add(new RoleClaimPermission(Permissions.Menus.UserManagement, "User management menu", "USER MANAGEMENT"));
            permissions.Add(new RoleClaimPermission(Permissions.Users.Edit, "Edit system users", "USER MANAGEMENT"));
            permissions.Add(new RoleClaimPermission(Permissions.Users.View, "View system users", "USER MANAGEMENT"));
            permissions.Add(new RoleClaimPermission(Permissions.Users.Activate, "Activate system users", "USER MANAGEMENT"));
            permissions.Add(new RoleClaimPermission(Permissions.Users.Roles, "Add, Edit and View system roles", "USER MANAGEMENT"));
            permissions.Add(new RoleClaimPermission(Permissions.Users.Permissiion, "Access permissions", "USER MANAGEMENT"));
            //masterdata
            permissions.Add(new RoleClaimPermission(Permissions.Menus.MasterData, "Master data menu", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.MasterData.Add, "Add  masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.MasterData.Edit, "Edit  masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.MasterData.View, "View  masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.MasterData.Delete, "Delete  masterdata", "MASTER DATA"));

            //masterdata:vehicle                
            permissions.Add(new RoleClaimPermission(Permissions.VehicleMasterData.Add, "Add vehicle  masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.VehicleMasterData.Edit, "Edit vehicle masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.VehicleMasterData.View, "View vehicle masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.VehicleMasterData.Delete, "Delete vehicle  masterdata", "MASTER DATA"));

            //masterdata:account                
            permissions.Add(new RoleClaimPermission(Permissions.AccountMasterData.Add, "Add Account  masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.AccountMasterData.Edit, "Edit Account masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.AccountMasterData.View, "View Account masterdata", "MASTER DATA"));
            permissions.Add(new RoleClaimPermission(Permissions.AccountMasterData.Delete, "Delete Account  masterdata", "MASTER DATA"));

            //setting
            permissions.Add(new RoleClaimPermission(Permissions.Menus.Setting, "Setting  menu", "SETTINGS"));
            permissions.Add(new RoleClaimPermission(Permissions.Setting.View, "View settings", "SETTINGS"));
            permissions.Add(new RoleClaimPermission(Permissions.Setting.Update, "Update settings", "SETTINGS"));

            //Reports
            permissions.Add(new RoleClaimPermission(Permissions.Menus.Report, "Report  menu", "REPORTS"));
            permissions.Add(new RoleClaimPermission(Permissions.Report.Dashboard, "Report Dashboard", "REPORTS"));
            permissions.Add(new RoleClaimPermission(Permissions.Report.ReportViewer, "Report Viewer", "REPORTS"));







            //invoicing

            permissions.Add(new RoleClaimPermission(Permissions.Menus.Invoice, "Invoicing menu", "INVOICING"));
            permissions.Add(new RoleClaimPermission(Permissions.Invoicing.Add, "Add Invoice", "INVOICING"));
            permissions.Add(new RoleClaimPermission(Permissions.Invoicing.Edit, "Edit Invoice", "INVOICING"));
            permissions.Add(new RoleClaimPermission(Permissions.Invoicing.View, "View Invoices", "INVOICING"));
            permissions.Add(new RoleClaimPermission(Permissions.Invoicing.Process, "Process Invoices", "INVOICING"));
            permissions.Add(new RoleClaimPermission(Permissions.Invoicing.Delete, "Delete Invoice", "INVOICING"));




            return permissions;
        }
    }
}
}

