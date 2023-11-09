using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Settings
{
    public class F1LSettings : SaveableSettings
    {
        protected override string Filename => "F1LSettings.config";
        private static string FilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F1T\\files\\";


        public F1LSettings() : base()
        {
            Username = "";
            RefreshToken = "";
            AuthToken = "";
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetField(ref _username, value, "Username"); }
        }

        private string _refreshToken;
        public string RefreshToken
        {
            get { return _refreshToken; }
            set { SetField(ref _refreshToken, value, "RefreshToken"); }
        }

        private string _authToken;
        public string AuthToken
        {
            get { return _authToken; }
            set { SetField(ref _authToken, value, "AuthToken"); }
        }


    }
}
