using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Paltrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void SeedPartnerOrganizations(AppDbContext context, IHostEnvironment env)
    {
        context.Database.EnsureCreated();
        if (!context.LogisticsPartners.Any())
        {
            var filePath = Path.Combine(env.ContentRootPath, "..", "Paltrack.Infrastructure", "Data", "SeedData", "PartnersData.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new CustomDateTimeConverter() }
                };

                var items = JsonSerializer.Deserialize<List<LogisticsPartner>>(json, options);

                if (items != null)
                {
                    context.LogisticsPartners.AddRange(items);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new FileNotFoundException($"The file '{filePath}' was not found. Ensure it exists in the specified location.");
            }
        }
    }

    private class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string[] formats = new[]
        {
            "yyyy-MM-dd",
            "yyyy-MM-ddTHH:mm:ss",
            "MM/dd/yyyy",
            "dd-MM-yyyy",
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            "MMM dd, yyyy",
            "MMM d, yyyy"

        };

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new JsonException($"Unable to parse '{str}' to DateTime.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
