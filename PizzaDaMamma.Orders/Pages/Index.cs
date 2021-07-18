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

                result = result.Replace("<!--here-->", build(items));
            }
            return result;
        }

        private static string build(MenuItemModel[] items)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0, icount = (items == null ? 0 : items.Length); i < icount; i++)
            {
                MenuItemModel item = items[i];
                string desc = string.Join(", ", item.Ingridients);
                string html = @$"
        <div class='pdm-col'>
            <div class='card-header'>
                {item.Name}
            </div>
            <div class='card-image'>
                <img src='img/menu/{item.Image}' />
            </div>
            <div class='card-description'>
                {desc}
            </div>
            <table class='striped card-table-info' cellspacing='0' cellpadding='0'>
                <tbody>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td'>Диаметр</td>
                        <td class='card-table-info-td'>{item.Diameter}</td>
                    </tr>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td'>Вес</td>
                        <td class='card-table-info-td'>{item.Weight}</td>
                    </tr>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td' colspan='2' style='text-align:center;font-weight:bold;font-style:italic;'>Пищевая ценность продукта на 100 г:</td>
                    </tr>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td'>Энергетическая ценность</td>
                        <td class='card-table-info-td'>{item.EnergyValue}</td>
                    </tr>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td'>Углеводы </td>
                        <td class='card-table-info-td'>{item.Carbohydrates}</td>
                    </tr>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td'>Белки </td>
                        <td class='card-table-info-td'>{item.Protein}</td>
                    </tr>
                    <tr class='card-table-info-row'>
                        <td class='card-table-info-td'>Жиры</td>
                        <td class='card-table-info-td'>{item.Fats}</td>
                    </tr>
                </tbody>
            </table>
            <table class='card-table-footer' cellspacing='0' cellpadding='0'>
                <tbody>
                    <tr class='card-table-footer-row'>
                        <td class='card-table-footer-td card-table-footer-price'>{item.Price}</td>
                        <td class='card-table-footer-td card-table-footer-button'>Выбрать</td>
                    </tr>
                </tbody>
            </table>
        </div>
";
                result.AppendLine(html);
            }
            return result.ToString();
        }

        private static MenuItemModel[] loadData(IWebHostEnvironment env)
        {
            MenuItemModel[] result = null;

            System.Type t = typeof(Startup);
            string file = string.Concat(t.Assembly.Location.Replace(t.Assembly.ManifestModule.Name, string.Empty), env.IsDevelopment() ? @"..\..\..\" : string.Empty, @"wwwroot\data\index.json");
            if (File.Exists(file))
            {
                string json = readFileAsString(file);
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
