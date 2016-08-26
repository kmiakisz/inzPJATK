using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class AddNewPhoto : System.Web.UI.Page
    {
        public static FileUpload fileupload2;
        String filePath = "";
        int technikaId,kategoriaId,autorId;
        String errorMessage = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
         
        }

        public void UploadButton_Click(object sender, EventArgs e)
        {
           
            
            fileupload2 = FileUpload1;
            if (fileupload2.HasFile)
            {
                try
                {
                    filePath = inzPJATKSNM.Controllers.AddPhotoController.addPhoto(fileupload2.PostedFile);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    Response.Redirect("ShowSurveys.aspx"+"?val="+errorMessage);
                }
                    
                
            }else{
                StatusLabel.Text="Nie wybrano żadnego pliku!!! .....";
            }
            technikaId = int.Parse(TechnikaDropDownList.SelectedValue);
            kategoriaId = int.Parse(KategoriaDropDownList.SelectedValue);
            autorId = int.Parse(AutorDropDownList.SelectedValue);
            try
            {
                inzPJATKSNM.Controllers.AddPhotoController.storePhotoToDb(filePath, technikaId, kategoriaId, autorId);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                Response.Redirect("ShowSurveys.aspx" + "?val=" + errorMessage);
            }
        
        }
       

     }
}
