using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Core.Extensions
{
    public static class DotNetEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                Environment.SetEnvironmentVariable(parts[0], string.Join("=", parts[1..]));
            }
        }
    }

    public static class ProgramSettings
    {
        public static void SetEnvironment(string env)
        {
            var envFile = $"{env}.env";
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, envFile);
            DotNetEnv.Load(dotenv);
        }

        public static string LoggerConectionHandler(IConfiguration settings, string sectionName)
        {
            var dbName = settings.GetSection($"AppSettings:{sectionName}:Db").Value;
            return Environment.GetEnvironmentVariable("DB_URL").Replace("{{DB_NAME}}", dbName);
        }
    }
}
