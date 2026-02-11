using bolly.Models;

namespace bolly.Services;

public class TestimonialService : ITestimonialService
{
    private readonly List<Testimonial> _testimonials = new();

    public TestimonialService()
    {
        InitializeTestimonials();
    }

    private void InitializeTestimonials()
    {
        _testimonials.AddRange(new[]
        {
            new Testimonial
            {
                Id = 1,
                PropertyTitle = "Nieuwweergevondenweg Sigh steeg # 1",
                PropertyImage = "/images/testimonial1.jpg",
                ClientName = "John Doe",
                ClientMessage = "Amazing service! The team helped me find the perfect property within my budget. Highly recommended!",
                Rating = 5,
                DatePosted = DateTime.Now.AddMonths(-2)
            },
            new Testimonial
            {
                Id = 2,
                PropertyTitle = "Indira Gandhiweg # 277/ 8 apartments + house",
                PropertyImage = "/images/testimonial2.jpg",
                ClientName = "Sarah Williams",
                ClientMessage = "Professional and efficient. The entire process was smooth and transparent.",
                Rating = 5,
                DatePosted = DateTime.Now.AddMonths(-1)
            },
            new Testimonial
            {
                Id = 3,
                PropertyTitle = "Chatterpal Straat # 7",
                PropertyImage = "/images/testimonial3.jpg",
                ClientName = "Michael Johnson",
                ClientMessage = "Great team with excellent market knowledge. They made selling my property hassle-free!",
                Rating = 5,
                DatePosted = DateTime.Now.AddDays(-15)
            }
        });
    }

    public Task<List<Testimonial>> GetAllTestimonialsAsync()
    {
        return Task.FromResult(_testimonials);
    }

    public Task<Testimonial?> GetTestimonialByIdAsync(int id)
    {
        return Task.FromResult(_testimonials.FirstOrDefault(t => t.Id == id));
    }
}
