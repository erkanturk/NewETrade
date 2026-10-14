using System.ComponentModel.DataAnnotations;

namespace ETICARET.Entities;

public class Comment : BaseEntity
{
    [MaxLength(1000)]
    public required string Text { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    // Identity kullanıcısının Id'si. Kullanıcı adını da saklıyoruz ki
    // yorumları listelerken her yorum için ayrıca kullanıcı sorgulamayalım.
    [MaxLength(450)]
    public required string UserId { get; set; }

    [MaxLength(256)]
    public required string UserName { get; set; }
}
