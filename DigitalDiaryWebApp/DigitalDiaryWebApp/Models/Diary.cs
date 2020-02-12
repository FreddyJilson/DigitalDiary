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
        public DateTime JournalDate { get; set; }
        public User GetUser { get{ return user; } }
        public string Content { get; set; }

        // Constructors
        public Diary()
        {
            user = new User();
            databaseAccess = new DataAccessLayer();
        }

        // Methods

        public bool CheckJournalExistsForTheUser(string selectedDate, string emailId)
        {
            return databaseAccess.CheckJournalEntryExists(selectedDate, emailId);
        }

        public List<string> AddJournalEntry(string selectedDate, string emailId, string journalContent)
        {
            var feedbackMessages = ValidateJournalEntry(selectedDate,emailId,journalContent);

            if (feedbackMessages.Count == 0)
            {
                databaseAccess.AddDiaryJournalEntry(selectedDate, emailId, journalContent);
                feedbackMessages.Add("Your journal entry is now added");
            }
            return feedbackMessages;
        }

        public List<string> ValidateJournalEntry(string selectedDate, string emailId, string journalContent)
        {
            var feedbackMessages = new List<string>();
            if (!ValidateJournalContent(journalContent))
            {
                feedbackMessages.Add("Please enter valid journal entry of atleast 100 characters and not more than 8000 characters");
            }

            if (CheckJournalExistsForTheUser(selectedDate, emailId))
            {
                feedbackMessages.Add("Already a journal entry for this date exists, please click edit if you would like to edit the journal");
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