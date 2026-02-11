using bolly.Models;
using System.Text.Json;

namespace bolly.Services;

public class PropertyService : IPropertyService
{
    private readonly List<Property> _properties = new();
    private readonly HttpClient _httpClient;

    public PropertyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task InitializeAsync()
    {
        await LoadPropertiesFromJsonAsync();
    }

    private async Task LoadPropertiesFromJsonAsync()
    {
        try
        {
            var json = await _httpClient.GetStringAsync("sample-data/properties-sample-data.json");
            var sampleProperties = JsonSerializer.Deserialize<List<SamplePropertyDto>>(json);

            if (sampleProperties != null)
            {
                foreach (var sample in sampleProperties)
                {
                    var property = new Property
                    {
                        Id = sample.Id,
                        Title = sample.Title,
                        Description = sample.Description,
                        Price = sample.Price,
                        Location = sample.Location,
                        Type = ParsePropertyType(sample.Type),
                        Status = ParsePropertyStatus(sample.Status),
                        Bedrooms = sample.Bedrooms,
                        Bathrooms = sample.Bathrooms,
                        Parking = sample.Parking,
                        SquareFeet = sample.SquareFeet,
                        ImageUrl = sample.ImageUrl,
                        AgentName = sample.AgentName,
                        ListedDate = sample.ListedDate,
                        IsFeatured = sample.IsFeatured
                    };
                    _properties.Add(property);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading properties from JSON: {ex.Message}");
            // Fallback to empty list - filter will show appropriate message
        }
    }

    private PropertyType ParsePropertyType(string type)
    {
        return type.ToLower() switch
        {
            "house" => PropertyType.House,
            "apartment" => PropertyType.Apartment,
            "villa" => PropertyType.Villa,
            "plot" => PropertyType.Plot,
            "studio" => PropertyType.Apartment,
            "commercial" => PropertyType.Commercial,
            "warehouse" => PropertyType.Commercial,
            "office" => PropertyType.Commercial,
            _ => PropertyType.House
        };
    }

    private PropertyStatus ParsePropertyStatus(string status)
    {
        return status.Contains("Rent", StringComparison.OrdinalIgnoreCase) 
            ? PropertyStatus.Rent 
            : PropertyStatus.Sale;
    }

    private class SamplePropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Parking { get; set; }
        public int SquareFeet { get; set; }
        public string ImageUrl { get; set; }
        public string AgentName { get; set; }
        public DateTime ListedDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsFeatured { get; set; }
    }

    private void InitializeProperties()
    {
        // Properties are loaded from JSON via InitializeAsync()
    }

    public Task<List<Property>> GetAllPropertiesAsync()
    {
        return Task.FromResult(_properties);
    }

    public Task<List<Property>> GetFeaturedPropertiesAsync()
    {
        return Task.FromResult(_properties.Where(p => p.IsFeatured).ToList());
    }

    public Task<List<Property>> GetLatestPropertiesAsync()
    {
        return Task.FromResult(_properties.OrderByDescending(p => p.ListedDate).ToList());
    }

    public Task<Property?> GetPropertyByIdAsync(int id)
    {
        return Task.FromResult(_properties.FirstOrDefault(p => p.Id == id));
    }

    public Task<List<Property>> SearchPropertiesAsync(SearchFilter filter)
    {
        var result = _properties.AsEnumerable();

        if (filter.Type.HasValue)
            result = result.Where(p => p.Type == filter.Type);

        if (filter.Status.HasValue)
            result = result.Where(p => p.Status == filter.Status);

        if (filter.MinPrice.HasValue)
            result = result.Where(p => p.Price >= filter.MinPrice);

        if (filter.MaxPrice.HasValue)
            result = result.Where(p => p.Price <= filter.MaxPrice);

        if (!string.IsNullOrEmpty(filter.Location))
            result = result.Where(p => p.Location.Contains(filter.Location, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(result.ToList());
    }

    public Task<List<Property>> GetPropertiesByTypeAsync(PropertyType type)
    {
        return Task.FromResult(_properties.Where(p => p.Type == type).ToList());
    }
}
