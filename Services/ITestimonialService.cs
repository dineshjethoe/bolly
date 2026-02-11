using bolly.Models;

namespace bolly.Services;

public interface ITestimonialService
{
    Task<List<Testimonial>> GetAllTestimonialsAsync();
    Task<Testimonial?> GetTestimonialByIdAsync(int id);
}
