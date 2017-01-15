using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using inzPJATKSNM.Models;
using inzPJATKSNM.Controllers;
using inzPJATKSNM.PrivilegeModels;

namespace inzPJATKSNM.Views
{
    public partial class AddNewPhoto : System.Web.UI.Page
    {
        public static FileUpload fileupload2;
        String filePath = "";
        String title = "";
        int technikaId, kategoriaId, autorId;
        String errorMessage = "";
        int loggedId;
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
                inzPJATKSNM.AuthModels.User user = inzPJATKSNM.Controllers.AuthenticationController.getUser(username);
                loggedId = user.userId;
                List<Int32> list = UserController.GetUserPrivilegeListIdPerUserId(loggedId);
                if(!list.Contains(2))
                {
                    Response.Redirect("AdministratorPanel.aspx");
                }
            }
            else
            {
                Response.Redirect("LogInView.aspx");
            }

            if (Request.QueryString["err"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenModal();", true);
            }

        }

        public void UploadButton_Click(object sender, EventArgs e)
        {
            fileupload2 = FileUpload1;
            if (fileupload2.HasFile)
            {
                try
                {
                    filePath = inzPJATKSNM.Controllers.AddPhotoController.addPhoto(fileupload2.PostedFile);
                    //inzPJATKSNM.Controllers.AddPhotoController.addPhoto(fileupload2.PostedFile);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    Response.Redirect("AddNewPhoto.aspx" + "?err=" + errorMessage);
                }

                technikaId = int.Parse(TechnikaDropDownList.SelectedValue);
                kategoriaId = int.Parse(KategoriaDropDownList.SelectedValue);
                string authorSelItems = AutorDropDownList.SelectedItem.Text;
                List<String> authList = inzPJATKSNM.Controllers.AddPhotoController.TrimAuthor(authorSelItems);
                string name = authList[0];
                string surname = authList[1];
                autorId = inzPJATKSNM.Controllers.AddPhotoController.getIdByNameAndSurname(name, surname);
                title = NazwaTextBox.Text.ToString();

                try
                {
                    string ifPhotoExists = inzPJATKSNM.Controllers.AddPhotoController.CheckPhoto(title,filePath);
                    if (ifPhotoExists.Equals("exists"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "failOpenPhotoModal();", true);
                        NazwaTextBox.Text = "";
                        fileupload2.Attributes.Clear(); 
                    }
                    else
                    {
                        inzPJATKSNM.Controllers.AddPhotoController.storePhotoToDb(filePath, technikaId, kategoriaId, autorId, title);
                        NazwaTextBox.Text = "";
                        fileupload2.Attributes.Clear();
                        Response.Redirect("AdministratorPanel.aspx");
                    }
                    

                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    Response.Redirect("AddNewPhoto.aspx" + "?err=" + errorMessage);
                }
            }
            else
            {
                StatusLabel.Text = "Nie wybrano żadnego pliku!!! .....";
                Response.Redirect("AddNewPhoto.aspx" + "?err=" + "Nie udało się dodać zdjęcia! Spróbuj jeszcze raz!");
            }
            //Response.Redirect("ShowSurveys.aspx");

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int dotIdx = args.Value.IndexOf('.');
            int len = args.Value.Length;
            string str = args.Value.ToString();
            string extension = str.Substring(dotIdx + 1, len - dotIdx - 1);
            extension = extension.ToLower();

            if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("png") || extension.Equals("bmp") || extension.Equals("psd"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
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


    }
}
