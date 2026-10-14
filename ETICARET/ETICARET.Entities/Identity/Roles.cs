namespace ETICARET.Entities.Identity;

/// <summary>
/// Rol adları tek yerde. Kodun her yerinde "admin" yazmak yerine Roles.Admin kullanırız;
/// yazım hatası derleme anında yakalanır.
/// </summary>
public static class Roles
{
    public const string Admin = "admin";
}
