using System.Text.Json.Serialization;

namespace PizzaDaMamma.Orders.Models
{
    public class MenuItemModel
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Изображение")]
        public string Image { get; set; }
        [JsonPropertyName("Ингридиенты")]
        public string[] Ingridients { get; set; }
        [JsonPropertyName("Диаметр")]
        public string Diameter { get; set; }
        [JsonPropertyName("Вес")]
        public string Weight { get; set; }
        [JsonPropertyName("Энергетическая ценность")]
        public string EnergyValue { get; set; }
        [JsonPropertyName("Углеводы")]
        public string Carbohydrates { get; set; }
        [JsonPropertyName("Белки")]
        public string Protein { get; set; }

        [JsonPropertyName("Жиры")]
        public string Fats { get; set; }
    }
}
