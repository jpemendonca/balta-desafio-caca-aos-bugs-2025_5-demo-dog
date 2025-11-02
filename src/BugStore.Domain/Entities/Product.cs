using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BugStore.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    
    private string _title = string.Empty;
    public string Title 
    { 
        get => _title;
        set
        {
            _title = value;
            Slug = GenerateSlug(value);
        } 
    }
    public string Description { get; set; }
    public string Slug { get; private set; }
    public decimal Price { get; set; }
    private static string GenerateSlug(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        // 1. Normalizar para remover acentos (é -> e, ç -> c)
        var normalizedString = text
            .ToLowerInvariant()
            .Normalize(NormalizationForm.FormD); // Decompõe (ex: "é" vira "e" + "´")

        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark) // Remove os acentos (ex: "´")
            {
                stringBuilder.Append(c);
            }
        }
        
        string slug = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

        // 2. Aplicar seu Regex para remover caracteres especiais restantes
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        // 3. Limpar espaços e substituir por hífens
        slug = Regex.Replace(slug, @"\s+", " ").Trim();
        slug = Regex.Replace(slug, @"\s", "-");

        return slug;
    }
}