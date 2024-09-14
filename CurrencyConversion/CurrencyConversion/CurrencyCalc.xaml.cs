using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CurrencyConversion
{
    /// <summary>
    /// MainPage of the Currency Conversion application.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate and parse the amount
                if (!double.TryParse(AmountTextBox.Text, out double amount))
                {
                    ResultTextBlock.Text = "Invalid amount. Please enter a numeric value.";
                    return;
                }

                // Get selected currencies
                string fromCurrency = ((ComboBoxItem)FromCurrencyComboBox.SelectedItem)?.Content.ToString().Split('-')[0].Trim();
                string toCurrency = ((ComboBoxItem)ToCurrencyComboBox.SelectedItem)?.Content.ToString().Split('-')[0].Trim();

                // Check if both currencies are selected
                if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
                {
                    ResultTextBlock.Text = "Please select both currencies.";
                    return;
                }

                // Fetch the conversion rate
                double conversionRate = FetchConversionRate(fromCurrency, toCurrency);

                // Check if the conversion rate is valid
                if (conversionRate <= 0)
                {
                    ResultTextBlock.Text = "Invalid conversion rate.";
                    return;
                }

                // Perform conversion
                double convertedAmount = amount * conversionRate;
                ResultTextBlock.Text = $"{amount} {fromCurrency} converts to {convertedAmount:0.00} {toCurrency}";
            }
            catch (Exception ex)
            {
                // Display error message
                ResultTextBlock.Text = $"An error occurred: {ex.Message}";
            }
        }

        private double FetchConversionRate(string fromCurrency, string toCurrency)
        {
            // Define conversion rates based on the provided rates
            // Conversion rates are specified as fromCurrency -> toCurrency

            // USD Rates
            if (fromCurrency == "USD")
            {
                switch (toCurrency)
                {
                    case "EUR": return 0.85189982;
                    case "GBP": return 0.72872436;
                    case "INR": return 74.257327;
                }
            }
            // EUR Rates
            if (fromCurrency == "EUR")
            {
                switch (toCurrency)
                {
                    case "USD": return 1.1739732;
                    case "GBP": return 0.8556672;
                    case "INR": return 87.00755;
                }
            }
            // GBP Rates
            if (fromCurrency == "GBP")
            {
                switch (toCurrency)
                {
                    case "USD": return 1.371907;
                    case "EUR": return 1.1686692;
                    case "INR": return 101.68635;
                }
            }
            // INR Rates
            if (fromCurrency == "INR")
            {
                switch (toCurrency)
                {
                    case "USD": return 0.011492628;
                    case "EUR": return 0.013492774;
                    case "GBP": return 0.0098339397;
                }
            }

            // If no rate is found, return a default value of 0
            return 0;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit(); // This line will need to be adjusted or removed
        }
    }
}
