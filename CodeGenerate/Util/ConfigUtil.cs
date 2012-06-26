using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;
using CJ.Demo.DBUtility.DBCommon;

namespace CodeGenerate.Util
{
    class ConfigUtil
    {
        public static void SwithDb(EnumDbType dbType)
        {
            switch (dbType)
            {
                case EnumDbType.Oracle:
                    DBHelper.ToOracle();
                    break;
                case EnumDbType.SqlServer:
                    DBHelper.ToSqlServer();
                    break;
            }
        }


        // This function shows how to write a key/value
        // pair to the appSettings section.
        static void WriteSettings(string name, string connectionString, string providerName)
        {

            // Get the application configuration file.
            System.Configuration.Configuration config =
               ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


            //// Create a unique key/value pair to add to 
            //// the appSettings section.
            //string keyName = "AppStg" + config.AppSettings.Settings.Count;
            //string value = string.Concat(DateTime.Now.ToLongDateString(),
            //               " ", DateTime.Now.ToLongTimeString());

            //// Add the key/value pair to the appSettings 
            //// section.
            //// config.AppSettings.Settings.Add(keyName, value);
            //System.Configuration.AppSettingsSection appSettings = config.AppSettings;
            //appSettings.Settings.Add(keyName, value);

            //// Save the configuration file.
            //config.Save(ConfigurationSaveMode.Modified);

            //// Force a reload in memory of the changed section.
            //// This to to read the section with the
            //// updated values.
            //ConfigurationManager.RefreshSection("appSettings");

            System.Configuration.ConnectionStringsSection connection = config.ConnectionStrings;
            connection.ConnectionStrings.Remove("Name");
            connection.ConnectionStrings.Add(new ConnectionStringSettings(name, connectionString, providerName));
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
