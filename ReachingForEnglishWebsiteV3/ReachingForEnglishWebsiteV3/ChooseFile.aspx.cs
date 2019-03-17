using System;
using System.Web;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class ChooseFile : System.Web.UI.Page
    {
        private const int FILE_SIZE_MAX = 15360000;//(15 MB)
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
        }
            #region Choose File
            protected void Submit(object sender, EventArgs e)
        {
            if (FileUpload.PostedFile.ContentLength > FILE_SIZE_MAX)
            {
                Response.Write("<script>alert('File Size Must Be Less Than 15MB.');</script>");
                return;
            }

            String ext = "";
            HttpPostedFile file = FileUpload.PostedFile;
            if ((file != null) && (file.ContentLength > 0))
            {
                if (FileUpload.PostedFile.ContentType == "audio/mp3")
                {
                    ext = "mp3";
                    if (!SqlConn.Regex_mp3(FileUpload.FileName))
                    {
                        Response.Write("<script>alert('mp3 name cannot contain spaces and must only have letters, numbers, dashes and underscores.');</script>");
                        return;
                    }
                }
                else if (FileUpload.PostedFile.ContentType == "application/pdf")
                {
                    ext = "pdf";
                    if (!SqlConn.Regex_pdf(FileUpload.FileName))
                    {
                        Response.Write("<script>alert('pdf name must only contain letters, numbers, spaces, underscores and dashes');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only PDF's and MP3 files are accepted.');</script>");
                    return;
                }

                String physicalFolderPath = ext == "mp3" ? Server.MapPath(".//Audio//") : Server.MapPath(".//PDF//");
                String physicalFilePath = physicalFolderPath + FileUpload.FileName;
                String virtualCurrentPath = HttpContext.Current.Request.Url.AbsoluteUri;
                String virtualAudioPath = virtualCurrentPath.Substring(0, virtualCurrentPath.LastIndexOf('/')) + (ext == "mp3" ? "/Audio/" : "/PDF/");
                String virtualFilePath = virtualAudioPath + FileUpload.FileName;
                if (!System.IO.File.Exists(physicalFilePath))
                {
                    try
                    {
                        FileUpload.SaveAs(physicalFilePath);
                        Response.Write("<script>alert('File Uploaded');</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('"+ex.Message+"');</script>");
                        return;
                    }
                }
                else
                    Response.Write("<script>alert('File already exists. If you would like to replace the file please click \"Replace File\" button on home page');</script>");
                try
                {
                    MySqlConnection connection = null;
                    connection = SqlConn.GetSqlConnection();
                    connection.Open();

                    String sql = "INSERT INTO lessons(userType, env, tid, lid, text, path, filename, ext) VALUES(@userType,@env,@tid,@lid,@text,@path,@fn,@ext)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@userType", Session["user"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@env", Session["env"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@tid", Session["tid"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@lid", Session["lid"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@text", Session["text"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@path", virtualFilePath);
                        cmd.Parameters.AddWithValue("@fn", FileUpload.FileName.ToString().Substring(0, FileUpload.FileName.ToString().IndexOf(".")).Trim());
                        cmd.Parameters.AddWithValue("@ext", ext.Trim());

                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }//end try
                catch (MySqlException mse)
                {
                    Response.Write("<script>alert('" + mse.Message + "');</script>");
                }
                catch (ArgumentException ae)
                {
                    Response.Write("<script>alert('" + ae.Message + "');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

                Server.Transfer("Home.aspx", true);
            }
            else
                Response.Write("<script>alert('Must Choose A File');</script>");
        }

        protected void Cancel(object sender, EventArgs e)
        {
            Session["oldUser"] = "";
            Session["oldEnv"] = "";
            Session["oldTid"] = "";
            Session["oldLid"] = "";
            Session["oldText"] = "";
            Session["ext"] = "";
            Server.Transfer("Home.aspx", true);
        }
        #endregion Choose File
    }
}