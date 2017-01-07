using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.PrivilegeModels
{
    public class UserPrivilege
    {
        public int id_user_priviledges { get; set; }
        public int usedId { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public int privilegeId { get; set; }
        public string privilegeName { get; set; }
    }
}