using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using inzPJATKSNM.Models;

namespace inzPJATKSNM.Narodowosc
{
    public partial class Delete : System.Web.UI.Page
    {
		protected inzPJATKSNM.Models.inzSNMEntities _db = new inzPJATKSNM.Models.inzSNMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Delete methd to delete the selected Narodowosc item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int Id_Narod)
        {
            using (_db)
            {
                var item = _db.Narodowosc.Find(Id_Narod);

                if (item != null)
                {
                    _db.Narodowosc.Remove(item);
                    _db.SaveChanges();
                }
            }
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single Narodowosc item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public inzPJATKSNM.Models.Narodowosc GetItem([FriendlyUrlSegmentsAttribute(0)]int? Id_Narod)
        {
            if (Id_Narod == null)
            {
                return null;
            }

            using (_db)
            {
	            return _db.Narodowosc.Where(m => m.Id_Narod == Id_Narod).FirstOrDefault();
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("../Default");
            }
        }
    }
}

