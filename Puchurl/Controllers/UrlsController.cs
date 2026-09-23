using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puchurl.Data;
using Puchurl.Models;

namespace Puchurl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlsController : ControllerBase
{
    private readonly AppDbContext _db;

    public UrlsController(AppDbContext db)
    {
        _db = db;
    }

    public record CreateUrlRequest(string OriginalUrl);
    public record CreateUrlResponse(string Code, string ShortUrl);

    [HttpPost]
    public async Task<ActionResult<CreateUrlResponse>> Create(CreateUrlRequest request)
    {
        var entity = new ShortUrl
        {
            OriginalUrl = request.OriginalUrl,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        _db.ShortUrls.Add(entity);
        await _db.SaveChangesAsync(); 

        entity.Code = Base62Encode(entity.Id);
        await _db.SaveChangesAsync();

        var shortUrl = $"{Request.Scheme}://{Request.Host}/{entity.Code}";

        return Ok(new CreateUrlResponse(entity.Code, shortUrl));
    }

    [HttpGet("/{code}")]
    public async Task<ActionResult> RedirectToOriginalUrl(string code)
    {
        var entity = await _db.ShortUrls.FirstOrDefaultAsync(u => u.Code == code);
        
        if (entity is null)
        {
            return NotFound();
        }

        entity.HitCount++;
        await _db.SaveChangesAsync();

        return Redirect(entity.OriginalUrl);
    }

    private static string Base62Encode(long value)
    {
        const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        if (value == 0) return alphabet[0].ToString();

        var chars = new List<char>();
        while (value > 0)
        {
            chars.Insert(0, alphabet[(int)(value % 62)]);
            value /= 62;
        }
        return new string(chars.ToArray());
    }
}
