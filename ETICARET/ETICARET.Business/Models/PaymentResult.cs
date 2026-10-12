using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Models;

/// <summary>
/// Record keywordu sadece veriyi yaşıyan değiştirilemeyen (immutable) nesneler oluşturmak için kullanılır.
/// Recordlar, sınıflara benzer şekilde davranır, ancak değer tabanlı eşitlik ve kopyalama gibi 
/// özelliklere sahiptir. Recordlar, genellikle veri taşıma nesneleri (DTO) veya immutable veri yapıları için tercih edilir.
/// </summary>
/// <param name="Success"></param>
/// <param name="PaymentId"></param>
/// <param name="ConversationId"></param>
/// <param name="ErrorMessage"></param>
public record PaymentResult(bool Success,string? PaymentId,string?ConversationId,string?ErrorMessage)
{
    public static PaymentResult Fail(string message) => new(false, null, null, message);
}
