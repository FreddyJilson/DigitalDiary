using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DigitalDiaryWebApp.Models;

namespace DigitalDiaryWebApp
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new User();
            var feedbackMessages = user.CreateUser(txtEmail.Text, txtUsername.Text, txtPassword.Text, txtFullname.Text);
            lblErrorMessage.Text = "";

            foreach (var message in feedbackMessages)
            {
                lblErrorMessage.Text += message + "<br>"; 
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}