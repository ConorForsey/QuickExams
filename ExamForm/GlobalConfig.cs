using QuickExamLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary
{
    public static class GlobalConfig
    {        
        /// <summary>
        /// Reference to the connection string in the view
        /// </summary>
        /// <returns></returns>
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["QuickExamsDB"].ConnectionString;
        }

        /// <summary>
        /// Holds the details of the current user logged in to the application
        /// </summary>
        public static User CurrentUser { get; set; }

        /// <summary>
        /// The email address used by the school using the system to send out emails
        /// </summary>
        /// <returns></returns>
        public static string SchoolEmailAddress()
        {
            return ConfigurationManager.AppSettings["SchoolEmailAddress"].ToString();
        }

        /// <summary>
        /// Gets the password for the stored school email address
        /// </summary>
        /// <returns></returns>
        public static string SchoolEmailAddressPassword()
        {
            return ConfigurationManager.AppSettings["SchoolEmailAddressPassword"].ToString();
        }
    }
}
