namespace bolly.Models;

public class Testimonial
{
    public int Id { get; set; }
    public string PropertyTitle { get; set; } = string.Empty;
    public string PropertyImage { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ClientMessage { get; set; } = string.Empty;
    public int Rating { get; set; } = 5;
    public DateTime DatePosted { get; set; }
}
