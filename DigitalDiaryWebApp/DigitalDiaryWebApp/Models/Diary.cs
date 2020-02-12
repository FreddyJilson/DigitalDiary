using System;

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

    }
}