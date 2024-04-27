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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }
        private bool EmailExists(SqlConnection conn, string email)
        {
            string query = "SELECT COUNT(*) FROM userInfo WHERE email = @Email;";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        private void signin_button_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(emailBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Text))
                {
                    MessageBox.Show("Please fill in all the required fields.");
                    return;
                }

                //1. Address of SQL Server and Database.
                string connection = "Data Source=DESKTOP-N9N0BK6\\SQLEXPRESS01;Initial Catalog=CrudOperation;Integrated Security=True;";

                //2. Establish Connection.
                SqlConnection conn = new SqlConnection(connection);

                //3. Open Connection.
                conn.Open();

                //4. Duplicate Emailchecking.
                string email = emailBox.Text;

                if (EmailExists(conn, email))
                {
                    MessageBox.Show("Email already exists.");
                    return;
                }

                //5. Prepare Query.
                string name = nameBox.Text;
                string password = passwordBox.Text;

                string query = "INSERT INTO userInfo (fullName, email, password) VALUES ('" + name + "', '" + email + "', '" + password + "');;";

                // 6. Execute Query with parameters
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fullName", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                cmd.ExecuteNonQuery();

                //7. Close Connection.
                conn.Close();

                MessageBox.Show("Congratulations, your account has been successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       

        private void login_label_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }
    }
}
