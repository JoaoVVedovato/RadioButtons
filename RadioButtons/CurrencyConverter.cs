using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RadioButtons
{
    public class CurrencyConverter
    {
        private readonly HttpClient _httpClient;
        private readonly CurrencyStorage _storage = new CurrencyStorage();

        public CurrencyRates ratesUSD;
        public string USDtoEUR;
        public string USDtoBRL;


        public CurrencyRates ratesBRL;
        public string BRLtoUSD;
        public string BRLtoEUR;


        public CurrencyRates ratesEUR;
        public string EURtoBRL;
        public string EURtoUSD;


        public CurrencyConverter()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);

            GetAllCurrencyRates();
        }

        public async Task<CurrencyRates> GetRatesAsync(string baseCurrency)
        {
            try
            {
                var url = $"https://open.er-api.com/v6/latest/{baseCurrency}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<CurrencyRates>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao tentar desserializar!");
            }
        }

        private async Task<CurrencyRates?> GetRatesOnlineOrOfflineAsync(string baseCurrency)
        {
            try
            {
                CurrencyRates rates = await GetRatesAsync(baseCurrency);
                _storage.Save(baseCurrency, rates);
                return rates;
            }
            catch
            {
                return _storage.Load(baseCurrency);
            }
        }

        private async void GetAllCurrencyRates()
        {
            ratesUSD = await GetRatesOnlineOrOfflineAsync("USD");
            ratesBRL = await GetRatesOnlineOrOfflineAsync("BRL");
            ratesEUR = await GetRatesOnlineOrOfflineAsync("EUR");

        }

        public decimal ConvertValue(decimal amount, string from, string to)
        {

            if (from == "USD" && to == "BRL")
                return amount * ratesUSD.rates["BRL"];
            else if (from == "USD" && to == "EUR")
                return amount * ratesUSD.rates["EUR"];
            else if (from == "BRL" && to == "USD")
                return amount * ratesBRL.rates["USD"];
            else if (from == "BRL" && to == "EUR")
                return amount * ratesBRL.rates["EUR"];
            else if (from == "EUR" && to == "USD")
                return amount * ratesEUR.rates["USD"];
            else if (from == "EUR" && to == "BRL")
                return amount * ratesEUR.rates["BRL"];
            else
                return amount;
        }
        // 2. Criar uma classe para salvar e para ler
        // todos os dados das cotações em  
        // arquivos no dispositivo no formato da string JSON
        // São 3 arquivos. Um para cada moeda.
        // Permite que o aplicativo funcione tanto Online quanto Offline


    }
}
