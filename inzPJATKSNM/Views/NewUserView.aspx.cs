using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.AuthModels;

namespace inzPJATKSNM.Views
{
    public partial class NewUserView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (System.Web.HttpContext.Current.User.Identity.Name != null)
            {
                inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(System.Web.HttpContext.Current.User.Identity.Name);
                if (user.rola.roleId != 1)
                {
                    Response.Redirect("LoginView.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginView.aspx");
            }
             
           
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            Rola role = new Rola();
            user.login = EmailTxt.Text;
            user.imie = NameTxt.Text;
            user.nazwisko = SurnameTxt.Text;
            role.roleId =Int32.Parse(RoleDDL.SelectedValue);
            user.rola = role;
            try
            {
                inzPJATKSNM.Controllers.AuthenticationController.saveUser(user);
            }
            catch (Exception ex)
            {
                Response.Redirect("NewUserView.aspx?err=" + ex);
            }

        }
    }
}