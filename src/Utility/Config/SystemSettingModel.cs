using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Utility.Config
{
    public class SystemSettingModel
    {
        private static SystemSettingModel _instance;

        public static IConfiguration Configs { get; set; }
        public string ApplicationName { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name;

        public string? Domain { get; set; }
        public string SecretKey { get; set; }
        public string SecretCode { get; set; }

        public static SystemSettingModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SystemSettingModel();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
    }

    public class MailSettingModel
    {
        public static MailSettingModel Instance { get; set; }
        public SmtpSetting Smtp { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
    }

    public class SmtpSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UsingCredential { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class VnPaySetting
    {
        public static VnPaySetting Instance { get; set; }
        public string VnPayUrl { get; set; }
        public string VnPayReturnUrl { get; set; }
        public string VnPayIPNUrl { get; set; }
        public string VnPayTmnCode { get; set; }
        public string VnPayHashSecret { get; set; }
    }

    public class VietQRSetting
    {
        public static VietQRSetting Instance { get; set; }
        public string ClientID { get; set; }
        public string APIKey { get; set; }
    }

    public class GoogleSetting
    {
        public static GoogleSetting Instance { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }

    /*public class AppConfig
    {
        public static AppConfig Instance { get; set; }
        private static Dictionary<string, string> _configValues = new Dictionary<string, string>();

        public string TAX => GetConfigValue("TAX");
        public string PLATFORM_FEE => GetConfigValue("PLATFORM_FEE");

        // Load all configs at startup or when needed
        public static void LoadConfigValues(ISystemConfigRepository configRepository)
        {
            var configs = configRepository.GetAllActiveConfigsAsync().Result;
            _configValues = configs.ToDictionary(c => c.Key, c => c.Value);
        }

        private static string GetConfigValue(string key)
        {
            if (_configValues.TryGetValue(key, out var value))
            {
                return value;
            }
            throw new KeyNotFoundException($"Configuration key {key} not found.");
        }

        // Update config in cache after a change
        public static void UpdateConfigInCache(string key, string newValue)
        {
            if (_configValues.ContainsKey(key))
            {
                _configValues[key] = newValue;
            }
        }
    }*/

}