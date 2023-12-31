﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmplyoeePortal
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LV8TTKU\SQLEXPRESS;Initial Catalog=StudentPortal;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignupBtn_Click(object sender, EventArgs e)
        {
            // check if passwords match
            if (passwordBox.Text.Equals(passwordConfirmBox.Text))
            {
                try
                {
                    String sqlQuery = "EXEC Create_Employee @username, @password, @fullname, @email, @phone, @date_of_birth, @level_of_education";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    cmd.Parameters.AddWithValue("@username", usernameBox.Text);
                    cmd.Parameters.AddWithValue("@password", passwordBox.Text);
                    cmd.Parameters.AddWithValue("@fullname", fullnameBox.Text);
                    cmd.Parameters.AddWithValue("@email", emailBox.Text);
                    cmd.Parameters.AddWithValue("@phone", phoneBox.Text);
                    cmd.Parameters.AddWithValue("@date_of_birth", dateOfBirthBox.Text);
                    cmd.Parameters.AddWithValue("@level_of_education", levelOfEducationList.SelectedValue.ToString());
                   
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        MessageLbl.Text = "Account created. You can Login now";
                    }
                    else
                    {
                        MessageLbl.Text = "Account not created.";
                    }

                    // close connection
                    con.Close();

                }
                catch (SqlException ex)
                {
                    MessageLbl.Text = "There's a problem: " + ex.Message.ToString();
                }
            }
            else
            {
                MessageLbl.Text = "Password don't match";
            }
        }
    }
}