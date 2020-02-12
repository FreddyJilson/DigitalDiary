using DigitalDiaryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigitalDiaryWebApp
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if(Session["User"] != null)
            {
                User user = (User)Session["User"];
                lblErrorMessage.Text = "You are logged in as " + user.Fullname + ". Please logout and then sign up.";                
            }

            else
            {
                Response.Redirect("Register.aspx");
            }
        }
    }
}