using System;
using System.Web;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace ReachingForEnglishWebsiteV3
{
    public partial class ReplaceFile : System.Web.UI.Page
    {
        private const int FILE_SIZE_MAX = 15360000;//(15 MB)
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)//if not logged in, return to login page
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
        }
        #region Choose File
        int SaveFile(string physicalFilePath, string oldFile)
        {
            try
            {
                FileUpload.SaveAs(physicalFilePath);//will overwrite existing file with same name hence replace the file
                if (FileUpload.FileName.Equals(oldFile))
                    Response.Write("<script>alert('File Updated Successfully');</script>");
                return 1;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return 0;
        }
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
                        Response.Write("<script>alert('pdf name must only contain letters, numbers, spaces, dashes and underscores');</script>");
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
                String oldFile = (string)Session["file"];

                if (!(SaveFile(physicalFilePath, oldFile) > 0))//if not successfully saved
                {
                    Response.Write("<script>alert('Error saving file.');</script>");
                    return;
                }
                if (!FileUpload.FileName.Equals(oldFile))//if the new file name is different than the old file name
                {
                    try//delete old file
                    {
                        String oldFileExt = oldFile.ToString().Substring(oldFile.ToString().IndexOf(".")+1).Trim();
                        if (oldFileExt == "mp3")
                            System.IO.File.Delete(Server.MapPath(".//Audio//") + oldFile);
                        else if (oldFileExt == "pdf")
                            System.IO.File.Delete(Server.MapPath(".//PDF//") + oldFile);
                        else
                            Response.Write("<script>alert('Error deleting old file, extention is not pdf or mp3');</script>");
                    }
                    catch (System.IO.IOException exc)
                    {
                        Response.Write("<script>alert('ERROR: " + exc.Message + "');</script>");
                    }
                    try//update lessons table path, filename and extention
                    {
                        MySqlConnection connection = null;
                        connection = SqlConn.GetSqlConnection();
                        connection.Open();
                        String filename = oldFile.ToString().Substring(0, oldFile.ToString().IndexOf(".")).Trim();//take off extention

                        String sql = "UPDATE lessons SET path = @path, filename = @fn, ext = @ext WHERE filename= @filename";

                        using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@filename", filename);
                            cmd.Parameters.AddWithValue("@path", virtualFilePath);
                            cmd.Parameters.AddWithValue("@fn", FileUpload.FileName.ToString().Substring(0, FileUpload.FileName.ToString().IndexOf(".")).Trim());
                            cmd.Parameters.AddWithValue("@ext", ext);

                            int a = cmd.ExecuteNonQuery();
                            if(a>0)
                                Response.Write("<script>alert('Table And File Updated Successfully');</script>");
                            else
                                Response.Write("<script>alert('Table Not Updated Successfully');</script>");
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
                }
                Session["file"] = "";
                Server.Transfer("Home.aspx", true);
            }
            else
                Response.Write("<script>alert('Must Choose A File');</script>");
        }

        protected void Cancel(object sender, EventArgs e)
        {
            Session["file"] = "";
            Server.Transfer("Home.aspx", true);
        }
        #endregion Choose File
    }
}