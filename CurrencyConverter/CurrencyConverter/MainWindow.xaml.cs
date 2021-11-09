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

        private int CurrencyId = 0;
        private double FromAmount = 0;
        private double ToAmount = 0;

        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
            GetData();
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
        /// Method used to bind and display values to my combo boxes.
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

        /// <summary>
        /// This method is git p
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
                BindCurrency();
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
    }
}
