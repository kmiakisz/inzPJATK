using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using inzPJATKSNM.Models;

namespace inzPJATKSNM.Narodowosc
{
    public partial class Default : System.Web.UI.Page
    {
		protected inzPJATKSNM.Models.inzSNMEntities _db = new inzPJATKSNM.Models.inzSNMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of Narodowosc entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<inzPJATKSNM.Models.Narodowosc> GetData()
        {
            return _db.Narodowosc;
        }
    }
}

