using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;
using System.Linq;
using System.Text;
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
using System.Diagnostics;

namespace CarRepairShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int randomNumber = 1001;
        List<CustomerList> customerLists = new List<CustomerList>();
        CustomerList c1;
        List<VehicleList> vehicleLists = new List<VehicleList>();
        VehicleList v1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            c1 = new CustomerList();
            c1.FirstName = firstName_txt.Text;
            c1.LastName = lastName_txt.Text;
            c1.Age = int.Parse(age_txt.Text);
            c1.City = city_txt.Text;
            c1.AccountNumber = randomNumber;
            customerLists.Add(c1);

            selectAccNumber_txt.Items.Add(c1.AccountNumber);

            custNameListBox.Items.Add(c1.FirstName + " " + c1.LastName);

            // For adding Customer city into cityListBox
            if (!cityListBox.Items.Contains(c1.City))
            {
                    cityListBox.Items.Add(c1.City);
            }
            

            string message = "Your information has been saved and your Unique Account Number is :";
            string title = "Close Window";
            MessageBox.Show(message + randomNumber.ToString(), title);
            randomNumber++;

            firstName_txt.Text = null;
            lastName_txt.Text = null;
            age_txt.Text = null;
            city_txt.Text = null;
            accountNumber_txt.Text = null;


            Console.WriteLine(c1.AccountNumber); //checking the customer list data
            Console.WriteLine(c1.FirstName); //checking the customer list data
            Console.WriteLine(c1.LastName); //checking the customer list data
            Console.WriteLine(c1.Age); //checking the customer list data
            Console.WriteLine(c1.City); //checking the customer list data
           
        }

        private void ButtonClear_Click_1(object sender, RoutedEventArgs e)
        {
            firstName_txt.Text = null;
            lastName_txt.Text = null;
            age_txt.Text = null;
            city_txt.Text = null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
          
            v1 = new VehicleList();
            v1.Make = vehicleMake_txt.Text;
            v1.Model = vehicleModel_txt.Text;
            v1.Color = vehicleColor_txt.Text;
            v1.NumberOfDoors = int.Parse(vehicleDoors_txt.Text);
            v1.SelectedAccNumber = int.Parse(selectAccNumber_txt.Text);
            
            vehicleLists.Add(v1);

            // For adding Vehicle Make into makeListBox
            
                if (!makeListBox.Items.Contains(v1.Make))
                {
                    makeListBox.Items.Add(v1.Make);
                }

            // For adding Vehicle Model into modelsListBox
            
            if (!modelsListBox.Items.Contains(v1.Model))
                {
                    modelsListBox.Items.Add(v1.Model);
                }


            // For adding Vehicle color into colorsListBox

            if (!colorsListBox.Items.Contains(v1.Color))
            {
                colorsListBox.Items.Add(v1.Color);
            }

            string message = "Your vehicle information has been saved :";
            string title = "Close Window";
            MessageBox.Show(message, title);

            vehicleMake_txt.Text = null;
            vehicleModel_txt.Text = null;
            vehicleColor_txt.Text = null;
            vehicleDoors_txt.Text = null;
            selectAccNumber_txt.Text = null;

            Console.WriteLine(v1.SelectedAccNumber); //checking the customer list data
            Console.WriteLine(v1.Make); //checking the customer list data
            Console.WriteLine(v1.Model); //checking the customer list data
            Console.WriteLine(v1.Color); //checking the customer list data
            Console.WriteLine(v1.NumberOfDoors); //checking the customer list data
            Console.WriteLine(v1.SelectedAccNumber); //checking the selected account number of customer
        }

        public void WriteToXml_Standard(object sender, RoutedEventArgs e)
        {

            XmlWriterSettings xmlSetting = new XmlWriterSettings();
            xmlSetting.Indent = true;
            xmlSetting.IndentChars = "\t";

            XmlWriter writer = XmlWriter.Create("CustomerList.xml", xmlSetting);

            writer.WriteStartDocument();
            writer.WriteStartElement("Customers");

            for (var i = 0; i < customerLists.Count; i++)
            {
                c1 = customerLists[i];
                writer.WriteStartElement("Customer");
                writer.WriteElementString("FirstName", c1.FirstName);
                writer.WriteElementString("LastName", c1.LastName);
                writer.WriteElementString("Age", c1.Age.ToString());
                writer.WriteElementString("City", c1.City);
                writer.WriteElementString("AccountNumber", c1.AccountNumber.ToString());
               
                for (var j = 0; j < vehicleLists.Count; j++)
                {
                    v1 = vehicleLists[j];
                    if(c1.AccountNumber == v1.SelectedAccNumber)
                    {
                        writer.WriteStartElement("Vehicles");
                        writer.WriteElementString("Make", v1.Make);
                        writer.WriteElementString("Model", v1.Model);
                        writer.WriteElementString("Color", v1.Color);
                        writer.WriteElementString("NumberOfDoors", v1.NumberOfDoors.ToString());
                        writer.WriteElementString("SelectedAccNumber", v1.SelectedAccNumber.ToString());
                        writer.WriteEndElement();
                    }
                } writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Close();
        }

        public void ReadFromXML_Standard(object sender, RoutedEventArgs e)
        {
            custNameListBox.Items.Clear();
            makeListBox.Items.Clear();
            modelsListBox.Items.Clear();
            cityListBox.Items.Clear();
            colorsListBox.Items.Clear();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XmlReader reader = XmlReader.Create("CustomerList.xml", settings);

            if (reader.ReadToDescendant("Customer"))
            {
                do
                {
                    reader.ReadStartElement("Customer");
                    c1 = new CustomerList();
                    c1.FirstName = reader.ReadElementContentAsString();
                    c1.LastName = reader.ReadElementContentAsString();
                    c1.Age = reader.ReadElementContentAsInt();
                    c1.City = reader.ReadElementContentAsString();
                    c1.AccountNumber = reader.ReadElementContentAsInt();
                    customerLists.Add(c1);

                    custNameListBox.Items.Add(c1.FirstName + " " + c1.LastName);

                    if (!cityListBox.Items.Contains(c1.City))
                    {
                        cityListBox.Items.Add(c1.City);
                    }

                do
                {
                    reader.ReadStartElement("Vehicles");
                    v1 = new VehicleList();
                    v1.Make = reader.ReadElementContentAsString();
                    v1.Model = reader.ReadElementContentAsString();
                    v1.Color = reader.ReadElementContentAsString();
                    v1.NumberOfDoors = reader.ReadElementContentAsInt();
                    v1.SelectedAccNumber = reader.ReadElementContentAsInt();
                    vehicleLists.Add(v1);

                    // For adding Vehicle Make into makeListBox

                    if (!makeListBox.Items.Contains(v1.Make))
                    {
                        makeListBox.Items.Add(v1.Make);
                    }

                    // For adding Vehicle Model into modelsListBox

                    if (!modelsListBox.Items.Contains(v1.Model))
                    {
                        modelsListBox.Items.Add(v1.Model);
                    }

                    // For adding Vehicle color into colorsListBox

                    if (!colorsListBox.Items.Contains(v1.Color))
                    {
                        colorsListBox.Items.Add(v1.Color);
                    }

                    } while (reader.ReadToNextSibling("Vehicles"));
                } while (reader.ReadToNextSibling("Customer"));
            }
            reader.Close();
            Console.WriteLine("Done Reading");
        }       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            custNameListBox.Items.Clear();
            cityListBox.Items.Clear();
            makeListBox.Items.Clear();
            modelsListBox.Items.Clear();
            colorsListBox.Items.Clear();
        }

        private void CustNameListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (custNameListBox.SelectedIndex != -1)
            {
                cityListBox.SelectedItems.Clear();
                makeListBox.SelectedItems.Clear();
                modelsListBox.SelectedItems.Clear();
                colorsListBox.SelectedItems.Clear();

                c1 = customerLists[custNameListBox.SelectedIndex];
                cityListBox.SelectedItem = c1.City;

                for (int i = 0; i < vehicleLists.Count; i++)
                {
                    v1 = vehicleLists[i];
                    if (c1.AccountNumber == v1.SelectedAccNumber)
                    {
                        makeListBox.SelectedItems.Add(v1.Make);
                        modelsListBox.SelectedItems.Add(v1.Model);
                        colorsListBox.SelectedItems.Add(v1.Color);
                    }
                }
            }
        }
        private void CityListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cityListBox.SelectedIndex != -1)
            {
                custNameListBox.SelectedItems.Clear();
                makeListBox.SelectedItems.Clear();
                modelsListBox.SelectedItems.Clear();
                colorsListBox.SelectedItems.Clear();

                for (int i = 0; i < customerLists.Count; i++)
                {
                    c1 = customerLists[cityListBox.SelectedIndex];
                    c1 = customerLists[i];
                    
                    for (int j = 0; j < customerLists.Count; j++ )
                    {
                        string fullName = c1.FirstName + " " + c1.LastName;
                        if(cityListBox.SelectedItem.ToString() == customerLists[i].City)
                        {
                            Debug.Write(fullName);
                            custNameListBox.SelectedItems.Add(fullName);
                        }
                    }
                }
            } 
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            vehicleMake_txt.Text = null;
            vehicleModel_txt.Text = null;
            vehicleColor_txt.Text = null;
            vehicleDoors_txt.Text = null;

        }
    }
}

