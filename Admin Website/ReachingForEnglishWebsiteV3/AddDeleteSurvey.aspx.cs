using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class AddDeleteSurvey : System.Web.UI.Page
    {
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else if(!IsPostBack)
                GetSurveyDDL(SqlConn.GetSqlConnection());
        }
        #region Fill Survey Drop Down List
        private void GetSurveyDDL(MySqlConnection connection)
        {
            try
            {
                SurveyDDL.Items.Add("Select a survey...");
                connection.Open();
                String sql = "SELECT name FROM survey_url ORDER BY name ASC";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            SurveyDDL.Items.Add(rdr.GetString("name"));
                        }//end while
                    }
                }
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('" + mse.Message + "');</script>");
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "');</script>");
            }

            connection.Close();
        }
        #endregion Fill Survey Drop Down List
        #region Enter Survey
        protected void SubmitAdd(object sender, EventArgs e)
        {
            if (!SqlConn.Regex_LidTid(SurveyURL.Text) && !SqlConn.Regex_LidTid(SurveyName.Text))
            {
                Response.Write("<script>alert('Neither URL or name can be blank.');</script>");
                return;
            }
            else
            {
                MySqlConnection connection = null;
                try
                {
                    if (SurveyURL.Text.Contains(":"))
                    {
                        Response.Write("<script>alert('URL must exclude \"https://\" or \"http://\" part of the URL');</script>");
                        return;
                    }
                    connection = SqlConn.GetSqlConnection();

                    connection.Open();

                    String sql = "INSERT INTO survey_url (url, name) VALUES (@url, @name)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@url", SurveyURL.Text);
                        cmd.Parameters.AddWithValue("@name", SurveyName.Text);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                    Response.Write("<script>alert('Survey Link Added.');</script>");
                }
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
        }
        #endregion Enter Survey
        #region Delete Survey
        protected void SubmitDelete(object sender, EventArgs e)
        {
            MySqlConnection connection = null;

            try
            {
                connection = SqlConn.GetSqlConnection();

                connection.Open();

                String sqlDelete = "DELETE FROM survey_url WHERE name = @name";
                using (MySqlCommand cmd = new MySqlCommand(sqlDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@name", SurveyDDL.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                }           
                connection.Close();
                Response.Write("<script>alert('Survey Link Deleted.');</script>");
            }
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
        #endregion Delete Survey
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}