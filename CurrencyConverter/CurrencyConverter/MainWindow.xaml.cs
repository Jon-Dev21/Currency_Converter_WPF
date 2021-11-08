using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        //Create an object for SqlConnection (Used to establish a connection with a database)       
        SqlConnection con = new SqlConnection();

        //Create an object for SqlCommand (Used to run SQL Queries)
        SqlCommand cmd = new SqlCommand();

        //Create object for SqlDataAdapter (Used to communicate data in right format)
        SqlDataAdapter da = new SqlDataAdapter();

        private int currentId = 0;
        private double FromAmount = 0;
        private double ToAmount = 0;

        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
        }

        /// <summary>
        /// Method used to establish a Database connection.
        /// </summary>
        public void MyConnection()
        {
            // Database connection string
            // Added System.Configuration to references in order to use Configuration Manager.
            String Conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(Conn);
            con.Open(); //Connection Open
        }

        /// <summary>
        /// Method used to bind values to my combo boxes.
        /// </summary>
        private void BindCurrency()
        {
            // BindStaticData();
            BindDatabaseData();
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

        /// <summary>
        /// Method is used to bind hardcoded currency data into our 
        /// currency comboboxes using hardcoded values.
        /// </summary>
        private void BindStaticData()
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
        /// Method is used to bind currency data into our 
        /// currency comboboxes using database values.
        /// </summary>
        private void BindDatabaseData()
        {
            // Open database connection.
            MyConnection();

            // Create a new DataTable Object to store the data received.
            DataTable dt = new DataTable();

            // Create a select statement query to get data from Currency_Master Table. Run this query in our connection.
            cmd = new SqlCommand("SELECT Id, CurrencyName FROM Currency_Master", con);

            cmd.CommandType = CommandType.Text;

            //It accepts a parameter that contains the command text of the object's selectCommand property.
            da = new SqlDataAdapter(cmd);

            // Filling data table with data obtained from the query.
            da.Fill(dt);


            // Now Will be creating the first row for the table where the -- SELECT -- is displayed. 
            // Similar to how BindStaticData() assigns this value to the comboBox.
            // Creating an object for DataRow
            DataRow newRow = dt.NewRow();

            //Assigning a value to Id column
            newRow["Id"] = 0;

            //Assigning value to CurrencyName column
            newRow["CurrencyName"] = "-- SELECT --";

            //Inserting a new row in dt with the data at a 0 position
            dt.Rows.InsertAt(newRow, 0);


            // If the data table is not null or empty
            if (dt != null && dt.Rows.Count > 0)
            {
                //Assign the datatable data to from currency combobox using ItemSource property.
                cmbFromCurrency.ItemsSource = dt.DefaultView;

                //Assign the datatable data to to currency combobox using ItemSource property.
                cmbToCurrency.ItemsSource = dt.DefaultView;
            }

            // Close database connection
            con.Close();


            //To display the underlying datasource for cmbFromCurrency
            cmbFromCurrency.DisplayMemberPath = "CurrencyName";

            //To use as the actual value for the items
            cmbFromCurrency.SelectedValuePath = "Id";

            //Show default item in combobox
            cmbFromCurrency.SelectedValue = 0;

            // Doing the same for the cmbToCurrency comboBox
            cmbToCurrency.DisplayMemberPath = "CurrencyName";
            cmbToCurrency.SelectedValuePath = "Id";
            cmbToCurrency.SelectedValue = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Method triggers when a data grid cell is selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
