using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DigitalDiaryWebApp.Models
{
    public class User
    {
        //Fields
        private DataAccessLayer databaseAccess;
        
        //Properties
        public String EmailId { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Fullname { get; set; }
        

        //Constructor
        public User()
        {
            databaseAccess = new DataAccessLayer();            
        }

        //Methods

        /* For user to login Digital Diary website. Used in: Home.aspx */
        public User Login(string username, string password)
        {
            //Get all users in the database
            var results = databaseAccess.GetAllUsers();
            //Get the result of User datatable
            var usersDataTable = results.Tables[0];
            var user = new User();

            //Loop through the User Data table and if user id or password or email matches then return the User object with all details
            if (usersDataTable != null)
            {
                for (int i = 0; i < usersDataTable.Rows.Count; i++)
                {
                    user.EmailId = usersDataTable.Rows[i]["EmailId"].ToString();
                    user.UserName = usersDataTable.Rows[i]["UserName"].ToString();
                    user.Password = usersDataTable.Rows[i]["Password"].ToString();
                    user.Fullname = usersDataTable.Rows[i]["Fullname"].ToString();
                    
                    if (User.EncryptUserName(username).Equals(user.UserName) && User.EncryptPassword(password).Equals(user.Password))
                    {
                        return user;
                    }
                }
            }
            // If login details don't match, then return a null value.
            return null;
        }

        /* Register user in database. Used in: Register.aspx */
        public List<string> CreateUser(string EmailId, string UserName, string Password, string Fullname)
        {
            var result = ValidateUser(EmailId, UserName, Password, Fullname);

            if (result.Count == 0)
            {
                // Encrypt UserID using SHA256
                string userIDHash = EncryptUserName(UserName);

                // Encrypt Password using SHA256
                string passwordHash = EncryptPassword(Password);

                databaseAccess.RegisterUser(EmailId, userIDHash, passwordHash, Fullname);
                result.Add("* You have now created the account, please login.");
            }
            return result;
        }

        public List<string> ValidateUser(string EmailId, string UserName, string Password, string Fullname)
        {
            List<string> validationResult = new List<string>();

            // When registering, if user email already exists in database then show the below error message
            if (CheckIfUserEmailExists(EmailId))
            {
                validationResult.Add("* An account with this email already exists. Please login using existing account.");
            }

            // Otherwise carry on doing remaining validation checks
            else
            {
                if (!ValidateEmail(EmailId) || EmailId.Length <= 0 || EmailId.Length > 254)
                {
                    validationResult.Add("* Valid email is required.");
                }

                if (UserName.Length <= 5 || UserName.Length > 10)
                    validationResult.Add("* Valid user name is required.");

                if (!ValidatePassword(Password))
                {
                    validationResult.Add("* Valid password required ranging from 8 to 14 characters and requires atleast: <br>" +
                                         "&nbsp 1 uppercase letter, <br>" +
                                         "&nbsp 1 lowercase letter, <br>" +
                                         "&nbsp 1 digit and <br>" +
                                         "&nbsp 1 special character ");
                }

                if (Fullname.Length <= 0 || Fullname.Length > 30)
                    validationResult.Add("* Valid full name is required.");
            }
            return validationResult;
        }

        // Phone validation: https://stackoverflow.com/questions/19715303/regex-that-accepts-only-numbers-0-9-and-no-characters
        public bool ValidatePhone(string str)
        {
            return Regex.IsMatch(str, "^[0-9]+$");
        }


        // Email validation: https://stackoverflow.com/questions/5342375/regex-email-validation#comment26865637_13704625
        public bool ValidateEmail(string str)
        {
            return Regex.IsMatch(str, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        //Password validation: https://stackoverflow.com/questions/5859632/regular-expression-for-password-validation/5859963
        public bool ValidatePassword(string password)
        {
            // Min 8 and max 15 characters. 1 uppercase, 1 lower case, 1 digit and 1 special character are required. 
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
        }

        /* Encrypt User ID and password: https://www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
         * Never decrypt authentication details
         * Only compare the hashed inputs with the stored hash values in the database for authentication.
         * https://stackoverflow.com/questions/212510/what-is-the-easiest-way-to-encrypt-a-password-when-i-save-it-to-the-registry
         */
        public static string EncryptUserName(string userid)
        {
            return User.Encryption(userid);
        }

        public static string EncryptPassword(string password)
        {
            return User.Encryption(password);
        }

        public static string Encryption(string encryptData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(encryptData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool CheckIfUserEmailExists(string EmailID)
        {
            return databaseAccess.CheckEmailExists(EmailID);
        }
    }
}
