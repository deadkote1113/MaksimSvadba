using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Svadba.Services;

public class ExcelService
{
    private readonly PostgresDbContext _context;

    public ExcelService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GetExcelAsync()
    {
        var result = new StringBuilder();
        var formas = await _context.Formas.ToListAsync();

        result.AppendLine("Имя;Почта;Комментарий");
        foreach (var forma in formas)
            if(!string.IsNullOrEmpty(forma.Name) && !string.IsNullOrEmpty(forma.Email))
                result.AppendLine($"{forma.Name.Trim()};{forma.Email.Trim()};{forma.Comment}");
        
        return Encoding.UTF8.GetBytes(result.ToString());
    }
}
