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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void show_button_Click(object sender, EventArgs e)
        {
            try
            {

                //1. Address of SQL Server and Database.
                string connection = "Data Source=DESKTOP-N9N0BK6\\SQLEXPRESS01;Initial Catalog=CrudOperation;Integrated Security=True;";

                //2. Establish Connection.
                SqlConnection conn = new SqlConnection(connection);

                //3. Open Connection.
                conn.Open();

                //4. Prepare Query.
                string query = "select * from userInfo";

                //5. Execute.
                SqlCommand cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                dataGridView1.DataSource = table;


                //6. Close Connection.
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void fetch_button_Click(object sender, EventArgs e)
        {
            try
            {

                //1. Address of SQL Server and Database.
                string connection = "Data Source=DESKTOP-N9N0BK6\\SQLEXPRESS01;Initial Catalog=CrudOperation;Integrated Security=True;";

                //2. Establish Connection.
                SqlConnection conn = new SqlConnection(connection);

                //3. Open Connection.
                conn.Open();

                //4. Prepare Query.
                string userID = idBox.Text;

                string query = "SELECT * FROM userInfo WHERE userID = " + userID;
                //5. Execute.
                SqlCommand cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    nameBox.Text = reader["fullName"].ToString();
                    emailBox.Text = reader["Email"].ToString();
                    passwordBox.Text = reader["Password"].ToString();
                }
                else
                {
                    MessageBox.Show("No data found.");
                }

                //6. Close Connection.
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Address of SQL Server and Database.
                string connection = "Data Source=DESKTOP-N9N0BK6\\SQLEXPRESS01;Initial Catalog=CrudOperation;Integrated Security=True;";

                // Establish Connection.
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Open Connection.
                    conn.Open();

                    // Prepare Query.
                    string fullName = nameBox.Text;
                    string email = emailBox.Text;
                    string password = passwordBox.Text;
                    

                    // Build the query string.
                    string query = @"UPDATE userInfo SET  fullName = @FullName, email = @Email, password = @Password WHERE userID = @UserID;";

                    // Execute Query.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                       
                        cmd.Parameters.AddWithValue("@UserID", idBox.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No record updated. ");
                        }
                    }

                    // Close Connection.
                    conn.Close();

                    // Clear text boxes.
                    nameBox.Text = "";
                    emailBox.Text = "";
                    passwordBox.Text = "";
                    idBox.Text = "";
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            try
            {

                //1. Address of SQL Server and Database.
                string connection = "Data Source=DESKTOP-N9N0BK6\\SQLEXPRESS01;Initial Catalog=CrudOperation;Integrated Security=True;";

                // Establish Connection.
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Open Connection.
                    conn.Open();

                    // Prepare Query.
                    string userID = idBox.Text;
                    string query = @"DELETE FROM userInfo WHERE userID = @UserID;";

                    // Execute Query.
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No record deleted. ");
                        }
                    }

                    // Close Connection.
                    conn.Close();

                    nameBox.Text = "";
                    emailBox.Text = "";
                    passwordBox.Text = "";
                    idBox.Text = "";
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
