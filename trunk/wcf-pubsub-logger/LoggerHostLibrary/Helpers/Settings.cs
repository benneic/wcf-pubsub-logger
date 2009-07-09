using System;
using System.Configuration;

namespace LoggerLibrary.Helpers
{
    public class Settings
    {
        public static string Get(string key)
        {
            string v = ConfigurationManager.AppSettings[key];
            if (null == v)
            {
                throw new ApplicationException("Cannot find the '" + key + "' setting in the configuration file");
            }
            return v;
        }

        public static int GetInteger(string key)
        {
            string v = Get(key);
            return int.Parse(v);
        }

        public static Boolean GetBoolean(string key)
        {
            string v = Get(key);
            return (v.ToLower().Trim() == "true");
        }

        public static string GetOptional(string key)
        {
            string v = ConfigurationManager.AppSettings[key];
            return v;
        }

        public static string GetOptional(string key, string defaultValue)
        {
            string v = ConfigurationManager.AppSettings[key];
            if (null == v)
            {
                return defaultValue;
            }
            return v;
        }
    }
}
