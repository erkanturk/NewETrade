namespace ETICARET.Business.Settings;

/// <summary>
/// appsettings.json → "Iyzico" bölümünün C# karşılığı (Options pattern).
/// Program.cs: builder.Services.Configure&lt;IyzicoSettings&gt;(builder.Configuration.GetSection("Iyzico"));
/// Kullanım:   IOptions&lt;IyzicoSettings&gt; ile constructor'dan alınır.
/// </summary>
public class IyzicoSettings
{
    public string BaseUrl { get; set; } = "https://sandbox-api.iyzipay.com";
    public string ApiKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
