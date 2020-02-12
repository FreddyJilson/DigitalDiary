using DigitalDiaryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigitalDiaryWebApp
{
    public partial class Diary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] != null)
            {
                User user = (User)Session["User"];
                lblFullname.Text = "Welcome " + user.Fullname;
            }

            else
            {
                Response.Redirect("Home.aspx");
            }
        }
    }
}