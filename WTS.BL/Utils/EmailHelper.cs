using System;
using System.Collections.Specialized;

// ReSharper disable AssignNullToNotNullAttribute

namespace WTS.BL.Utils
{
    public static class EmailHelper
    {
        public static NameValueCollection AppSettings;
        private static string _SmtpServer;
        private static int _SmtpPort;
        private static string _AdminEmail;
        private static string _LoginEmail;
        private static string _LoginPassword;
        private static bool _EnableSSL;
        private static bool _HtmlFormat;

        private static string _BccAll;
        private static string _TestEmailFrom;
        private static string _TestEmailTo;

        public static void SetConfig(NameValueCollection appSettings)
        {
            AppSettings = appSettings;
            Init();
        }

        private static void Init()
        {
            _SmtpServer = AppSettings["SmtpServer"];
            _SmtpPort = Convert.ToInt32(AppSettings["SmtpPort"]);
            _LoginEmail = AppSettings["LoginEmail"];
            _AdminEmail = AppSettings["AdminEmail"];
            _LoginPassword = AppSettings["LoginPassword"];
            _EnableSSL = Convert.ToBoolean(AppSettings["EnableSSL"]);
            _HtmlFormat = Convert.ToBoolean(AppSettings["HtmlFormat"]);

            _BccAll = AppSettings["BccAll"];
            _TestEmailFrom = AppSettings["TestEmailFrom"];
            _TestEmailTo = AppSettings["TestEmailTo"];
        }   
    }
}