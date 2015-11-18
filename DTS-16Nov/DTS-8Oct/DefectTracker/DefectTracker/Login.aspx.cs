using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DefectTracker.DataSource;
using DefectTracker.Models;

namespace DefectTracker
{
    /// <summary>
    /// This Code is for LoginModule- develpoed By Prabasini
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        [WebMethod]
        public static HttpResponseMessage ChangePassword(object changePassword)
        {
            try 
            { 
                var data = GetPasswordInfo(changePassword);
           
                var user = UserDatasource.GetUserById(data.RegisteredUserName);
                if (user.UserId == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        ReasonPhrase = "LoginUser name does not exist in the database"
                    };
                    return response;
                }
                else
                {
                    if (user.Password == data.CurrentPassword)
                    {
                        UserDatasource.UpdatePwd(data.NewPassword, data.RegisteredUserName, "CP");
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            ReasonPhrase = "Current password is wrong"
                        };
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
                      
        }

        [WebMethod]
        public static HttpResponseMessage ForgotPassword(object forgotPassword)
        {
            try
            { 
                var data = GetFPInfo(forgotPassword);

                if (!UserDatasource.IsEmailExist(data.RegisteredEmail))
                {
                    //if there is error in server side use the below code.
                    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        ReasonPhrase = "Email Id does not exist in the database"
                    };
                    return response;
                }
                else
                {
                    string msgBody;
                    string pwd = UserDatasource.PasswordGenerator();
                    msgBody = "Your new Password : " + pwd;
                    Email.SendMail(data.RegisteredEmail,msgBody);
                    UserDatasource.UpdatePwd(pwd, data.RegisteredEmail,"FP");
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
               
                var role = UserDatasource.UserRole(txtUserName.Text);
                Session.Add("Role", role);
                //var role = "PM";
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkRememberMe.Checked);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = "ManageProject.aspx";

                    //if (role != "PM")
                    //    returnUrl = "ManageDefects.aspx";//need to add this page

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

            if (this.IsAlphaNumeric(userName) && userName.Length <= 50 && password.Length <= 50)
            {               
                returnValue = UserDatasource.ValidateUser(userName, password);
            }
            else
            {
                lblmsg.InnerText = "user name not alpha-numeric or exceed the length !";
                lblmsg.Visible = true;
            }

            return returnValue;
        }
        private static Models.ChangePassword GetPasswordInfo(object changepassword)
        {
            var objPwd= new Models.ChangePassword();
            var tmp = (Dictionary<string, object>)changepassword;

            object objEmail= null;
            object objCPwd= null;
            object objNPwd = null;
            object objConfPwd = null;

            if (tmp.TryGetValue("RegisteredUserName", out objEmail))
                objPwd.RegisteredUserName = objEmail.ToString();

            if (tmp.TryGetValue("CurrentPassword", out objCPwd))
                objPwd.CurrentPassword = objCPwd.ToString();

            if (tmp.TryGetValue("NewPassword", out objNPwd))
                objPwd.NewPassword = objNPwd.ToString();

            if (tmp.TryGetValue("ConfirmPassword", out objConfPwd))
                objPwd.ConfirmPassword = objConfPwd.ToString();

            return objPwd;
        }
        private static ForgotPassword GetFPInfo(object forgotPwd)
        {
            var objPwd = new ForgotPassword();
            var tmp = (Dictionary<string, object>)forgotPwd;
            object objEmail = null;

            if (tmp.TryGetValue("RegisteredEmail", out objEmail))
                objPwd.RegisteredEmail = objEmail.ToString();

            return objPwd;
        }
    }
}