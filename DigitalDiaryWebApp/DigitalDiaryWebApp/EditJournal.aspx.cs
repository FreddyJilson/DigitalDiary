using DigitalDiaryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;


namespace DigitalDiaryWebApp
{
    public partial class EditJournal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] != null && Session["JournalEntry"] != null)
            {
                if(!IsPostBack)
                {
                    User user = (User)Session["User"];
                    lblFullname.Text = "Welcome " + user.Fullname + "!";


                    Models.Diary journalEntry = (Models.Diary)Session["JournalEntry"];
                    txtEditJournal.Text = journalEntry.Content;
                    lblDate.Text = "Date (dd/mm/yyyy): " + journalEntry.JournalDate;
                }                                          
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
            Models.Diary journalEntry = (Models.Diary)Session["JournalEntry"];
            User user = (User)Session["User"];
            Models.Diary myDiary = new Models.Diary();
            
            // Execute code for editing journal entry if journal content is between 100 and 8000 characters in length.
            if(myDiary.ValidateJournalContent(txtEditJournal.Text))
            {
                myDiary.EditJournalEntry(user.EmailId, journalEntry.JournalDate, txtEditJournal.Text);
                // After performing the execution of edit method for journal entry changes, 
                // show confirmation message and remove visibility of all buttons except for home button.
                txtEditJournal.Visible = false;
                btnConfirmEdit.Visible = false;
                btnCancel.Visible = true;
                btnCancel.Text = "Go back to my diary.";
                Session["JournalEntry"] = null;
                lblDate.Text = "The edited changes are now made. ";
            }

            // Show error message if content is less than 100 characters or more thn 8000 characters. 
            else
            {
                lblDate.Text = "Invalid journal entry length. Please type journal entry of atleast 100 characters and less than 8000 characters.";
            }
        }
    }
}