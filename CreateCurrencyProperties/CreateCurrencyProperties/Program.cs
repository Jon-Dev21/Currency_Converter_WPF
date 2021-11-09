﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCurrencyProperties
{
    /// <summary>
    /// This program creates all the properties for currencies.
    /// Output copied in order to use it for the Rate class in my Currency Converter WPF app.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string[] currency = { "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BRL", "BSD", "BTC", "BTN", "BWP", "BYN", "BZD", "CAD", "CDF", "CHF", "CLF", "CLP", "CNH", "CNY", "COP", "CRC", "CUC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "EGP", "ERN", "ETB", "EUR", "FJD", "FKP", "GBP", "GEL", "GGP", "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF", "IDR", "ILS", "IMP", "INR", "IQD", "IRR", "ISK", "JEP", "JMD", "JOD", "JPY", "KES", "KGS", "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRO", "MRU", "MUR", "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RUB", "RWF", "SAR", "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLL", "SOS", "SRD", "SSP", "STD", "STN", "SVC", "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD", "TZS", "UAH", "UGX", "USD", "UYU", "UZS", "VES", "VND", "VUV", "WST", "XAF", "XAG", "XAU", "XCD", "XDR", "XOF", "XPD", "XPF", "XPT", "YER", "ZAR", "ZMW", "ZWL" };
            string[] queryValues = { "\'AED\',3.6732", "\'AFN\',90.89653", "\'ALL\',106.120767", "\'AMD\',476.451695", "\'ANG\',1.800934", "\'AOA\',597", "\'ARS\',100.0274", "\'AUD\',1.351241", "\'AWG\',1.801", "\'AZN\',1.700805", "\'BAM\',1.688943", "\'BBD\',2", "\'BDT\',85.682032", "\'BGN\',1.6883", "\'BHD\',0.376987", "\'BIF\',1988.661874", "\'BMD\',1", "\'BND\',1.347645", "\'BOB\',6.88995", "\'BRL\',5.5445", "\'BSD\',1", "\'BTC\',0.000015051971", "\'BTN\',74.00754", "\'BWP\',11.335987", "\'BYN\',2.443999", "\'BZD\',2.014214", "\'CAD\',1.24555", "\'CDF\',2002.655789", "\'CHF\',0.912397", "\'CLF\',0.029126", "\'CLP\',803.67", "\'CNH\',6.393937", "\'CNY\',6.3962", "\'COP\',3869.551111", "\'CRC\',639.933977", "\'CUC\',1", "\'CUP\',25.75", "\'CVE\',95.5", "\'CZK\',21.804599", "\'DJF\',177.898512", "\'DKK\',6.420751", "\'DOP\',56.348527", "\'DZD\',137.815679", "\'EGP\',15.7288", "\'ERN\',15.00062", "\'ETB\',47.495015", "\'EUR\',0.863166", "\'FJD\',2.0773", "\'FKP\',0.737604", "\'GBP\',0.737604", "\'GEL\',3.16", "\'GGP\',0.737604", "\'GHS\',6.110516", "\'GIP\',0.737604", "\'GMD\',52.1", "\'GNF\',9593.430258", "\'GTQ\',7.735791", "\'GYD\',209.27096", "\'HKD\',7.78995", "\'HNL\',24.096197", "\'HRK\',6.4898", "\'HTG\',98.825583", "\'HUF\',311.851667", "\'IDR\',14239.771556", "\'ILS\',3.11317", "\'IMP\',0.737604", "\'INR\',73.952509", "\'IQD\',1457.929697", "\'IRR\',42250", "\'ISK\',129.8", "\'JEP\',0.737604", "\'JMD\',155.232661", "\'JOD\',0.709", "\'JPY\',112.84514286", "\'KES\',111.67", "\'KGS\',84.798899", "\'KHR\',4067.540057", "\'KMF\',424.875023", "\'KPW\',900", "\'KRW\',1178.928188", "\'KWD\',0.301745", "\'KYD\',0.832714", "\'KZT\',429.13762", "\'LAK\',10422.685015", "\'LBP\',1510.858325", "\'LKR\',201.352094", "\'LRD\',147.224974", "\'LSL\',15.006476", "\'LYD\',4.558384", "\'MAD\',9.062365", "\'MDL\',17.525604", "\'MGA\',3969.061694", "\'MKD\',53.220154", "\'MMK\',1813.66746", "\'MNT\',2851.992224", "\'MOP\',8.016442", "\'MRO\',356.999828", "\'MRU\',36.153797", "\'MUR\',43.300003", "\'MVR\',15.45", "\'MWK\',815.340157", "\'MXN\',20.3352", "\'MYR\',4.152", "\'MZN\',63.842", "\'NAD\',14.96", "\'NGN\',413.446941", "\'NIO\',35.204054", "\'NOK\',8.507975", "\'NPR\',118.411928", "\'NZD\',1.398605", "\'OMR\',0.38499", "\'PAB\',1", "\'PEN\',4.013315", "\'PGK\',3.508662", "\'PHP\',50.015503", "\'PKR\',170.299913", "\'PLN\',3.963967", "\'PYG\',6885.599404", "\'QAR\',3.638366", "\'RON\',4.2707", "\'RSD\',101.51561", "\'RUB\',71.204", "\'RWF\',1018.453639", "\'SAR\',3.750577", "\'SBD\',8.035419", "\'SCR\',13.302889", "\'SDG\',439.5", "\'SEK\',8.5773", "\'SGD\',1.34704", "\'SHP\',0.737604", "\'SLL\',10863.349903", "\'SOS\',578.060903", "\'SRD\',21.492", "\'SSP\',130.26", "\'STD\',20956.440504", "\'STN\',21.45", "\'SVC\',8.744236", "\'SYP\',1257.254008", "\'SZL\',15.006477", "\'THB\',32.789", "\'TJS\',11.26109", "\'TMT\',3.51", "\'TND\',2.8325", "\'TOP\',2.241504", "\'TRY\',9.6954", "\'TTD\',6.790729", "\'TWD\',27.766499", "\'TZS\',2300", "\'UAH\',26.062555", "\'UGX\',3541.383706", "\'USD\',1", "\'UYU\',43.626331", "\'UZS\',10685.355489", "\'VES\',4.4179", "\'VND\',22632.273463", "\'VUV\',111.224217", "\'WST\',2.568092", "\'XAF\',566.199625", "\'XAG\',0.04101723", "\'XAU\',0.0005485", "\'XCD\',2.70255", "\'XDR\',0.708661", "\'XOF\',566.199625", "\'XPD\',0.00048714", "\'XPF\',103.003074", "\'XPT\',0.00095263", "\'YER\',250.124874", "\'ZAR\',14.939888", "\'ZMW\',17.432169", "\'ZWL\',322" };

            foreach (var curr in currency)
            {
                // Prints all currency properties
                // Console.WriteLine("public double "+curr+" { get; set; }");

                // Prints all .Add lines to add data to the data table
                //Console.WriteLine($"dt.Rows.Add(\"{curr}\", val.rates.{curr});");
            }

            foreach (var val in queryValues)
            {
                // Prints all insert Queries to insert data and values.
                Console.WriteLine($"INSERT INTO Currency_Master(CurrencyName, Amount) VALUES ({val});");
            }
        }
    }
}
