using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This class calls an API that returns currency rates and values
        /// </summary>
        public class Root
        {
            public Rate rates { get; set; } // Gets all records in raets and set
            public long timestamp;
            public string license;
        }

        /// <summary>
        /// This class contains a series of currency rates.
        /// </summary>
        public class Rate
        {
            /// <summary>
            /// Emitari Dirham
            /// </summary>
            public double AED { get; set; }
            /// <summary>
            /// Afghan Afghani
            /// </summary>
            public double AFN { get; set; }
            /// <summary>
            /// Albanian Lek
            /// </summary>
            public double ALL { get; set; }
            public double AMD { get; set; }
            public double ANG { get; set; }
            public double AOA { get; set; }
            public double ARS { get; set; }
            public double AUD { get; set; }
            public double AWG { get; set; }
            public double AZN { get; set; }
            public double BAM { get; set; }
            public double BBD { get; set; }
            public double BDT { get; set; }
            public double BGN { get; set; }
            public double BHD { get; set; }
            public double BIF { get; set; }
            public double BMD { get; set; }
            public double BND { get; set; }
            public double BOB { get; set; }
            public double BRL { get; set; }
            public double BSD { get; set; }
            public double BTC { get; set; }
            public double BTN { get; set; }
            public double BWP { get; set; }
            public double BYN { get; set; }
            public double BZD { get; set; }
            /// <summary>
            /// Canadian Dollar
            /// </summary>
            public double CAD { get; set; }
            public double CDF { get; set; }
            public double CHF { get; set; }
            public double CLF { get; set; }
            public double CLP { get; set; }
            public double CNH { get; set; }
            public double CNY { get; set; }
            public double COP { get; set; }
            public double CRC { get; set; }
            public double CUC { get; set; }
            public double CUP { get; set; }
            public double CVE { get; set; }
            /// <summary>
            /// Czech Koruna
            /// </summary>
            public double CZK { get; set; }
            public double DJF { get; set; }
            /// <summary>
            /// Danish Kronw
            /// </summary>
            public double DKK { get; set; }
            public double DOP { get; set; }
            public double DZD { get; set; }
            public double EGP { get; set; }
            public double ERN { get; set; }
            public double ETB { get; set; }
            /// <summary>
            /// Euro
            /// </summary>
            public double EUR { get; set; }

            public double FJD { get; set; }
            public double FKP { get; set; }
            public double GBP { get; set; }
            public double GEL { get; set; }
            public double GGP { get; set; }
            public double GHS { get; set; }
            public double GIP { get; set; }
            public double GMD { get; set; }
            public double GNF { get; set; }
            public double GTQ { get; set; }
            public double GYD { get; set; }
            public double HKD { get; set; }
            public double HNL { get; set; }
            public double HRK { get; set; }
            public double HTG { get; set; }
            public double HUF { get; set; }
            public double IDR { get; set; }
            public double ILS { get; set; }
            public double IMP { get; set; }
            /// <summary>
            /// Indian rupee
            /// </summary>
            public double INR { get; set; }
            public double IQD { get; set; }
            public double IRR { get; set; }
            /// <summary>
            /// Icelandic Krona
            /// </summary>
            public double ISK { get; set; }
            public double JEP { get; set; }
            public double JMD { get; set; }
            public double JOD { get; set; }

            /// <summary>
            /// Japanese Yen
            /// </summary>
            public double JPY { get; set; }
            public double KES { get; set; }
            public double KGS { get; set; }
            public double KHR { get; set; }
            public double KMF { get; set; }
            public double KPW { get; set; }
            public double KRW { get; set; }
            public double KWD { get; set; }
            public double KYD { get; set; }
            public double KZT { get; set; }
            public double LAK { get; set; }
            public double LBP { get; set; }
            public double LKR { get; set; }
            public double LRD { get; set; }
            public double LSL { get; set; }
            public double LYD { get; set; }
            public double MAD { get; set; }
            public double MDL { get; set; }
            public double MGA { get; set; }
            public double MKD { get; set; }
            public double MMK { get; set; }
            public double MNT { get; set; }
            public double MOP { get; set; }
            public double MRO { get; set; }
            public double MRU { get; set; }
            public double MUR { get; set; }
            public double MVR { get; set; }
            public double MWK { get; set; }
            public double MXN { get; set; }
            public double MYR { get; set; }
            public double MZN { get; set; }
            public double NAD { get; set; }
            public double NGN { get; set; }
            public double NIO { get; set; }
            public double NOK { get; set; }
            public double NPR { get; set; }

            /// <summary>
            /// New Zealand Dollar
            /// </summary>
            public double NZD { get; set; }

            public double OMR { get; set; }
            public double PAB { get; set; }
            public double PEN { get; set; }
            public double PGK { get; set; }
            /// <summary>
            /// Philippine Peso
            /// </summary>
            public double PHP { get; set; }
            public double PKR { get; set; }
            public double PLN { get; set; }
            public double PYG { get; set; }
            public double QAR { get; set; }
            public double RON { get; set; }
            public double RSD { get; set; }
            public double RUB { get; set; }
            public double RWF { get; set; }
            public double SAR { get; set; }
            public double SBD { get; set; }
            public double SCR { get; set; }
            public double SDG { get; set; }
            public double SEK { get; set; }
            public double SGD { get; set; }
            public double SHP { get; set; }
            public double SLL { get; set; }
            public double SOS { get; set; }
            public double SRD { get; set; }
            public double SSP { get; set; }
            public double STD { get; set; }
            public double STN { get; set; }
            public double SVC { get; set; }
            public double SYP { get; set; }
            public double SZL { get; set; }
            public double THB { get; set; }
            public double TJS { get; set; }
            public double TMT { get; set; }
            public double TND { get; set; }
            public double TOP { get; set; }
            public double TRY { get; set; }
            public double TTD { get; set; }
            public double TWD { get; set; }
            public double TZS { get; set; }
            public double UAH { get; set; }
            public double UGX { get; set; }
            /// <summary>
            /// US Dollar
            /// </summary>
            public double USD { get; set; }
            public double UYU { get; set; }
            public double UZS { get; set; }
            public double VES { get; set; }
            public double VND { get; set; }
            public double VUV { get; set; }
            public double WST { get; set; }
            public double XAF { get; set; }
            public double XAG { get; set; }
            public double XAU { get; set; }
            public double XCD { get; set; }
            public double XDR { get; set; }
            public double XOF { get; set; }
            public double XPD { get; set; }
            public double XPF { get; set; }
            public double XPT { get; set; }
            public double YER { get; set; }
            public double ZAR { get; set; }
            public double ZMW { get; set; }
            public double ZWL { get; set; }

        }

        //Create an object for SqlConnection (Used to establish a connection with a database)       
        SqlConnection con = new SqlConnection();

        //Create an object for SqlCommand (Used to run SQL Queries)
        SqlCommand cmd = new SqlCommand();

        //Create object for SqlDataAdapter (Used to communicate data in right format)
        SqlDataAdapter da = new SqlDataAdapter();

        private int CurrencyId = 0;             // Currency Id used for when using a Database as a data source.
        private double FromAmount = 0;          // Variable to store the rate from the To Currency Combo Box
        private double ToAmount = 0;            // Variable to store the rate from the To Currency Combo Box
        private double convertedValue;          // Variable used to store the converted currency value.

        /// <summary>
        /// This Root object is used to store the response from the GetDataFromAPI(string url) method.
        /// </summary>
        Root val = new Root();

        public MainWindow()
        {
            InitializeComponent();
            PopulateDataSourceComboBox();
            GetCurrencyValues();
            BindCurrency(dataSourceCBox.Text);
            // Show data from the database into Currency Master
            GetData();

            // Added Open exchange Rates API to get real time currency rates.
            //Created new class Root & Rate
        }

        /// <summary>
        /// This method assigns the Root Object converted from the http request in the GetDataFromAPI method
        /// to the val Root Object used to store the currency rates, timestamps and license.
        /// </summary>
        private async void GetCurrencyValues()
        {
            val = await GetDataFromAPI<Root>("https://openexchangerates.org/api/latest.json?app_id=bc0282de451246838fdd2bea4aaa3152");
            //BindCurrency(dataSourceCBox.Text);
        }

        /// <summary>
        /// This asynchronous method makes an http request to the Open exchange rates API.
        /// If the request is successful, it converts the response content into a string and
        /// maps the result string into the Root Class JSON Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<Root> GetDataFromAPI<T> (string url)
        {
            var myRoot = new Root();
            try
            {
                // The HttpClient class provides a base class for sending and receiving http requests
                using (var client = new HttpClient())
                {
                    // The timespan to wait before the request times out.
                    client.Timeout = TimeSpan.FromMinutes(1);

                    // Variable to await the http response message from the API
                    HttpResponseMessage response = await client.GetAsync(url);

                    // If the status returns Ok (200)
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Serialize the HTTP content to a string
                        var ResponseString = await response.Content.ReadAsStringAsync();

                        // Convert the response string into a JSON Object
                        var ResponseObject = JsonConvert.DeserializeObject<Root>(ResponseString);

                        // MessageBox.Show("TimeStamp: " + ResponseObject.timestamp, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        return ResponseObject;
                    }
                    return myRoot;
                }
            }
            catch 
            {
                return myRoot;
            }
        }

        /// <summary>
        /// Method used to establish a Database connection.
        /// </summary>
        public void MyConnection()
        {
            try
            {
                // Database connection string
                // Added System.Configuration to references in order to use Configuration Manager.
                String Conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(Conn);
                con.Open(); //Connection Open
            }
            catch (Exception e)
            {
                MessageBox.Show("A connection to the database could not be established.\nPlease set up a database and create a table using the queries provided in queries.txt. \nAfter this, provide a valid connection string to the MyConnection method., prov", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Method used to bind and display values to my combo boxes.
        /// </summary>
        private void BindCurrency(string DataSource)
        {
            if(DataSource == "API")
            {
                BindHttpData();
                ClearControls();
            } else if(DataSource == "Database")
            {
                try
                {
                    BindDatabaseData();
                    // Show data from the database into Currency Master
                    GetData();
                    ClearControls();
                } catch (Exception e)
                {
                    MessageBox.Show("A connection to the database could not be established.\nPlease set up a database and create a table using the queries provided in queries.txt. \nAfter this, provide a valid connection string to the MyConnection method., prov","Database Connection Error",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            else if(DataSource == "Static Data")
            {
                BindStaticData();
                ClearControls();
            }

            

        }

        /// <summary>
        /// This method is executed whenever the Convert button is clicked. It will convert my currency 
        /// from one specified currency to another specified currency. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            

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
                FromAmount = double.Parse(cmbFromCurrency.SelectedValue.ToString());
                ToAmount = double.Parse(cmbToCurrency.SelectedValue.ToString());
                convertedValue = ToAmount * double.Parse(txtCurrency.Text) / FromAmount;

                

                // Show the label converted currency name and the value of the currency in the same line.
                // The N3 parameter in the ToString method is used to format the string and place 000 after a dot (.)
                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N5");
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
            dtCurrency.Rows.Add("USD - US Dollar", 1);
            dtCurrency.Rows.Add("EUR - Euro", 0.86);
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
            cmd = new SqlCommand("SELECT Id, Amount, CurrencyName FROM Currency_Master", con);

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
            cmbFromCurrency.SelectedValuePath = "Amount";

            //Show default item in combobox
            cmbFromCurrency.SelectedValue = 0;

            // Doing the same for the cmbToCurrency comboBox
            cmbToCurrency.DisplayMemberPath = "CurrencyName";
            cmbToCurrency.SelectedValuePath = "Amount";
            cmbToCurrency.SelectedValue = 0;
        }

        /// <summary>
        /// Method is used to bind hardcoded currency data into our 
        /// currency comboboxes using hardcoded values.
        /// </summary>
        private void BindHttpData()
        {
            // using System.Data
            // Creating new data table.
            DataTable dt = new DataTable();

            // Adding columns to the data table. (Text, Value)
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");

            // Add rows to the datatable with text and value (currency exchange rate).
            dt.Rows.Add("-- SELECT --", 0);
            dt.Rows.Add("AED", val.rates.AED);
            dt.Rows.Add("AFN", val.rates.AFN);
            dt.Rows.Add("ALL", val.rates.ALL);
            dt.Rows.Add("AMD", val.rates.AMD);
            dt.Rows.Add("ANG", val.rates.ANG);
            dt.Rows.Add("AOA", val.rates.AOA);
            dt.Rows.Add("ARS", val.rates.ARS);
            dt.Rows.Add("AUD", val.rates.AUD);
            dt.Rows.Add("AWG", val.rates.AWG);
            dt.Rows.Add("AZN", val.rates.AZN);
            dt.Rows.Add("BAM", val.rates.BAM);
            dt.Rows.Add("BBD", val.rates.BBD);
            dt.Rows.Add("BDT", val.rates.BDT);
            dt.Rows.Add("BGN", val.rates.BGN);
            dt.Rows.Add("BHD", val.rates.BHD);
            dt.Rows.Add("BIF", val.rates.BIF);
            dt.Rows.Add("BMD", val.rates.BMD);
            dt.Rows.Add("BND", val.rates.BND);
            dt.Rows.Add("BOB", val.rates.BOB);
            dt.Rows.Add("BRL", val.rates.BRL);
            dt.Rows.Add("BSD", val.rates.BSD);
            dt.Rows.Add("BTC", val.rates.BTC);
            dt.Rows.Add("BTN", val.rates.BTN);
            dt.Rows.Add("BWP", val.rates.BWP);
            dt.Rows.Add("BYN", val.rates.BYN);
            dt.Rows.Add("BZD", val.rates.BZD);
            dt.Rows.Add("CAD", val.rates.CAD);
            dt.Rows.Add("CDF", val.rates.CDF);
            dt.Rows.Add("CHF", val.rates.CHF);
            dt.Rows.Add("CLF", val.rates.CLF);
            dt.Rows.Add("CLP", val.rates.CLP);
            dt.Rows.Add("CNH", val.rates.CNH);
            dt.Rows.Add("CNY", val.rates.CNY);
            dt.Rows.Add("COP", val.rates.COP);
            dt.Rows.Add("CRC", val.rates.CRC);
            dt.Rows.Add("CUC", val.rates.CUC);
            dt.Rows.Add("CUP", val.rates.CUP);
            dt.Rows.Add("CVE", val.rates.CVE);
            dt.Rows.Add("CZK", val.rates.CZK);
            dt.Rows.Add("DJF", val.rates.DJF);
            dt.Rows.Add("DKK", val.rates.DKK);
            dt.Rows.Add("DOP", val.rates.DOP);
            dt.Rows.Add("DZD", val.rates.DZD);
            dt.Rows.Add("EGP", val.rates.EGP);
            dt.Rows.Add("ERN", val.rates.ERN);
            dt.Rows.Add("ETB", val.rates.ETB);
            dt.Rows.Add("EUR", val.rates.EUR);
            dt.Rows.Add("FJD", val.rates.FJD);
            dt.Rows.Add("FKP", val.rates.FKP);
            dt.Rows.Add("GBP", val.rates.GBP);
            dt.Rows.Add("GEL", val.rates.GEL);
            dt.Rows.Add("GGP", val.rates.GGP);
            dt.Rows.Add("GHS", val.rates.GHS);
            dt.Rows.Add("GIP", val.rates.GIP);
            dt.Rows.Add("GMD", val.rates.GMD);
            dt.Rows.Add("GNF", val.rates.GNF);
            dt.Rows.Add("GTQ", val.rates.GTQ);
            dt.Rows.Add("GYD", val.rates.GYD);
            dt.Rows.Add("HKD", val.rates.HKD);
            dt.Rows.Add("HNL", val.rates.HNL);
            dt.Rows.Add("HRK", val.rates.HRK);
            dt.Rows.Add("HTG", val.rates.HTG);
            dt.Rows.Add("HUF", val.rates.HUF);
            dt.Rows.Add("IDR", val.rates.IDR);
            dt.Rows.Add("ILS", val.rates.ILS);
            dt.Rows.Add("IMP", val.rates.IMP);
            dt.Rows.Add("INR", val.rates.INR);
            dt.Rows.Add("IQD", val.rates.IQD);
            dt.Rows.Add("IRR", val.rates.IRR);
            dt.Rows.Add("ISK", val.rates.ISK);
            dt.Rows.Add("JEP", val.rates.JEP);
            dt.Rows.Add("JMD", val.rates.JMD);
            dt.Rows.Add("JOD", val.rates.JOD);
            dt.Rows.Add("JPY", val.rates.JPY);
            dt.Rows.Add("KES", val.rates.KES);
            dt.Rows.Add("KGS", val.rates.KGS);
            dt.Rows.Add("KHR", val.rates.KHR);
            dt.Rows.Add("KMF", val.rates.KMF);
            dt.Rows.Add("KPW", val.rates.KPW);
            dt.Rows.Add("KRW", val.rates.KRW);
            dt.Rows.Add("KWD", val.rates.KWD);
            dt.Rows.Add("KYD", val.rates.KYD);
            dt.Rows.Add("KZT", val.rates.KZT);
            dt.Rows.Add("LAK", val.rates.LAK);
            dt.Rows.Add("LBP", val.rates.LBP);
            dt.Rows.Add("LKR", val.rates.LKR);
            dt.Rows.Add("LRD", val.rates.LRD);
            dt.Rows.Add("LSL", val.rates.LSL);
            dt.Rows.Add("LYD", val.rates.LYD);
            dt.Rows.Add("MAD", val.rates.MAD);
            dt.Rows.Add("MDL", val.rates.MDL);
            dt.Rows.Add("MGA", val.rates.MGA);
            dt.Rows.Add("MKD", val.rates.MKD);
            dt.Rows.Add("MMK", val.rates.MMK);
            dt.Rows.Add("MNT", val.rates.MNT);
            dt.Rows.Add("MOP", val.rates.MOP);
            dt.Rows.Add("MRO", val.rates.MRO);
            dt.Rows.Add("MRU", val.rates.MRU);
            dt.Rows.Add("MUR", val.rates.MUR);
            dt.Rows.Add("MVR", val.rates.MVR);
            dt.Rows.Add("MWK", val.rates.MWK);
            dt.Rows.Add("MXN", val.rates.MXN);
            dt.Rows.Add("MYR", val.rates.MYR);
            dt.Rows.Add("MZN", val.rates.MZN);
            dt.Rows.Add("NAD", val.rates.NAD);
            dt.Rows.Add("NGN", val.rates.NGN);
            dt.Rows.Add("NIO", val.rates.NIO);
            dt.Rows.Add("NOK", val.rates.NOK);
            dt.Rows.Add("NPR", val.rates.NPR);
            dt.Rows.Add("NZD", val.rates.NZD);
            dt.Rows.Add("OMR", val.rates.OMR);
            dt.Rows.Add("PAB", val.rates.PAB);
            dt.Rows.Add("PEN", val.rates.PEN);
            dt.Rows.Add("PGK", val.rates.PGK);
            dt.Rows.Add("PHP", val.rates.PHP);
            dt.Rows.Add("PKR", val.rates.PKR);
            dt.Rows.Add("PLN", val.rates.PLN);
            dt.Rows.Add("PYG", val.rates.PYG);
            dt.Rows.Add("QAR", val.rates.QAR);
            dt.Rows.Add("RON", val.rates.RON);
            dt.Rows.Add("RSD", val.rates.RSD);
            dt.Rows.Add("RUB", val.rates.RUB);
            dt.Rows.Add("RWF", val.rates.RWF);
            dt.Rows.Add("SAR", val.rates.SAR);
            dt.Rows.Add("SBD", val.rates.SBD);
            dt.Rows.Add("SCR", val.rates.SCR);
            dt.Rows.Add("SDG", val.rates.SDG);
            dt.Rows.Add("SEK", val.rates.SEK);
            dt.Rows.Add("SGD", val.rates.SGD);
            dt.Rows.Add("SHP", val.rates.SHP);
            dt.Rows.Add("SLL", val.rates.SLL);
            dt.Rows.Add("SOS", val.rates.SOS);
            dt.Rows.Add("SRD", val.rates.SRD);
            dt.Rows.Add("SSP", val.rates.SSP);
            dt.Rows.Add("STD", val.rates.STD);
            dt.Rows.Add("STN", val.rates.STN);
            dt.Rows.Add("SVC", val.rates.SVC);
            dt.Rows.Add("SYP", val.rates.SYP);
            dt.Rows.Add("SZL", val.rates.SZL);
            dt.Rows.Add("THB", val.rates.THB);
            dt.Rows.Add("TJS", val.rates.TJS);
            dt.Rows.Add("TMT", val.rates.TMT);
            dt.Rows.Add("TND", val.rates.TND);
            dt.Rows.Add("TOP", val.rates.TOP);
            dt.Rows.Add("TRY", val.rates.TRY);
            dt.Rows.Add("TTD", val.rates.TTD);
            dt.Rows.Add("TWD", val.rates.TWD);
            dt.Rows.Add("TZS", val.rates.TZS);
            dt.Rows.Add("UAH", val.rates.UAH);
            dt.Rows.Add("UGX", val.rates.UGX);
            dt.Rows.Add("USD", val.rates.USD);
            dt.Rows.Add("UYU", val.rates.UYU);
            dt.Rows.Add("UZS", val.rates.UZS);
            dt.Rows.Add("VES", val.rates.VES);
            dt.Rows.Add("VND", val.rates.VND);
            dt.Rows.Add("VUV", val.rates.VUV);
            dt.Rows.Add("WST", val.rates.WST);
            dt.Rows.Add("XAF", val.rates.XAF);
            dt.Rows.Add("XAG", val.rates.XAG);
            dt.Rows.Add("XAU", val.rates.XAU);
            dt.Rows.Add("XCD", val.rates.XCD);
            dt.Rows.Add("XDR", val.rates.XDR);
            dt.Rows.Add("XOF", val.rates.XOF);
            dt.Rows.Add("XPD", val.rates.XPD);
            dt.Rows.Add("XPF", val.rates.XPF);
            dt.Rows.Add("XPT", val.rates.XPT);
            dt.Rows.Add("YER", val.rates.YER);
            dt.Rows.Add("ZAR", val.rates.ZAR);
            dt.Rows.Add("ZMW", val.rates.ZMW);
            dt.Rows.Add("ZWL", val.rates.ZWL);



            // Binding our DataTable to the ComboBox in the combo boxes
            cmbFromCurrency.ItemsSource = dt.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text"; // What will be displayed in the dropdown combo box.
            cmbFromCurrency.SelectedValuePath = "Value"; // What value will be internally given when something is selected.
            cmbFromCurrency.SelectedIndex = 0; // Start our selection at index 0 

            cmbToCurrency.ItemsSource = dt.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text"; // What will be displayed in the dropdown combo box.
            cmbToCurrency.SelectedValuePath = "Value"; // What value will be internally given when something is selected.
            cmbToCurrency.SelectedIndex = 0; // Start our selection at index 0 
        }

        /// <summary>
        /// This method clears out fields from the Currency master tab.
        /// </summary>
        private void ClearMaster()
        {
            try
            {
                txtAmount.Text = string.Empty;
                txtCurrencyName.Text = string.Empty;
                btnSave.Content = "Save";
                GetData();
                CurrencyId = 0;
                BindCurrency(dataSourceCBox.Text);
                txtAmount.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method is used to bind the database data to the dataGrid view in the Currency_Masters Tab.
        /// </summary>
        public void GetData()
        {
            //The method is used for connect with database and open database connection    
            MyConnection();

            //Create Datatable object
            DataTable dt = new DataTable();

            //Write Sql Query for Get data from database table. Query written in double quotes and after comma provide connection    
            cmd = new SqlCommand("SELECT * FROM Currency_Master", con);

            //CommandType define Which type of command execute like Text, StoredProcedure, TableDirect.    
            cmd.CommandType = CommandType.Text;

            //It is accept a parameter that contains the command text of the object's SelectCommand property.
            da = new SqlDataAdapter(cmd);

            //The DataAdapter serves as a bridge between a DataSet and a data source for retrieving and saving data. The Fill operation then adds the rows to destination DataTable objects in the DataSet    
            da.Fill(dt);

            //dt is not null and rows count greater than 0
            if (dt != null && dt.Rows.Count > 0)
            {
                //Assign DataTable data to dgvCurrency using ItemSource property.   
                dgvCurrency.ItemsSource = dt.DefaultView;
            }
            else
            {
                dgvCurrency.ItemsSource = null;
            }
            //Database connection Close
            con.Close();
        }

        /// <summary>
        /// This method runs when the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Since databases are involved, will use try catch blocks
            try
            {
                // If text amount or currency name is empty, display an error message.
                if(txtAmount.Text == null || txtAmount.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter an amount", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtAmount.Focus();
                    return;
                } else if (txtCurrencyName.Text == null || txtCurrencyName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a currency name", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtCurrencyName.Focus();
                    return;
                } else
                {
                    // Check if we are not trying to edit the default record in the combo box which contains the "-- Select --" text.
                    if (CurrencyId > 0)
                    {
                        // If the user wants to update a record and selects yes.
                        if (MessageBox.Show("Are you sure you want to update this record?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            // Open a database connection.
                            MyConnection();

                            // Create a new DataTable Object to store the data received.
                            DataTable dt = new DataTable();

                            // Create an update query to update the data in Currency_Master Table. Run this query in our connection.
                            cmd = new SqlCommand("UPDATE Currency_Master SET Amount = @Amount, CurrencyName = @CurrencyName WHERE Id = @Id", con);

                            cmd.CommandType = CommandType.Text;

                            // Updating parameter values for query.
                            cmd.Parameters.AddWithValue("@Id", CurrencyId);
                            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                            cmd.Parameters.AddWithValue("@CurrencyName", txtCurrencyName.Text);
                            cmd.ExecuteNonQuery();

                            // Close the database connection
                            con.Close();

                            // Display an update successful message
                            MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else // Save Button code
                    {
                        if (MessageBox.Show("Are you sure you want to save?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            // Open a database connection.
                            MyConnection();

                            // Create a new DataTable Object to store the data received.
                            DataTable dt = new DataTable();

                            // Create an insert query to add data in Currency_Master Table. Run this query in our connection.
                            cmd = new SqlCommand("INSERT INTO Currency_Master(Amount,CurrencyName) VALUES(@Amount,@CurrencyName)", con);

                            cmd.CommandType = CommandType.Text;

                            // Updating parameter values for query.
                            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                            cmd.Parameters.AddWithValue("@CurrencyName", txtCurrencyName.Text);
                            cmd.ExecuteNonQuery();

                            // Close the database connection
                            con.Close();

                            // Display an update successful message
                            MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    ClearMaster();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method runs when the cancel button is clicked. It executes the ClearMaster() method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearMaster();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method triggers when a data grid cell is selected. In this data grid we have 2 icons.
        /// The edit and delete icon. When the edit icon is selected, the save button changes into update.
        /// When the delete icon is selected, a record is deleted from the data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //Creating object for DataGrid. Done by parsing the sender object.
                DataGrid grd = (DataGrid)sender;
                //Creating object for DataRowView using the grid current item row selected
                DataRowView row_selected = grd.CurrentItem as DataRowView;

                // if the row_selected is not null (It's not an empty row)
                if (row_selected != null)
                {
                    // If the Currency DataGrid items count is greater than zero (If there are items in the data grid)
                    if (dgvCurrency.Items.Count > 0)
                    {
                        // If the Data grid selected cell is greater than 0 (If we selected a cell)
                        if (grd.SelectedCells.Count > 0)
                        {
                            //Get selected row Id column value and assign it to the CurrencyId variable
                            CurrencyId = Int32.Parse(row_selected["Id"].ToString());

                            // if the DisplayIndex is equal to zero then the Edit cell is selected
                            if (grd.SelectedCells[0].Column.DisplayIndex == 0)
                            {
                                // Get the selected row Amount column value and Set it to the Amount textbox value
                                txtAmount.Text = row_selected["Amount"].ToString();

                                // Get the selected row CurrencyName column value and Set it to CurrencyName textbox value
                                txtCurrencyName.Text = row_selected["CurrencyName"].ToString();

                                // Change save button text from Save to Update
                                btnSave.Content = "Update";
                            }

                            // IF DisplayIndex is equal to one then it's the Delete cell                    
                            if (grd.SelectedCells[0].Column.DisplayIndex == 1)
                            {
                                
                                //Show confirmation dialogue box
                                if (MessageBox.Show("Are you sure you want to delete ?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                {
                                    // Open a database connection
                                    MyConnection();
                                    DataTable dt = new DataTable();

                                    //Execute delete query for delete record from table using Id
                                    cmd = new SqlCommand("DELETE FROM Currency_Master WHERE Id = @Id", con);
                                    cmd.CommandType = CommandType.Text;

                                    //CurrencyId set in @Id parameter and send it in delete statement
                                    cmd.Parameters.AddWithValue("@Id", CurrencyId);
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                    MessageBox.Show("Data deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                    ClearMaster();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method calls the Convert Currency method when both combo boxes have valid currencies to convert to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFromCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If currencies have been selected, convert currency automatically.
            //if (cmbFromCurrency.SelectedIndex != 0 && cmbToCurrency.SelectedIndex != 0 && cmbFromCurrency.Text != "-- SELECT --" && cmbToCurrency.Text != "-- SELECT --")
            //    Convert_Click(sender, e);
        }

        /// <summary>
        /// This method calls the Convert Currency method when both combo boxes have valid currencies to convert to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbToCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If currencies have been selected, convert currency automatically.
            //if (cmbFromCurrency.SelectedIndex != 0 && cmbToCurrency.SelectedIndex != 0 && cmbFromCurrency.Text != "-- SELECT --" && cmbToCurrency.Text != "-- SELECT --")
            //    Convert_Click(sender, e);
        }

        /// <summary>
        /// This method is executed when the Data Source combo box value has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataSourceCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindCurrency(dataSourceCBox.SelectedValue.ToString());
        }

        /// <summary>
        /// This method gives the options to the combo box.
        /// </summary>
        private void PopulateDataSourceComboBox()
        {
            // using System.Data
            // Creating new data table.
            DataTable dtDataSource = new DataTable();

            // Adding columns to the data table. (Text, Value)
            dtDataSource.Columns.Add("Text");


            // Add rows to the datatable with text and value (currency exchange rate).
            dtDataSource.Rows.Add("Static Data");
            dtDataSource.Rows.Add("Database");
            dtDataSource.Rows.Add("API");
            
            
            // Binding our DataTable to the ComboBox in the combo boxes
            dataSourceCBox.ItemsSource = dtDataSource.DefaultView;
            dataSourceCBox.DisplayMemberPath = "Text"; // What will be displayed in the dropdown combo box.
            dataSourceCBox.SelectedValuePath = "Text"; // What value will be internally given when something is selected.
            dataSourceCBox.SelectedIndex = 0; // Start our selection at index 0 
        }
    }
}
