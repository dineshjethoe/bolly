using bolly.Models;

namespace bolly.Services;

public interface IPropertyService
{
    Task<List<Property>> GetAllPropertiesAsync();
    Task<List<Property>> GetFeaturedPropertiesAsync();
    Task<List<Property>> GetLatestPropertiesAsync();
    Task<Property?> GetPropertyByIdAsync(int id);
    Task<List<Property>> SearchPropertiesAsync(SearchFilter filter);
    Task<List<Property>> GetPropertiesByTypeAsync(PropertyType type);
}
