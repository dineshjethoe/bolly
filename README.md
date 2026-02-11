# DC Rise - Real Estate Platform (MudBlazor)

A modern real estate web application built with Blazor WebAssembly and MudBlazor component library. This project showcases a property listing platform inspired by dc-rise.sr.

## Project Structure

```
bolly/
├── Components/              # Reusable Razor components
│   ├── HomeSection.razor    # Main home page section with featured properties
│   ├── PropertyCard.razor   # Individual property card display
│   ├── PropertyFilter.razor # Property search and filter component
│   ├── PropertyTypeGrid.razor # Property type selection grid
│   └── TestimonialCard.razor  # Client testimonial card
├── Models/                  # Data models
│   ├── Property.cs          # Property entity with PropertyType and PropertyStatus enums
│   ├── SearchFilter.cs      # Search filter criteria model
│   └── Testimonial.cs       # Client testimonial entity
├── Pages/                   # Razor pages (routable components)
│   ├── Home.razor           # Homepage (/)
│   ├── Index.razor          # Alternative homepage
│   ├── Properties.razor     # All properties listing page
│   ├── PropertyDetail.razor # Individual property detail page
│   ├── About.razor          # About us page
│   └── Contact.razor        # Contact form page
├── Services/                # Business logic and data services
│   ├── IPropertyService.cs  # Property service interface
│   ├── PropertyService.cs   # Property service implementation
│   ├── ITestimonialService.cs # Testimonial service interface
│   └── TestimonialService.cs  # Testimonial service implementation
├── Layout/                  # Layout components
│   ├── MainLayout.razor     # Main application layout with header and footer
│   ├── NavMenu.razor        # Navigation menu (MudBlazor AppBar)
│   └── Themes/
│       └── AppTheme.cs      # Centralized MudBlazor theme configuration
├── wwwroot/                 # Static files
│   ├── index.html           # HTML entry point
│   ├── css/
│   │   └── app.css          # Global styles
│   └── images/              # Property and testimonial images
├── App.razor                # Root component with routing
├── _Imports.razor           # Global namespace imports
├── Program.cs               # Application setup and dependency injection
└── bolly.csproj             # Project file

```

## Key Features

### 1. **Property Management**
- Browse all properties with detailed information
- Filter properties by type (Plot, House, Apartment, Villa, Office, Warehouse)
- Search properties by status (Sale, Rent, Sold) and location
- View detailed property information including:
  - Bedrooms, bathrooms, parking spaces
  - Square footage and property type
  - Agent contact information
  - Listed date

### 2. **Components**
- **PropertyCard**: Displays property information in a card format with image, details, and price
- **PropertyFilter**: Allows users to search properties with multiple criteria
- **PropertyTypeGrid**: Interactive grid showing property types
- **TestimonialCard**: Displays client feedback and testimonials
- **HomeSection**: Main landing page with featured and latest properties

### 3. **Services**
- **PropertyService**: Manages property data and provides search/filter functionality
- **TestimonialService**: Manages client testimonials and feedback

### 4. **Pages**
- **Home**: Landing page with hero section, property types, featured properties, latest listings, and testimonials
- **Properties**: All properties page with search and filter
- **PropertyDetail**: Detailed view of individual properties
- **About**: Company information and values
- **Contact**: Contact form and contact information

### 5. **Design & Theme**
- Professional MudBlazor theme with custom colors (Purple gradient: #667eea to #764ba2)
- Responsive design that works on desktop, tablet, and mobile
- Dark and light theme support
- Smooth animations and transitions
- Accessible navigation with drawer menu on mobile

## Data Models

### Property
```csharp
public class Property
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    public PropertyType Type { get; set; }
    public PropertyStatus Status { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int Parking { get; set; }
    public double SquareFeet { get; set; }
    public string ImageUrl { get; set; }
    public string AgentName { get; set; }
    public DateTime ListedDate { get; set; }
    public bool IsFeatured { get; set; }
}
```

### Testimonial
```csharp
public class Testimonial
{
    public int Id { get; set; }
    public string PropertyTitle { get; set; }
    public string PropertyImage { get; set; }
    public string ClientName { get; set; }
    public string ClientMessage { get; set; }
    public int Rating { get; set; }
    public DateTime DatePosted { get; set; }
}
```

### SearchFilter
```csharp
public class SearchFilter
{
    public PropertyType? Type { get; set; }
    public PropertyStatus? Status { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Location { get; set; }
}
```

## Enums

### PropertyType
- Plot
- House
- Apartment
- Villa
- Office
- Warehouse
- Commercial

### PropertyStatus
- Sale
- Rent
- Sold

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio Code or Visual Studio
- Blazor WebAssembly SDK

### Running the Application

1. Navigate to the project directory:
   ```bash
   cd c:\repos\bolly
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and navigate to `https://localhost:5001`

## Dependency Injection

The application uses ASP.NET Core dependency injection for services:

```csharp
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
```

## MudBlazor Components Used

- `MudContainer`: Container for layout
- `MudGrid`: Responsive grid layout
- `MudItem`: Grid item
- `MudCard`: Card component for content
- `MudButton`: Button component
- `MudText`: Text typography
- `MudIcon`: Icon display
- `MudChip`: Tag/badge component
- `MudTextField`: Input field
- `MudSelect`: Dropdown selector
- `MudAppBar`: Application header
- `MudNavLink`: Navigation links
- `MudDrawer`: Side drawer for mobile navigation
- `MudAlert`: Alert messages
- `MudDivider`: Divider line
- `MudForm`: Form container
- `MudLink`: Hyperlink
- `MudImage`: Image component
- `MudBreadcrumbs`: Breadcrumb navigation
- `MudThemeProvider`: Theme provider
- `MudDialogProvider`: Dialog/modal support
- `MudSnackbarProvider`: Toast notification support

## Theme Customization

The theme is configured in `Layout/Themes/AppTheme.cs`:

```csharp
public static MudTheme Theme = new MudTheme
{
    PaletteLight = new PaletteLight
    {
        Primary = "#667eea",      // Purple
        Secondary = "#764ba2",    // Dark Purple
        Tertiary = "#f093fb",     // Pink
        // ... more colors
    },
    // ... other configurations
};
```

## Adding New Properties

To add new properties, modify the `InitializeProperties()` method in `PropertyService.cs`:

```csharp
_properties.Add(new Property
{
    Id = 7,
    Title = "Your Property Title",
    Description = "Description",
    Price = 100000,
    Location = "Location",
    Type = PropertyType.House,
    Status = PropertyStatus.Sale,
    // ... other properties
});
```

## Adding Images

1. Place property images in the `wwwroot/images/` directory
2. Reference them in properties using the path `/images/property-name.jpg`

## Future Enhancements

- [ ] Backend API integration
- [ ] User authentication and profiles
- [ ] Property listings management for agents
- [ ] Advanced search with map integration
- [ ] Email notifications
- [ ] Payment processing
- [ ] Admin dashboard
- [ ] Real-time chat with agents

## Technologies Used

- **Blazor WebAssembly**: .NET web framework for building interactive web apps
- **MudBlazor**: Material Design component library for Blazor
- **C# 12**: Latest C# language features
- **.NET 8.0**: Latest .NET framework version

## License

This project is based on the design of dc-rise.sr and created for educational purposes.

## Support

For questions or issues, please contact us at:
- Phone: (597) 881-1195 / (597) 887-6688
- Email: info@dc-rise.sr
