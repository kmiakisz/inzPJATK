using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.Controllers;

namespace inzPJATKSNM.Views
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        inzPJATKSNM.AuthModels.User user;
        int userId;
        List<Int32> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            if (Session["token"] != null)
            {
                string username = inzPJATKSNM.Controllers.AuthenticationController.getLogin((string)HttpContext.Current.Session["token"]);
                user = inzPJATKSNM.Controllers.AuthenticationController.getUser(username);
                list = inzPJATKSNM.Controllers.UserController.GetUserPrivilegeListIdPerUserId(user.userId);
                if (user.rola.roleId == 1)
                {
                    HiddenField1.Value = user.userId.ToString();
                    if (!list.Contains(7))
                    {
                        NewUserBtn.Visible = false;
                    }
                    else
                    {
                        NewUserBtn.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }

        }

        protected void NewUserBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewUserView.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }
        protected void BckBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministratorPanel.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            userId = Int32.Parse(row.Cells[1].Text);
            Response.Redirect("ManagePrivileges.aspx?id=" + userId);            
        }
        protected override void InitializeCulture()
        {
            HttpCookie cookie = Request.Cookies["CultureInfo"];

            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value); ;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pl-PL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            }

            base.InitializeCulture();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!list.Contains(12))
                {
                    ((System.Web.UI.Control)e.Row.Cells[0].Controls[1]).Visible = false;
                }
                else
                {
                    ((System.Web.UI.Control)e.Row.Cells[0].Controls[1]).Visible = true;
                }
            }
        } 
    }
}