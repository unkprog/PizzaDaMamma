using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PizzaDaMamma.Orders.Models;

namespace PizzaDaMamma.Orders.Pages
{
    public static class Index
    {
        public static string Load(IWebHostEnvironment env)
        {
            string result = string.Empty;
            System.Type t = typeof(Startup);
            string file = string.Concat(t.Assembly.Location.Replace(t.Assembly.ManifestModule.Name, string.Empty), env.IsDevelopment() ? @"..\..\..\" : string.Empty, @"wwwroot\index.html");
            if (File.Exists(file))
            {
                result = readFileAsString(file);

                MenuItemModel[] items = loadData(env);

                result = result.Replace("<!--here-->", build());
            }
            return result;
        }

        private static string build()
        {
            string result = string.Empty;

            return result;
        }

        private static MenuItemModel[] loadData(IWebHostEnvironment env)
        {
            MenuItemModel[] result = null;

            System.Type t = typeof(Startup);
            string file = string.Concat(t.Assembly.Location.Replace(t.Assembly.ManifestModule.Name, string.Empty), env.IsDevelopment() ? @"..\..\..\" : string.Empty, @"wwwroot\data\index.json");
            if (File.Exists(file))
            {
                string json = readFileAsString(file, Encoding.GetEncoding("Windows-1251"));
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All) // System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                result = JsonSerializer.Deserialize<MenuItemModel[]>(json, options);
            }
            return result;
        }

        private static string readFileAsString(string aFileName, Encoding enc = null)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (File.Exists(aFileName))
            {
                string line = "";

                using (FileStream fileStream = new FileStream(aFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, enc == null ? Encoding.UTF8 : enc)) //.Default))
                    {
                        line = streamReader.ReadLine();
                        while (line != null)
                        {
                            strBuilder.AppendLine(line);
                            line = streamReader.ReadLine();
                        }
                    }
                }
            }
            return strBuilder.ToString();
        }
    }
}
