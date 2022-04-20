using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileUploading
{
    public partial class FileUploading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        void UploadFile()
        {
            try
            {
                if (fuFileUpload.HasFile)
                {
                    //Check File Extension
                    string fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName);

                    if (fileExtension.ToLower() != ".doc" && fileExtension.ToLower() != ".docx" && fileExtension.ToLower() != ".xls" && fileExtension.ToLower() != ".xlsx")
                    {
                        lblMessage.Text = "Files with .doc, .docsx, .xls and .xlsx only allowed.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        //Check file size
                        int fileSize = fuFileUpload.PostedFile.ContentLength / 1024;

                        //Allow less than 3 MB only(1024*3)
                        if (fileSize > 3072)
                        {
                            lblMessage.Text = "File size should be less than 3 MB only.";
                        }
                        else
                        {
                            string folderPath = Server.MapPath("~/UploadedFiles/");

                            //Check whether Directory is available or not.
                            if (!Directory.Exists(folderPath))
                            {
                                //if directory is not available, then create a folder.
                                Directory.CreateDirectory(folderPath);
                            }

                            //save the file to folder
                            fuFileUpload.SaveAs(folderPath + Path.GetFileName(fuFileUpload.FileName));

                            //dispaly message after uploaded
                            lblMessage.Text = "File " + Path.GetFileName(fuFileUpload.FileName) + " uploaded successfully...!";
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please select a file to upload.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}