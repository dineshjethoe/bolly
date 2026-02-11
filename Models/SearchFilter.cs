namespace bolly.Models;

public class SearchFilter
{
    public PropertyType? Type { get; set; }
    public PropertyStatus? Status { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Location { get; set; }
}
