using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace deneme.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            
            // E-posta ayarları - appsettings.json'dan gelecek
            _smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
            _smtpUsername = _configuration["EmailSettings:Username"] ?? "";
            _smtpPassword = _configuration["EmailSettings:Password"] ?? "";
            _fromEmail = _configuration["EmailSettings:FromEmail"] ?? "noreply@armicrm.com";
            _fromName = _configuration["EmailSettings:FromName"] ?? "Armi CRM";
        }

        public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string userName)
        {
            try
            {
                var subject = "Şifre Sıfırlama Talebi - Armi CRM";
                var resetLink = $"http://localhost:7000/Account/ResetPassword?token={resetToken}&email={toEmail}";
                
                var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background: linear-gradient(135deg, #ffb6b9 0%, #ff7eb3 100%); padding: 20px;'>
                    <div style='max-width: 600px; margin: 0 auto; background: white; border-radius: 12px; padding: 30px; box-shadow: 0 8px 32px rgba(0,0,0,0.1);'>
                        <h2 style='color: #ff7eb3; text-align: center; margin-bottom: 30px;'>🔑 Şifre Sıfırlama</h2>
                        
                        <p style='color: #333; font-size: 16px;'>Merhaba <strong>{userName}</strong>,</p>
                        
                        <p style='color: #666; line-height: 1.6;'>
                            Armi CRM hesabınız için şifre sıfırlama talebinde bulundunuz. 
                            Şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklayın:
                        </p>
                        
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' 
                               style='background: linear-gradient(90deg, #ff758c 0%, #ff7eb3 100%); 
                                      color: white; 
                                      padding: 15px 30px; 
                                      text-decoration: none; 
                                      border-radius: 8px; 
                                      font-weight: bold;
                                      display: inline-block;'>
                                Şifremi Sıfırla
                            </a>
                        </div>
                        
                        <p style='color: #999; font-size: 14px; text-align: center;'>
                            Bu bağlantı 1 saat geçerlidir. Eğer bu talebi siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.
                        </p>
                        
                        <div style='border-top: 1px solid #eee; margin-top: 30px; padding-top: 20px; text-align: center;'>
                            <p style='color: #999; font-size: 12px;'>
                                © 2024 Armi CRM - Tüm hakları saklıdır
                            </p>
                        </div>
                    </div>
                </body>
                </html>";

                return await SendEmailAsync(toEmail, subject, body);
            }
            catch (Exception ex)
            {
                // Log hatayı
                Console.WriteLine($"Şifre sıfırlama e-postası gönderilirken hata: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendWelcomeEmailAsync(string toEmail, string userName)
        {
            try
            {
                var subject = "Hoş Geldiniz - Armi CRM";
                
                var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background: linear-gradient(135deg, #ffb6b9 0%, #ff7eb3 100%); padding: 20px;'>
                    <div style='max-width: 600px; margin: 0 auto; background: white; border-radius: 12px; padding: 30px; box-shadow: 0 8px 32px rgba(0,0,0,0.1);'>
                        <h2 style='color: #ff7eb3; text-align: center; margin-bottom: 30px;'>🎉 Hoş Geldiniz!</h2>
                        
                        <p style='color: #333; font-size: 16px;'>Merhaba <strong>{userName}</strong>,</p>
                        
                        <p style='color: #666; line-height: 1.6;'>
                            Armi CRM ailesine hoş geldiniz! Hesabınız başarıyla oluşturuldu ve artık 
                            tüm özelliklerden yararlanabilirsiniz.
                        </p>
                        
                        <div style='background: #f8f9fa; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                            <h3 style='color: #ff7eb3; margin-top: 0;'>🚀 Neler Yapabilirsiniz:</h3>
                            <ul style='color: #666; line-height: 1.8;'>
                                <li>Müşteri bilgilerini yönetebilirsiniz</li>
                                <li>Ağ sistemlerini takip edebilirsiniz</li>
                                <li>Raporlar oluşturabilirsiniz</li>
                                <li>Ekip üyeleriyle işbirliği yapabilirsiniz</li>
                            </ul>
                        </div>
                        
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='http://localhost:7000/Account/Login' 
                               style='background: linear-gradient(90deg, #ff758c 0%, #ff7eb3 100%); 
                                      color: white; 
                                      padding: 15px 30px; 
                                      text-decoration: none; 
                                      border-radius: 8px; 
                                      font-weight: bold;
                                      display: inline-block;'>
                                Giriş Yap
                            </a>
                        </div>
                        
                        <div style='border-top: 1px solid #eee; margin-top: 30px; padding-top: 20px; text-align: center;'>
                            <p style='color: #999; font-size: 12px;'>
                                © 2024 Armi CRM - Tüm hakları saklıdır
                            </p>
                        </div>
                    </div>
                </body>
                </html>";

                return await SendEmailAsync(toEmail, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hoş geldin e-postası gönderilirken hata: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using var client = new SmtpClient(_smtpServer, _smtpPort);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail, _fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-posta gönderilirken hata: {ex.Message}");
                return false;
            }
        }
    }
} 