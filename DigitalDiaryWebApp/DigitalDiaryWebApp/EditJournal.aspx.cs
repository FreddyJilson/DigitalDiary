using DigitalDiaryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.UI.HtmlControls;

namespace DigitalDiaryWebApp
{
    public partial class EditJournal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] != null && Session["JournalEntry"] != null)
            {
                User user = (User)Session["User"];
                lblFullname.Text = "Welcome " + user.Fullname + "!";
                
                
                Models.Diary journalEntry = (Models.Diary)Session["JournalEntry"];
                lblDate.Text = journalEntry.JournalDate;
                txtEditJournal.Text = journalEntry.Content;                
            }

            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["JournalEntry"] = null;
            Response.Redirect("Diary.aspx");
        }

        protected void btnConfirmEdit_Click(object sender, EventArgs e)
        {
            // Add code here to execute journal entry changes...
            
            txtEditJournal.Visible = false;
            btnConfirmEdit.Visible = false;
            btnCancel.Visible = true;
            btnCancel.Text = "Go back to my diary.";
            Session["JournalEntry"] = null;
            lblDate.Text = "The edited changes are now made. ";
        }

    }
}