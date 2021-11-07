using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
        }


        /// <summary>
        /// Method used to bind values to my combo boxes.
        /// </summary>
        private void BindCurrency()
        {
            // using System.Data
            // Creating new data table.
            DataTable dtCurrency = new DataTable();

            // Adding columns to the data table. (Text, Value)
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            // Add rows to the datatable with text and value (currency exchange rate).
            dtCurrency.Rows.Add("-- SELECT --", 0);
            dtCurrency.Rows.Add("INR - Indian Rupee", 1);
            dtCurrency.Rows.Add("USD - US Dollar", 75);
            dtCurrency.Rows.Add("EUR - Euro", 85);
            dtCurrency.Rows.Add("SAR - Saudi Arabian Riyal", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM - German Deutsche Mark", 43);

            // Binding our DataTable to the ComboBox in the combo boxes
            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text"; // What will be displayed in the dropdown combo box.
            cmbFromCurrency.SelectedValuePath = "Value"; // What value will be internally given when something is selected.
            cmbFromCurrency.SelectedIndex = 0; // Start our selection at index 0 

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text"; // What will be displayed in the dropdown combo box.
            cmbToCurrency.SelectedValuePath = "Value"; // What value will be internally given when something is selected.
            cmbToCurrency.SelectedIndex = 0; // Start our selection at index 0 
        }

        /// <summary>
        /// This method is executed whenever the Convert button is clicked. It will convert my currency 
        /// from one specified currency to another specified currency. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            // Variable used to store the converted currency value.
            double convertedValue;

            // If amount input textBox value is null, display a message prompting user to enter an amount.
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a currency value.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // After clicking ok on the message box, focus on amount textbox.
                txtCurrency.Focus();
                return;
            } // If Convert Currency FROM combobox has no value, prompt user to select a currency to convert from. 
            else if(cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a currency to convert from.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // After clicking ok on the message box, focus on currencyFom combobox.
                cmbFromCurrency.Focus();
            } // If Convert Currency TO combobox has no value, prompt user to select a currency to convert to.
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a currency to convert to.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // After clicking ok on the message box, focus on currencyFom combobox.
                cmbToCurrency.Focus();
            } 

            // If the FromCurrency and ToCurrency combobox have the same value, 
            if(cmbFromCurrency.SelectedValue == cmbToCurrency.SelectedValue)
            {
                // There is nothing to convert to. Just assign the same value
                // double.parse will convert the string input into a double
                convertedValue = double.Parse(txtCurrency.Text);

                // Show the label converted currency name and the value of the currency in the same line.
                // The N3 parameter in the ToString method is used to format the string and place 000 after a dot (.)
                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            } else
            {
                // Convert / Calculate the currency
                // ("Currency to convert from" rate * Amount) / "Currency to convert to" rate
                convertedValue = double.Parse(cmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text) / double.Parse(cmbToCurrency.SelectedValue.ToString());

                

                // Show the label converted currency name and the value of the currency in the same line.
                // The N3 parameter in the ToString method is used to format the string and place 000 after a dot (.)
                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }

        }

        /// <summary>
        /// This click method calls the clearControls method which resets every value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        /// <summary>
        /// Function that validates if input is a number using Regex. 
        /// (Used to filter out keystrokes that are not numbers)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // using System.Text.RegularExpressions
            Regex regex = new Regex("[^0-9]+");
            // If the text matches the regular expression (numbers only), it can be entered
            e.Handled = regex.IsMatch(e.Text);

        }

        /// <summary>
        /// This method clears every value in the textbox and comboboxes.
        /// </summary>
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;

            lblCurrency.Content = "";
            txtCurrency.Focus();
        }
    }
}
