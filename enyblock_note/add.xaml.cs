using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;

namespace enyblock_note
{
    public partial class add : PhoneApplicationPage
    {
        private string location = "";
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public add()
        {
            InitializeComponent();

            GeoCoordinateWatcher my_watcher = new GeoCoordinateWatcher();

            var my_position = my_watcher.Position;

            // set a default value, if we canot get the current location,
            // the we'll default to Redmond, WA

            double latitude = 47.674;
            double longitude = -122.12;

            if (!my_position.Location.IsUnknown)
            {
                latitude = my_position.Location.Latitude;
                longitude = my_position.Location.Longitude;
            }


            myTerraService.TerraServiceSoapClient client = new myTerraService.TerraServiceSoapClient();
            client.ConvertLonLatPtToNearestPlaceCompleted += new EventHandler<myTerraService.ConvertLonLatPtToNearestPlaceCompletedEventArgs>(client_ConvertLonLatPtToNearestPlaceCompleted);
            client.ConvertLonLatPtToNearestPlaceAsync(new myTerraService.LonLatPt() { Lat = latitude, Lon = longitude });
        
        }

        void client_ConvertLonLatPtToNearestPlaceCompleted(object sender, myTerraService.ConvertLonLatPtToNearestPlaceCompletedEventArgs e)
        {
            location = e.Result;
            //throw new NotImplementedException();
        }





        private void app_bar_cancle_click(object sender, EventArgs e)
        {
            navigate_back();
        }

        private void app_bar_save_click(object sender, EventArgs e)
        {
            // save the note

            if (0 == location.Trim().Length)
            {
                location = "Unknown";
            }

            // create the file name
            string file_name = create_file_name();


            // isolated storage the content 
            var app_strage = IsolatedStorageFile.GetUserStoreForApplication();

            using(var stream_writer = new StreamWriter(app_strage.OpenFile(file_name,FileMode.Create)))
            {
                stream_writer.Write(edit_note_textbox.Text);
            }

            // navigate the main_page.xaml
            navigate_back();
        }

        // create the file name 
        private string create_file_name()
        {
            StringBuilder sb = new StringBuilder();

            // create the date
            sb.Append(DateTime.Now.Year);
            sb.Append("_");
            sb.Append(string.Format("{0:00}",DateTime.Now.Month));
            sb.Append("_");
            sb.Append(string.Format("{0:00}", DateTime.Now.Day));
            sb.Append("_");
            sb.Append(string.Format("{0:00}", DateTime.Now.Hour));
            sb.Append("_");
            sb.Append(string.Format("{0:00}", DateTime.Now.Minute));
            sb.Append("_");
            sb.Append(string.Format("{0:00}", DateTime.Now.Second));
            sb.Append("_");

            // create the location
            location = location.Replace(", ", "_");
            location = location.Replace(" ", "-");


            // create the file suffix
            sb.Append(location);
            sb.Append(".txt");

            return (sb.ToString());
        }

        private void navigate_back()
        {
            //NavigationService.Navigate(new Uri("/enyblock_note;component/Mainpage.xaml", UriKind.Relative));

            NavigationService.GoBack();
            settings["state"] = "";
            settings["value"] = "";
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string temp_state = "";

            if (settings.Contains("state"))
            {
                if (settings.TryGetValue<string>("state", out temp_state))
                {
                    if (temp_state == "add")
                    {
                        string temp_value = "";
                        if (settings.TryGetValue<string>("value", out temp_value))
                        {
                            edit_note_textbox.Text = temp_value;
                        }

                    }
                }
            }

            settings["state"] = "add";
            settings["value"] = edit_note_textbox.Text;

            edit_note_textbox.Focus();
            edit_note_textbox.SelectionStart = edit_note_textbox.Text.Length;
        }

        private void edit_note_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings["value"] = edit_note_textbox.Text;
        }
    }
}