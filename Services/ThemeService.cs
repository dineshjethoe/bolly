namespace bolly.Services;

public class ThemeService
{
    private bool _isDarkMode = true;
    
    public event Action? OnThemeChanged;
    
    public bool IsDarkMode => _isDarkMode;
    
    public void SetDarkMode(bool isDark)
    {
        if (_isDarkMode != isDark)
        {
            _isDarkMode = isDark;
            OnThemeChanged?.Invoke();
        }
    }
    
    public void ToggleDarkMode()
    {
        SetDarkMode(!_isDarkMode);
    }
}
