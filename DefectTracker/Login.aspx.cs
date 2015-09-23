using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// This Code is for LoginModule- develpoed By Prabasini
/// </summary>
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// This event fire on click of login. It checks user credential and redirect user to respective page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool authenticated = this.ValidateCredentials(txtUserName.Text, txtPWD.Text);

        if (authenticated)
        {
            var returnUrl = Request.QueryString["ReturnURL"];
            var role = "PM";
            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkRememberMe.Checked);

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "ManageProjects.aspx";

                if (role != "PM")
                    returnUrl = "ManageDefects.aspx";//need to add this page

                Response.Redirect(returnUrl);
            }

        }
        else
        {
            lblmsg.InnerText = "Invalid user name and password !";
            lblmsg.Visible = true;
        }
    }
    public bool IsAlphaNumeric(string text)
    {
        return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
    }

    private bool ValidateCredentials(string userName, string password)
    {

        bool returnValue = false;

        if (userName == password)
        {
            returnValue = true;
        }

        //if (this.IsAlphaNumeric(userName) && userName.Length <= 50 && password.Length <= 50)
        //{
        //    SqlConnection conn = null;

        //    try
        //    {
        //        string sql = "select count(*) from users where username = @username and password = @password";

        //        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["defecttrack_data"].ConnectionString);
        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //        SqlParameter user = new SqlParameter();
        //        user.ParameterName = "@username";
        //        user.Value = userName.Trim();
        //        cmd.Parameters.Add(user);

        //        SqlParameter pass = new SqlParameter();
        //        pass.ParameterName = "@password";
        //        pass.Value = Hasher.HashString(password.Trim());
        //        cmd.Parameters.Add(pass);

        //        conn.Open();

        //        int count = (int)cmd.ExecuteScalar();

        //        if (count > 0) returnValue = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log your error
        //    }
        //    finally
        //    {
        //        if (conn != null) conn.Close();
        //    }
        //}
        //else
        //{
        //    // Log error - user name not alpha-numeric or 
        //    // username or password exceed the length limit!
        //}

        return returnValue;
    }
}