namespace Puchurl.Models;

public class ShortUrl
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string OriginalUrl { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public long HitCount { get; set; }
}

