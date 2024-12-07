using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace FamilyManage.Shared
{
    public class WeatherForecast
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}