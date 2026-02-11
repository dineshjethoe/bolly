namespace bolly.Services;

using System.Text.Json;

public class LanguageService
{
    private string _currentLanguage = "nl"; // Dutch by default
    private Dictionary<string, Dictionary<string, object>> _translations = new();
    private readonly HttpClient _httpClient;

    public event Action? OnLanguageChanged;

    public LanguageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string CurrentLanguage => _currentLanguage;

    public async Task InitializeAsync()
    {
        await LoadTranslationsAsync("nl");
        await LoadTranslationsAsync("en");
    }

    public void SetLanguage(string languageCode)
    {
        if (_currentLanguage != languageCode && _translations.ContainsKey(languageCode))
        {
            _currentLanguage = languageCode;
            OnLanguageChanged?.Invoke();
        }
    }

    public string Translate(string key)
    {
        return GetTranslationValue(key) ?? key;
    }

    private string? GetTranslationValue(string key)
    {
        if (!_translations.TryGetValue(_currentLanguage, out var languageDict))
            return null;

        var keys = key.Split('.');
        object? current = languageDict;

        foreach (var k in keys)
        {
            if (current is Dictionary<string, object> dict && dict.TryGetValue(k, out var value))
            {
                current = value;
            }
            else if (current is JsonElement element && element.ValueKind == JsonValueKind.Object)
            {
                if (element.TryGetProperty(k, out var property))
                {
                    current = property;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        if (current is JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.String)
            {
                return jsonElement.GetString();
            }
        }

        return current?.ToString();
    }

    private async Task LoadTranslationsAsync(string languageCode)
    {
        try
        {
            var response = await _httpClient.GetAsync($"resources/{languageCode}.json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(content);
            var rootElement = document.RootElement.Clone();
            
            var translations = new Dictionary<string, object>();
            foreach (var property in rootElement.EnumerateObject())
            {
                translations[property.Name] = property.Value;
            }
            
            _translations[languageCode] = translations;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading translations for {languageCode}: {ex.Message}");
        }
    }

    public List<string> GetAvailableLanguages() => ["nl", "en"];

    public string GetLanguageName(string languageCode)
    {
        return languageCode.ToLower() switch
        {
            "nl" => "Nederlands",
            "en" => "English",
            _ => languageCode
        };
    }
}
