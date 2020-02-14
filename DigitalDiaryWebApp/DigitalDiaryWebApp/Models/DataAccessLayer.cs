using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DigitalDiaryWebApp.Models
{
    public class DataAccessLayer
    {
        //Fields
        private SqlConnection connection; // Instance of database connection used to access appointable database.
        private string connectionString = ConfigurationManager.ConnectionStrings["DigitalDiary"].ConnectionString;
        public string ConnectionString { get { return connectionString; } } //Gets the connection string of appointable database.

        /// <summary>
        /// Methods to perform: Create, Update, Delete operations.
        /// </summary>
        /// <param name="sqlQueryToExecute">SQL query to execute.</param>
        public int InsertOrUpdateOrDeleteData(SqlCommand ExecuteStoredProcedureOrTSQLQuery)
        {
            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                ExecuteStoredProcedureOrTSQLQuery.Connection = connection;
                return ExecuteStoredProcedureOrTSQLQuery.ExecuteNonQuery();
            }
        }

        public DataSet ReadData(SqlCommand ExecuteStoredProcedureOrTSQLQuery)
        {
            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                DataSet dataSet = new DataSet();
                ExecuteStoredProcedureOrTSQLQuery.Connection = connection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(ExecuteStoredProcedureOrTSQLQuery);
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        private int CheckRecordExistInTable(SqlCommand ExecuteStoredProcedureOrTSQLQuery)
        {
            using (connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                ExecuteStoredProcedureOrTSQLQuery.Connection = connection;
                return (int)ExecuteStoredProcedureOrTSQLQuery.ExecuteScalar();
            }
        }

        /// <summary>
        /// User.cs class method calls
        /// </summary>

        public DataSet GetAllUsers()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM[User]";
            return ReadData(command);
        }
                        
        public void RegisterUser(string EmailId, string UserName, string Password, string Fullname)
        {
            // Road block: I was having issues to add the user registration details to the user table.
            // Solution was to add square brackets to User like [User]: https://stackoverflow.com/questions/6082412/sql-error-incorrect-syntax-near-the-keyword-user 

            // Aadd parameters and sql query to execute in the database.
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@EmailId", EmailId);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Fullname", Fullname);
            command.CommandText = "INSERT into [User] (EmailId,UserName,Password,Fullname) " +
                                  "VALUES (@EmailId,@UserName,@Password,@Fullname)";

            // Execute sql query command object with given parameters.
            InsertOrUpdateOrDeleteData(command);
        }

        public bool CheckEmailExists(string emailID)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@EmailId", emailID);
            command.CommandText = "SELECT COUNT(*) FROM[User] WHERE([EmailId] = @EmailId)";
            int recordCount = CheckRecordExistInTable(command);
            if (recordCount > 0)
                return true;
            return false;
        }      

        /// <summary>
        /// Diary.cs class method calls
        /// </summary>
        
        public void AddDiaryJournalEntry(string journalDate, string emailId, string journalContent)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@JournalDate", journalDate);
            command.Parameters.AddWithValue("@EmailId", emailId);
            command.Parameters.AddWithValue("@JournalContent", journalContent);
            command.CommandText = "INSERT into [Diary] (JournalDate,EmailId,JournalContent) " +
                                  "VALUES (@JournalDate,@EmailId,@JournalContent)";

            // Execute sql query command object with given parameters.
            InsertOrUpdateOrDeleteData(command);
        }

        public void EditDiaryJournalEntry(string EmailId, string JournalDate, string newJournalContent)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@EmailId", EmailId);
            command.Parameters.AddWithValue("@JournalDate", JournalDate);
            command.Parameters.AddWithValue("@JournalContent", newJournalContent);
            command.CommandText = "UPDATE [Diary] SET JournalContent = @JournalContent WHERE EmailId = @EmailId AND JournalDate = @JournalDate";
            InsertOrUpdateOrDeleteData(command);            
        }

        public void DeleteDiaryJournalEntry(string EmailId, string JournalDate)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@EmailId", EmailId);
            command.Parameters.AddWithValue("@JournalDate", JournalDate);
            command.CommandText = "DELETE FROM Diary WHERE EmailId = @EmailId AND JournalDate = @JournalDate";
            InsertOrUpdateOrDeleteData(command);

        }

        public DataSet ViewJournalEntryByDateAndEmailId(string journalDate, string emailId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@JournalDate", journalDate);
            command.Parameters.AddWithValue("@EmailId", emailId);
            command.CommandText = "SELECT * FROM[Diary] WHERE([JournalDate] = @JournalDate AND [EmailId] = @EmailId)";
            return ReadData(command);
        }

        public bool CheckJournalEntryExists(string journalDate, string emailId)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@JournalDate", journalDate);
            command.Parameters.AddWithValue("@EmailId", emailId);
            command.CommandText = "SELECT COUNT(*) FROM[Diary] WHERE([JournalDate] = @JournalDate AND [EmailId] = @EmailId)";
            int recordCount = CheckRecordExistInTable(command);
            if (recordCount > 0)
                return true;
            return false;
        }
    }
}