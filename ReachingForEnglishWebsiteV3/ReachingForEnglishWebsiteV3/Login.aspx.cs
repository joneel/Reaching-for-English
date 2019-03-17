using System;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace ReachingForEnglishWebsiteV3
{
    public partial class Login : System.Web.UI.Page
    {
        private RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();
        private String dbUsername;
        private String dbSalt;
        private String dbPass;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["olduserType"] = "";
            Session["oldTid"] = "";
            Session["oldLid"] = "";
            Session["oldText"] = "";
            Session["ee"] = "";

            MySqlConnection connection = GetSqlConnection();

            try
            {
                connection.Open();

                String sql = "SELECT * FROM credentials";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dbUsername = (String)rdr[0];
                    dbSalt = (String)rdr[1];
                    dbPass = (String)rdr[2];
                }//end while
                rdr.Close();

            }//end try
            catch (MySqlException ex)
            {
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Response.Write("<script>alert('Cannot connect to server.  Contact administrator');</script>");
                        break;
                    case 1045:
                        Response.Write("<script>alert('Invalid username/password, please try again');</script>");
                        break;
                    default:
                        Response.Write("<script>alert('"+ex.Number+"');</script>");
                        break;
                }//end switch
            }//end catch

            connection.Close();

        }//end method
        #endregion Page Load
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
                Response.Write("<script>alert('" + e.Message + "');</script>");
                throw new ArgumentException();
            }
            return conn;
        }
        #endregion GetSQLConnection
        #region Submit Button
        protected void Submit(object sender, EventArgs e)
        {
            if (pass.Text == "" || pass.Text == null || user.Text == "" || user.Text == null)
                Response.Write("<script>alert('Must have all fields filled out.');</script>");
            else
            {
                String adminPassword = pass.Text;

                byte[] encryptedpasswordAdmin = GenerateSaltedHash(adminPassword);
                String fin = "";
                foreach (byte i in encryptedpasswordAdmin)
                    fin += i;
                String cred = String.Concat(fin, dbSalt);

                bool matchingpass = ComparePasswords(cred, dbPass);

                if (matchingpass && dbUsername == user.Text)
                {
                    Session["confirm"] = matchingpass;
                    Response.Redirect("Home.aspx");
                }//end if
                else
                    Response.Write("<script>alert('Wrong Username or Password, Please Try Again');</script>");
            }//end else
        }//end method
        #endregion Submit Button
        #region FileViewer Button
        protected void FileViewerButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("FileViewer.aspx", true);
        }
        #endregion FileViewer Button
        #region Helper Methods
        static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            System.Security.Cryptography.RNGCryptoServiceProvider.Create().GetBytes(salt);
            return salt;
        }//end method
        static byte[] GenerateSaltedHash(String password)
        {
            // Convert the plain string pwd into bytes
            byte[] passwordbytes = System.Text.UnicodeEncoding.Unicode.GetBytes(password);
            HashAlgorithm algorithm = new SHA512Managed();
            return algorithm.ComputeHash(passwordbytes);
        }//end method
        public static bool ComparePasswords(String str, String str2)
        {
            if (!str.Equals(str2))
                return false;
            return true;
        }//end method  
        private int RandomInteger(int min, int max)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);
                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }//end while
            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) * (scale / (double)uint.MaxValue));
        }//end method
        #endregion Helper Methods
    }
}