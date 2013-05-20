using System;
using System.Collections.Generic;
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
using System.IO.IsolatedStorage;
using System.IO;

namespace enyblock_note
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            //settings["state"] = "";
            //settings["value"] = "";

            string temp_state = "";

            if (settings.Contains("state"))
            {
                if (settings.TryGetValue<string>("state", out temp_state))
                {
                    if (temp_state == "add")
                    {
                        NavigationService.Navigate(new Uri("/enyblock_note;component/add.xaml", UriKind.Relative));
                    }
                    else if (temp_state == "edit")
                    {
                        NavigationService.Navigate(new Uri("/enyblock_note;component/view_edit.xaml", UriKind.Relative));
                    }
                    else
                    {

                    }
                }
            }

            bind_list();
        }

        #region app_bar Icon click events

        private void app_bar_add_click(object sender, EventArgs e)
        {
            
            /*create a file to testing */
            //string file_name = "2012_04_18_09_52_20_Redmond_Washington-USA.txt";
            //string file_content = "this is just a test to see if I can get this to work!";

            //var app_storage = IsolatedStorageFile.GetUserStoreForApplication();

            //if (!app_storage.FileExists(file_name))
            //{
            //    using (var stream_writer = new StreamWriter(app_storage.CreateFile(file_name)))
            //    {
            //        stream_writer.WriteLine(file_content);
            //    }
            //}

            //bind_list();

            NavigationService.Navigate(new Uri("/enyblock_note;component/add.xaml", UriKind.Relative));
        }

        private void app_bar_help_click(object sender, EventArgs e)
        {
            if (help_canvas.Visibility == System.Windows.Visibility.Collapsed)
            {
                help_canvas.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion

        

        // bing list function 
        private void bind_list()
        {
            var app_storage = IsolatedStorageFile.GetUserStoreForApplication();

            string[] file_name_list = app_storage.GetFileNames();

            List<Note> notes = new List<Note>();

            foreach (string temp_file_name in file_name_list)
            {
                if ("__ApplicationSettings" != temp_file_name)
                {
                    // retrieve the file 
                    string file_name = temp_file_name;

                    // pluck out the date parts
                    string year = temp_file_name.Substring(0, 4);
                    string month = temp_file_name.Substring(5, 2);
                    string day = temp_file_name.Substring(8, 2);
                    string hour = temp_file_name.Substring(11, 2);
                    string minute = temp_file_name.Substring(14, 2);
                    string second = temp_file_name.Substring(17, 2);

                    // create a new DateTime object
                    DateTime note_date_created = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second));

                    // parse out the location
                    string location = temp_file_name.Substring(20);
                    location = location.Replace("_", ", ");
                    location = location.Replace("-", " ");
                    location = location.Substring(0, location.Length - 4);

                    notes.Add(new Note() { m_note_location = location, m_note_date_created = note_date_created.ToLongDateString(), m_file_name = file_name });
                }
            }

            notes_listbox.ItemsSource = notes;
        }

        private void view_note_hylinkbutton(object sender, RoutedEventArgs e)
        {

            HyperlinkButton click_hylinkbutton = (HyperlinkButton)sender;

            string uri = string.Format("/enyblock_note;component/view_edit.xaml?id={0}", click_hylinkbutton.Tag);

            NavigationService.Navigate(new Uri(uri, UriKind.Relative));

        }

        private void help_close_button(object sender, RoutedEventArgs e)
        {
            if (help_canvas.Visibility == System.Windows.Visibility.Visible)
            {
                help_canvas.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


    }
}