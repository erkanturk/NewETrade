using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Settings;
/// <summary>
/// Appsettings.json dosyasında Iyzico ayarlarını temsil eden sınıf.
/// Program.cs builder.services.Configure<IyzicoSettings>(builder.Configuration.
/// GetSection("IyzicoSettings")); ile appsettings.json 
/// dosyasındaki IyzicoSettings bölümünü bu sınıfa bağlayabilirsiniz.
/// </summary>

public class IyzicoSettings
{
    public string BaseUrl { get; set; }= "https://sandbox-api.iyzipay.com";
    public string ApiKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
