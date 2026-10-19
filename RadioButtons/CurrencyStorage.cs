using System;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace RadioButtons
{
    public class CurrencyStorage
    {
        private string GetFilePath(string baseCurrency)
        {
            return Path.Combine(FileSystem.AppDataDirectory, $"rates_{baseCurrency}.json");
        }

        public void Save(string baseCurrency, CurrencyRates rates)
        {
            try
            {
                string json = JsonSerializer.Serialize(rates);
                File.WriteAllText(GetFilePath(baseCurrency), json);
            }
            catch
            {
             
            }
        }

        public CurrencyRates? Load(string baseCurrency)
        {
            try
            {
                string path = GetFilePath(baseCurrency);

                if (!File.Exists(path))
                    return null;

                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<CurrencyRates>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
