using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Settings
{
    /// <summary>
    /// Appsettings.json dosyasındaki Email ayarlarını tarafından bu sınıf dolacak
    /// Password:Gmail hesap şifresi değil Google uygulama şifresi olmalı. Google uygulama şifresi almak için
    /// Google hesabınıza giriş yapın ve "Güvenlik" 
    /// sekmesine gidin. "Uygulama şifreleri"
    /// bölümünden yeni bir uygulama şifresi oluşturun ve bu şifreyi kullanın.
    /// </summary>
    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromName { get; set; } = "ETICARET";

    }
}
