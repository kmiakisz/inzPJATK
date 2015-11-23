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
    public partial class Edit : System.Web.UI.Page
    {
		protected inzPJATKSNM.Models.inzSNMEntities _db = new inzPJATKSNM.Models.inzSNMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Update methd to update the selected Narodowosc item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(int  Id_Narod)
        {
            using (_db)
            {
                var item = _db.Narodowosc.Find(Id_Narod);

                if (item == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", Id_Narod));
                    return;
                }

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes here
                    _db.SaveChanges();
                    Response.Redirect("../Default");
                }
            }
        }

        // This is the Select method to selects a single Narodowosc item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public inzPJATKSNM.Models.Narodowosc GetItem([FriendlyUrlSegmentsAttribute(0)]int? Id_Narod)
        {
            if (Id_Narod == null)
            {
                return null;
            }

            using (_db)
            {
                return _db.Narodowosc.Find(Id_Narod);
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
