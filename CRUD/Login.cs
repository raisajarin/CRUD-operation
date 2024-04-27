using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (emailBox.Text == "admin" && passwordBox.Text == "admin")
                {
                    new Admin().Show();
                    this.Hide();
                }
                else
                {
                    //1. Address of SQL Server and Database.
                    string connection = "Data Source=DESKTOP-N9N0BK6\\SQLEXPRESS01;Initial Catalog=CrudOperation;Integrated Security=True;";

                    //2. Establish Connection.
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        //3. Open Connection.
                        conn.Open();

                        //4. Prepare Query.
                        string email = emailBox.Text;
                        string password = passwordBox.Text;

                        string query = "SELECT COUNT(*) FROM userInfo WHERE (email = @email ) AND password = @password;";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@password", password);

                            int count = (int)cmd.ExecuteScalar();

                            if (count > 0)
                            {
                                new Afterlogin().Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect email or password. Please try again.");
                            }
                        }

                        //5. Close Connection.
                        conn.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void signup_label_Click(object sender, EventArgs e)
        {
            new Signup().Show();
            this.Hide();
        }
    }
}
