using QuickExamLibrary.DataAccessController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Controllers
{
    public abstract class ControllerBase
    {
        private readonly SQLConnector sqlConnector = new SQLConnector();

        /// <summary>
        /// Getter method for the SQLConnector
        /// </summary>
        /// <returns></returns>
        public SQLConnector SQLConnector()
        {
            return sqlConnector;
        }

        public async Task<bool> ChangePassword(string existingPassword, string newPassword, string newPassConfirm)
        {
            if (String.Equals(newPassword, newPassConfirm))
            {
                await SQLConnector().ChangePassword(GlobalConfig.CurrentUser.UserId, existingPassword, newPassword);
                return true;
            }
            else return false;         
        }
    }
}
