using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample
{
    public class AppSettings
    {
        public DbSettings ServicesDb { get; set; }
    }

    public class DbSettings
    {
        public string Connection { get; set; }
        public string Db { get; set; }

        public string Url(string name)
        {
            return Connection.Replace("{{DB_NAME}}", name);
        }
    }
}
