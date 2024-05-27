using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.UserEntities
{
    public class Permissions
    {
        public class Menus
        {
            public const string UserManagement = "menus.usermanagement";
            public const string Setting = "menus.setting";
            public const string MasterData = "menus.masterdata";
            public const string Report = "menus.report";
            public const string Invoice = "menus.invoicing";
            public const string AppTask = "menus.apptask";
            public const string Assessment = "menus.assessment";



        }

        public class Users
        {
            public const string Activate = "users.activate";
            public const string Edit = "users.edit";
            public const string View = "users.view";
            public const string Roles = "users.roles";
            public const string Permissiion = "users.permission";
        }

        public class Setting
        {
            public const string Update = "setting.update";
            public const string View = "setting.view";


        }
        public class Report
        {
            public const string Dashboard = "report.dashboard";
            public const string ReportViewer = "report.viewer";

        }
        public class MasterData
        {
            public const string Add = "md.add";
            public const string Edit = "md.edit";
            public const string View = "md.view";
            public const string Delete = "md.delete";

        }
        public class VehicleMasterData
        {
            public const string Add = "mdv.add";
            public const string Edit = "mdv.edit";
            public const string View = "mdv.view";
            public const string Delete = "mdv.delete";

        }
        public class AccountMasterData
        {
            public const string Add = "mda.add";
            public const string Edit = "mda.edit";
            public const string View = "mda.view";
            public const string Delete = "mda.delete";

        }

        public class AppTaskPermision
        {
            public const string Add = "at.add";
            public const string Edit = "at.edit";
            public const string View = "at.view";
            public const string Delete = "at.delete";

        }
        public class AssessmentPermision
        {
            public const string Add = "assm.add";
            public const string Edit = "assm.edit";
            public const string View = "assm.view";
            public const string Delete = "assm.delete";

        }
        public class Invoicing
        {
            public const string Add = "inv.add";
            public const string Edit = "inv.edit";
            public const string View = "inv.view";
            public const string Process = "inv.process";
            public const string Delete = "inv.delete";


        }



    }
}
