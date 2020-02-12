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
                    lblFullname.Text = "Welcome " + user.Fullname + "!";
                                   
                    txtJournal.Visible = false;
                    txtEditJournal.Visible = false;
                    btnAddJournal.Visible = false;
                    btnEditJournal.Visible = false;
                    btnDeleteJournal.Visible = false;
                    lblMessage.Text = "";
                    
                    ValidateVisibilityOfControlsAndShowJournalEntries();                  
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

            if(feedbackMessages.Count == 1)
            {
                foreach (var message in feedbackMessages)
                {
                    if (message.Equals("* Please enter valid journal entry of atleast 100 characters and not more than 8000 characters"))
                    {
                        txtEditJournal.Visible = true;
                        btnAddJournal.Visible = true;
                        txtJournal.Visible = false;
                        btnEditJournal.Visible = false;
                        btnDeleteJournal.Visible = false;
                    }

                    else
                    {
                        txtEditJournal.Text = "Type here to add your journal entry...";
                        txtEditJournal.Visible = false;
                        btnAddJournal.Visible = false;
                        txtJournal.Visible = false;
                        btnEditJournal.Visible = false;
                        btnDeleteJournal.Visible = false;
                    }
                }
                
            }                          
        }

        protected void calendarDiary_SelectionChanged(object sender, EventArgs e)
        {
            ValidateVisibilityOfControlsAndShowJournalEntries();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }

        // Other methods
        public void ValidateVisibilityOfControlsAndShowJournalEntries()
        {
            User user = (User)Session["User"];
            lblMessage.Text = "";

            if (calendarDiary.SelectedDate.Date == DateTime.MinValue.Date)
            {
                lblMessage.Text = "* Please select a date.";
            }

            else
            {
                var diary = new Models.Diary();
                bool journalExists = diary.CheckJournalExistsForTheUser(calendarDiary.SelectedDate.Date.ToString("dd/MM/yyyy"), user.EmailId);
                
                // If journal entry exists
                if (journalExists)
                {
                    txtJournal.Visible = true;
                    txtEditJournal.Visible = false;
                    btnAddJournal.Visible = false;
                    btnEditJournal.Visible = true;
                    btnDeleteJournal.Visible = true;
                    // execute code to show journal content for the specified date and user.
                    var diaryDetails = diary.ViewJournalEntry(calendarDiary.SelectedDate.Date.ToString("dd/MM/yyyy"), user.EmailId);
                    lblMessage.Text = "* Scroll down bottom to see journal entry.";
                    txtJournal.Text = "Journal entry on: " + diaryDetails.JournalDate + "\n \n" + diaryDetails.Content;                    
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
                    lblMessage.Text = "* No journal entry is added. Scroll down bottom to add an entry.";
                }
            }
        }

        protected void btnEditJournal_Click(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            var diary = new Models.Diary();            
            diary.GetUser.EmailId = user.EmailId;
            diary.JournalDate = calendarDiary.SelectedDate.Date.ToString("dd/MM/yyyy");
            Session["JournalEntry"] = diary;
            Response.Redirect("EditJournal.aspx");
        }
    }
}
 