using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Kursach.ConnectionDB
{
    internal class ConnectionString
    {
        static public string ConnectionStr
        {
            get
            {
                return
                    ConfigurationManager.ConnectionStrings["Kursach.Properties.Settings.Connection"].ConnectionString;
            }
        }
    }
}
