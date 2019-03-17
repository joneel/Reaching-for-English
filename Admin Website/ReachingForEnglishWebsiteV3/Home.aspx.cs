
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ReachingForEnglishWebsiteV3
{
    public partial class Home : System.Web.UI.Page
    {
        #region Load Page
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    GridLessons(GetSqlConnection());
                }
            }
        }
        #endregion Load Page
        #region GetConnectionString
        private string GetConnectionString()
        {
            string server = "mysql5012.site4now.net";
            string database = "db_a3acac_engdata";
            string uid = "db_a3acac_admin";
            string password = "Reaching4English";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; SslMode=none";

            return connectionString;
        }
        #endregion GetConnectionString
        #region GetSQLConnection
        public MySqlConnection GetSqlConnection()
        {
            string connectionString = GetConnectionString();
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('"+e.Message+"');</script>");
                throw new ArgumentException();
            }
            return conn;
        }
        #endregion GetSQLConnection
        #region Fill GridView
        private void GridLessons(MySqlConnection connection)
        {
            try
            {
                connection.Open();

                String sql = "SELECT userType,env,tid,lid,text,path,filename,ext FROM lessons ORDER BY userType,env,tid,lid ASC";

                MySqlCommand cmd = new MySqlCommand(sql, connection);

                DataTable dt = new DataTable();
                MySqlDataAdapter src = new MySqlDataAdapter(cmd);
                src.Fill(dt);
                gridLesson.DataSource = dt;
                gridLesson.DataBind();

            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('"+mse.Message+"');</script>");
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('"+e.Message+"');</script>");
            }

            connection.Close();
        }
        #endregion Fill GridView
        #region OnRowDataBound
        protected void Lesson_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Attributes.Add("style", "width:20%;");//Scripts
                e.Row.Cells[5].Attributes.Add("style", "width:10%;");//Filename
            }
        }
        #endregion OnRowDataBound
        #region OnRowEditing
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            DropDownList ddlUsers = (DropDownList)gridLesson.Rows[e.NewEditIndex].FindControl("UsersDDL");
            DropDownList ddlEnv = (DropDownList)gridLesson.Rows[e.NewEditIndex].FindControl("EnvDDL");
            Label user = (Label)gridLesson.Rows[e.NewEditIndex].FindControl("user");
            Label env = (Label)gridLesson.Rows[e.NewEditIndex].FindControl("env");
            Label tid = (Label)gridLesson.Rows[e.NewEditIndex].FindControl("tid");
            Label lid = (Label)gridLesson.Rows[e.NewEditIndex].FindControl("lid");
            Label text = (Label)gridLesson.Rows[e.NewEditIndex].FindControl("text");
            Session["oldUser"] = user.Text;
            Session["oldEnv"] = env.Text;
            Session["oldTid"] = tid.Text;
            Session["oldLid"] = lid.Text;
            Session["oldText"] = text.Text;
            Session["ext"] = gridLesson.Rows[e.NewEditIndex].Cells[6].Text;

            gridLesson.EditIndex = e.NewEditIndex;

            GridLessons(GetSqlConnection());
        }

        #endregion OnRowEditing
        #region OnRowUpdating
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridLesson.Rows[e.RowIndex];
            DropDownList ddlUsers = (DropDownList)gridLesson.Rows[e.RowIndex].FindControl("UsersDDL");
            DropDownList ddlEnv = (DropDownList)gridLesson.Rows[e.RowIndex].FindControl("EnvDDL");
            DropDownList ddlTid = (DropDownList)gridLesson.Rows[e.RowIndex].FindControl("TopicDDL");

            string oldUser = (string)Session["oldUser"];
            string oldEnv = (string)Session["oldEnv"];
            string oldTid = (string)Session["oldTid"];
            string oldLid = (string)Session["oldLid"];
            string oldText = (string)Session["oldText"];
            string ext = (string)Session["ext"];
            string newTid = ddlTid.SelectedValue.ToString();
            string newUser = ddlUsers.SelectedValue.ToString();
            string newEnv = ddlEnv.SelectedValue.ToString();
            string newLid = ((TextBox)row.FindControl("LessonTextBox")).Text;
            string newText = ((TextBox)row.FindControl("TextBox")).Text;

            if (newLid == "" || !Regex_LidTid(newLid))
            {
                Response.Write("<script>alert('Lesson cannot be empty and can only contain alphanumeric, [,-:], and spaces.');</script>");
                return;
            }

            MySqlConnection connection = null;
            try
            {
                connection = GetSqlConnection();

                connection.Open();
                String sql = "UPDATE IGNORE lessons SET userType=@newUser, env=@newEnv, tid=@newTid, lid=@newLid, text=@newText WHERE userType = @oldUser && env = @oldEnv && tid = @oldTid && lid = @oldLid && ext = @ext ";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@newUser", newUser);
                    cmd.Parameters.AddWithValue("@newEnv", newEnv);
                    cmd.Parameters.AddWithValue("@newTid", newTid);
                    cmd.Parameters.AddWithValue("@newLid", newLid);
                    cmd.Parameters.AddWithValue("@newText", newText);
                    cmd.Parameters.AddWithValue("@oldUser", oldUser);
                    cmd.Parameters.AddWithValue("@oldEnv", oldEnv);
                    cmd.Parameters.AddWithValue("@oldTid", oldTid);
                    cmd.Parameters.AddWithValue("@oldLid", oldLid);
                    cmd.Parameters.AddWithValue("@ext", ext);

                    cmd.ExecuteNonQuery();

                }//end cmd
            }//end try
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('"+mse.Message+"');</script>");
            }
            catch (ArgumentException ae)
            {
                Response.Write("<script>alert('"+ae.Message+"');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }

            connection.Close();

            Session["oldUser"] = "";
            Session["oldEnv"] = "";
            Session["oldTid"] = "";
            Session["oldLid"] = "";
            Session["oldText"] = "";
            Session["ext"] = "";

            gridLesson.EditIndex = -1;
            GridLessons(connection);
        }

        #endregion OnRowUpdating
        #region OnRowCancelingEdit
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            Session["oldUser"] = "";
            Session["oldEnv"] = "";
            Session["oldTid"] = "";
            Session["oldLid"] = "";
            Session["oldText"] = "";
            Session["ext"] = "";
            gridLesson.EditIndex = -1;
            GridLessons(GetSqlConnection());
        }

        #endregion OnRowCancelingEdit
        #region OnRowDeleting
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gridLesson.Rows[e.RowIndex];

            string user = "";
            string env = "";
            string tid = "";
            string lid = "";
            string ext = "";
            string filename = "";

            if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow)) //if in editing mode
            {
                DropDownList ddlUsers = (DropDownList)row.FindControl("UsersDDL");
                DropDownList ddlEnv = (DropDownList)row.FindControl("EnvDDL");
                DropDownList ddlTid = (DropDownList)row.FindControl("TopicDDL");

                lid = ((TextBox)row.FindControl("LessonTextBox")).Text;
                user = ddlUsers.SelectedValue.ToString();
                env = ddlEnv.SelectedValue.ToString();
                tid = ddlTid.SelectedValue.ToString();
            }
            else//not in editing mode
            {
                Label lblUser = (Label)row.FindControl("user");
                Label lblEnv = (Label)row.FindControl("env");
                Label lblTid = (Label)row.FindControl("tid");
                Label lblLid = (Label)row.FindControl("lid");

                user = lblUser.Text;
                env = lblEnv.Text;
                tid = lblTid.Text;
                lid = lblLid.Text;
            }

            ext = row.Cells[6].Text;
            filename = row.Cells[5].Text;
            MySqlConnection connection = null;

            try
            {
                connection = GetSqlConnection();

                connection.Open();

                String sqlDelete = "DELETE FROM lessons WHERE userType = @user && env = @env && tid = @tid && lid = @lid && ext = @ext ";

                using (MySqlCommand cmd = new MySqlCommand(sqlDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@env", env);
                    cmd.Parameters.AddWithValue("@tid", tid);
                    cmd.Parameters.AddWithValue("@lid", lid);
                    cmd.Parameters.AddWithValue("@ext", ext);

                    cmd.ExecuteNonQuery();
                }

                Int32 pathCount = -1;
                String sqlCount = "SELECT COUNT(tid) FROM lessons WHERE filename = @filename && ext = @ext";

                using (MySqlCommand cmd = new MySqlCommand(sqlCount, connection))
                {

                    cmd.Parameters.AddWithValue("@filename", filename);
                    cmd.Parameters.AddWithValue("@ext", ext);
                    pathCount = int.Parse(cmd.ExecuteScalar().ToString());
                }
                connection.Close();
                gridLesson.EditIndex = -1;
                GridLessons(connection);

                if (pathCount == 0)//no other lessons point to this file so delete file
                {
                    try
                    {
                        string physicalFolderPath = ext == "mp3" ? Server.MapPath(".//Audio//") : Server.MapPath(".//PDF//");
                        string physicalFilePath = physicalFolderPath + filename + "." + ext;

                        if (System.IO.File.Exists(physicalFilePath))
                        {
                            try
                            {
                                System.IO.File.Delete(physicalFilePath);
                            }
                            catch (Exception ex)
                            {
                                Response.Write("<script>alert('"+ex.Message+"');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('File does not exist');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('"+ex.Message+"');</script>");
                    }
                }
            }//end try
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('"+mse.Message+"');</script>");
            }
            catch (ArgumentException ae)
            {
                Response.Write("<script>alert('"+ae.Message+"');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
        }
        #endregion OnRowDeleting
        #region Add Lesson/Topic Delete Topic
        protected void AddTopicButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddTopic.aspx", true);
        }
        protected void AddLessonButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("SelectUser.aspx", true);
        }
        protected void DeleteLessonButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("DeleteTopic.aspx", true);
        }
        protected void SelectFileButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("SelectFile.aspx", true);
        }
        protected void FileViewerButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("FileViewer.aspx", true);
        }
        protected void AddDeleteSurveyButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddDeleteSurvey.aspx", true);
        }
        #endregion Add Lesson/Topic Delete Topic
        #region Get Grid Edit Drop Down Lists
        public List<string> FillUsersDDL()
        {
            List<string> userList = new List<string>();
            try
            {
                MySqlConnection connection = GetSqlConnection();
                connection.Open();
                String sql = "SELECT userType FROM user ORDER BY userType ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            userList.Add(rdr.GetString("userType"));
                        }//end while
                    }
                }

                connection.Close();
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('" + mse.Message + "');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return userList;
        }
        public List<string> FillEnvDDL()
        {
            List<string> envList = new List<string>();
            try
            {
                MySqlConnection connection = GetSqlConnection();
                connection.Open();
                String sql = "SELECT env FROM environment ORDER BY env ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            envList.Add(rdr.GetString("env"));
                        }//end while
                    }
                }

                connection.Close();
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('" + mse.Message + "');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return envList;
        }
        public List<string> FillTopicDDL()
        {
            List<string> tidList = new List<string>();
            try
            {
                MySqlConnection connection = GetSqlConnection();
                connection.Open();
                String sql = "SELECT tid FROM topics ORDER BY tid ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            tidList.Add(rdr.GetString("tid"));
                        }//end while
                    }
                }

                connection.Close();
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('" + mse.Message + "');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return tidList;
        }
        #endregion Get Grid Edit Drop Down Lists
        #region Regex
        public bool Regex_LidTid (string topic)
        {
            //String LidRegex = @"^([a-zA-Z0-9,\-:']{1}[ ]?){1,99}[a-zA-Z0-9]{1}$";
            //Anything but blank
            String LidRegex = @"^(?!\s*$).+";
            if (!Regex.IsMatch(topic, LidRegex))
                return false;
            return true;
        }
        public bool Regex_mp3(string topic)
        {
            //no new line, tab, form feed or spaces [^\s] and can contain alphanumeric, numerical, 
            //underscores and dashes and must end in ".mp3"
            String FilenameRegex = @"^[^\s][A-Za-z0-9_-]+\.(mp3|MP3)$";
            if (!Regex.IsMatch(topic, FilenameRegex))
                return false;
            return true;
        }
        public bool Regex_pdf(string topic)
        {
            //can contain alphanumeric, numerical, numbers, spaces, underscores and dashes
            //and must end in ".pdf"
            String FilenameRegex = @"^[A-Za-z0-9 _-]+\.(pdf|PDF)$";
            if (!Regex.IsMatch(topic, FilenameRegex))
                return false;
            return true;
        }

        #endregion Regex
    }
}