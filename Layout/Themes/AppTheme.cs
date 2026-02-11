using MudBlazor;

namespace bolly.Themes;

public static class AppTheme
{
    public static MudTheme Theme = new MudTheme
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#667eea",
            Secondary = "#764ba2",
            Tertiary = "#f093fb",
            Success = "#4caf50",
            Warning = "#ff9800",
            Error = "#f44336",
            Info = "#2196f3",
            AppbarBackground = "#667eea",
            AppbarText = "#FFFFFF",
            Background = "#fafafa",
            Surface = "#ffffff",
            DrawerBackground = "#ffffff",
            DrawerText = "#000000"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#667eea",
            Secondary = "#764ba2",
            Tertiary = "#f093fb",
            Success = "#4caf50",
            Warning = "#ff9800",
            Error = "#f44336",
            Info = "#2196f3",
            AppbarBackground = "#1a1a2e",
            AppbarText = "#ffffff",
            Black = "#0d0221",
            Background = "#0d0221",
            Surface = "#16213e",
            DrawerBackground = "#16213e",
            DrawerText = "#ffffff"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" }
            }
        }
    };

    public static MudTheme DarkTheme = new MudTheme
    {
        PaletteDark = new PaletteDark
        {
            Primary = "#667eea",
            Secondary = "#764ba2",
            Tertiary = "#f093fb",
            Success = "#4caf50",
            Warning = "#ff9800",
            Error = "#f44336",
            Info = "#2196f3",
            AppbarBackground = "#1a1a2e",
            AppbarText = "#ffffff",
            Black = "#0d0221",
            Background = "#0d0221",
            Surface = "#16213e",
            DrawerBackground = "#16213e",
            DrawerText = "#ffffff"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" }
            }
        }
    };
}


