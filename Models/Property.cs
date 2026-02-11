namespace bolly.Models;

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public PropertyType Type { get; set; }
    public PropertyStatus Status { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int Parking { get; set; }
    public double SquareFeet { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string AgentName { get; set; } = string.Empty;
    public DateTime ListedDate { get; set; }
    public bool IsFeatured { get; set; }
}

public enum PropertyType
{
    Plot,
    House,
    Apartment,
    Villa,
    Office,
    Warehouse,
    Commercial
}

public enum PropertyStatus
{
    Sale,
    Rent,
    Sold
}
