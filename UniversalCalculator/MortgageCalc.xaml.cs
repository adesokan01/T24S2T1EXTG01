using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalc : Page
	{
		public MortgageCalc()
		{
			this.InitializeComponent();
		}

		private async void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			double principalBorrow;
			double yearlyInterestRate;
			int numOfMonths;
			int numOfYears;

			try
			{
				// attempt to parse user input
				principalBorrow = double.Parse(principalBorrowTextBox.Text);
				yearlyInterestRate = double.Parse(yearlyInterestRateTextBox.Text);
				numOfMonths = int.Parse(monthsTextBox.Text);
				numOfYears = int.Parse(yearsTextBox.Text);
			}
			catch
			{
				// alert user of input error
				var dialog = new MessageDialog("Please input valid number amounts for all fields.");
				await dialog.ShowAsync();

				return;
			}

			int totalNumMonths = numOfMonths + numOfYears * 12;

			double monthlyInterestRate;
			double monthlyRepayment;

			// calculate monthly interest rate
			monthlyInterestRate = yearlyInterestRate / 100 / 12;

			// calculate monthly repayment
			// monthlyRepayment = principalBorrow * monthlyInterestRate * Math.Pow((1 + monthlyInterestRate), totalNumMonths) / (Math.Pow((1 + monthlyInterestRate), totalNumMonths) - 1);

			monthlyRepayment = calculateLoanRepayment(principalBorrow, monthlyInterestRate, totalNumMonths);

			// display monthly interest rate and monthly repayment
			monthlyInterestRateTextBox.Text = monthlyInterestRate.ToString("F4") + "%";
			monthlyRepaymentTextBox.Text = monthlyRepayment.ToString("F2");
		}

		private double calculateLoanRepayment(double principal, double monthlyRate, int months)
		{
			return principal * monthlyRate * Math.Pow((1 + monthlyRate), months) / (Math.Pow((1 + monthlyRate), months) - 1);
		}
	}
}
