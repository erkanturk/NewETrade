using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Models;
/// <summary>
/// İşlem sonucunu temsil eden bir kayıt türü. Başarılı veya başarısız durumunu ve ilgili mesajı içerir.
/// Business katmanında servislerin işlem sonuçlarını standart bir şekilde döndürmek için kullanılır.
/// Controller mesajı ekranda gösterir.
/// Kullanımı: return ServiceResult.Ok("İşlem başarılı."); veya return ServiceResult.Fail("İşlem başarısız.");
/// </summary>

public record ServiceResult(bool Success, string Message)
{
    public static ServiceResult Ok(string message = "İşlem başarılı.") => new(true, message);

    public static ServiceResult Fail(string message) => new(false, message);
}
