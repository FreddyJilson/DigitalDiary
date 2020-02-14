using System;
using System.Collections.Generic;

namespace DigitalDiaryWebApp.Models
{
    public class Diary
    {
        // Fields
        private User user;
        private DataAccessLayer databaseAccess;

        // Properties
        public string DiaryEntryId { get; set; }
        public string JournalDate { get; set; }
        public User GetUser { get{ return user; } }
        public string Content { get; set; }

        // Constructors
        public Diary()
        {
            user = new User();
            databaseAccess = new DataAccessLayer();
        }

        // Methods

        public Diary ViewJournalEntry(string selectedDate, string emailId)
        {
            var JournalEntry = databaseAccess.ViewJournalEntryByDateAndEmailId(selectedDate,emailId);
            //Get the result of Diary datatable
            var journalDataTable = JournalEntry.Tables[0];
            Diary myDiary = new Diary();
            
            //Loop through the Diary Data table then return the Diary object with all details
            if (journalDataTable != null)
            {
                for (int i = 0; i < journalDataTable.Rows.Count; i++)
                {
                    myDiary.DiaryEntryId = journalDataTable.Rows[i]["DiaryEntryId"].ToString();
                    myDiary.JournalDate = journalDataTable.Rows[i]["JournalDate"].ToString();
                    myDiary.GetUser.EmailId = journalDataTable.Rows[i]["EmailId"].ToString();
                    myDiary.Content = journalDataTable.Rows[i]["JournalContent"].ToString();                    
                }
                return myDiary;
            }
            // If diary details are null.
            return null;
        }
        

        public List<string> AddJournalEntry(string selectedDate, string emailId, string journalContent)
        {
            var feedbackMessages = ValidateJournalEntry(selectedDate,emailId,journalContent);

            if (feedbackMessages.Count == 0)
            {
                databaseAccess.AddDiaryJournalEntry(selectedDate, emailId, journalContent);
                feedbackMessages.Add("* Your journal entry is now added for the date: " + selectedDate);
            }
            return feedbackMessages;
        }

        public void EditJournalEntry(string EmailId, string JournalDate, string JournalContent)
        {
            databaseAccess.EditDiaryJournalEntry(EmailId, JournalDate, JournalContent);
        }

        public bool CheckJournalExistsForTheUser(string selectedDate, string emailId)
        {
            return databaseAccess.CheckJournalEntryExists(selectedDate, emailId);
        }

        public List<string> ValidateJournalEntry(string selectedDate, string emailId, string journalContent)
        {
            var feedbackMessages = new List<string>();
            if (!ValidateJournalContent(journalContent))
            {
                feedbackMessages.Add("* Please enter valid journal entry of atleast 100 characters and not more than 8000 characters");
            }

            if (CheckJournalExistsForTheUser(selectedDate, emailId))
            {
                feedbackMessages.Add("* Already a journal entry for this date exists, please click edit if you would like to edit the journal");
            }
            return feedbackMessages;
        }

        public bool ValidateJournalContent(string journalContent)
        {
            if(journalContent.Length < 100 || journalContent.Length > 8000)
                return false;
            
            return true;
        }       
    }
}