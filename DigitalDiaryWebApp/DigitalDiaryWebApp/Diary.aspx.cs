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
                if(!IsPostBack)
                {
                    User user = (User)Session["User"];
                    lblFullname.Text = "Welcome " + user.Fullname;
                    txtJournal.Visible = false;
                    txtEditJournal.Visible = false;
                    btnAddJournal.Visible = false;
                    btnEditJournal.Visible = false;
                    btnDeleteJournal.Visible = false;
                    lblMessage.Text = "";
                }
                lblMessage.Text = "";
            }

            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnAddJournal_Click(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            var diary = new Models.Diary();
            var feedbackMessages = diary.AddJournalEntry(calendarDiary.SelectedDate.Date.ToString("dd/MM/yyyy"),user.EmailId,txtEditJournal.Text);
            lblMessage.Text = "";

            foreach (var message in feedbackMessages)
            {
                lblMessage.Text += message;
            }
        }

        protected void calendarDiary_SelectionChanged(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            if (calendarDiary.SelectedDate.Date == DateTime.MinValue.Date)
            {
                lblMessage.Text = "Please select a date";
            }

            else
            {
                var diary = new Models.Diary();
                bool journalExists = diary.CheckJournalExistsForTheUser(calendarDiary.SelectedDate.Date.ToString("dd/MM/yyyy"), user.EmailId);

                // If journal entry exists
                if (journalExists)
                {
                    // execute code to show journal content for the specified date and user.
                    txtJournal.Visible = true;
                    btnAddJournal.Visible = false;
                    btnEditJournal.Visible = true;
                    btnDeleteJournal.Visible = true;
                }

                // If journal does not exist
                else
                {
                    // Show message saying no journal entry is added
                    txtJournal.Visible = false;
                    txtEditJournal.Visible = true;
                    btnAddJournal.Visible = true;
                    btnEditJournal.Visible = false;
                    btnDeleteJournal.Visible = false;
                }
            }
        }
    }
}