using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace inzPJATKSNM.Views
{
    public partial class AddMusic : System.Web.UI.Page
    {
        public static FileUpload fileuploadPom;
        String filePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AnulujButton_Click(object sender, EventArgs e)
        {

        }

        protected void ZapiszButton_Click(object sender, EventArgs e)
        {
            fileuploadPom = MusicFileUpload;
            if (fileuploadPom.HasFile)
            {
                
                    filePath = inzPJATKSNM.Controllers.AddMusicController.addMusic(fileuploadPom.PostedFile);
                
                

            }
            else
            {
                //error handling tu powinien byc 
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int dotIdx = args.Value.IndexOf('.');
            int len = args.Value.Length;
            string str = args.Value.ToString();
            string extension = str.Substring(dotIdx, len - dotIdx);

            if (extension.Equals("mp3") || extension.Equals("wav"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

     
    }
}