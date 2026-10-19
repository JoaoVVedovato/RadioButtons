namespace RadioButtons
{
    public partial class MainPage : ContentPage
    {
        CurrencyConverter currencyConverter;

        public MainPage()
        {
            InitializeComponent();

            currencyConverter = new CurrencyConverter();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // 3. Utilizar as cotações atualizadas.

            RadioButton rb = sender as RadioButton;

            if (rb.Content.Equals("Dolar"))
            {
                lbValorConvertido.Text =
                    String.Format("{0:F2}",
                    Convert.ToSingle(txValor.Text) / 4.98f);
            }

            if (rb.Content.Equals("Euro"))
            {
                lbValorConvertido.Text =
                    String.Format("{0:F2}",
                    Convert.ToSingle(txValor.Text) / 5.60f);
            }

            if (rb.Content.Equals("Pesos"))
            {
                lbValorConvertido.Text =
                    String.Format("{0:F2}",
                    Convert.ToSingle(txValor.Text) / 0.003276f);
            }


        }

        private void OnPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
        }

        private void btConverterPicker_Clicked(object sender, EventArgs e)
        {
            decimal amount = Convert.ToDecimal(txValor.Text);

            string from = pickFrom.SelectedItem as string;
            string to = pickTo.SelectedItem as string;

            decimal convertedValue = 
                currencyConverter.ConvertValue(amount, from, to);

            lbValorConvertidoPicker.Text =
                String.Format("{0:C2}", convertedValue);

        }
    }
}
