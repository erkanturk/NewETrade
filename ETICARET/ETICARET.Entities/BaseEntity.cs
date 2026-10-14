namespace ETICARET.Entities;

/// <summary>
/// Bütün tabloların ortak alanları. Her entity bu sınıftan türer.
/// NOT: abstract → tek başına "new BaseEntity()" yapılamaz, sadece miras alınır.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }

    // Kayıt oluşturulduğu an otomatik dolar (varsayılan değer).
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
