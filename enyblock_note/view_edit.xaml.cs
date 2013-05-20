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
    public partial class view_edit : PhoneApplicationPage
    {

        string file_name = "";
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public view_edit()
        {
            InitializeComponent();
        }

        private void navigate_back()
        {
            //NavigationService.Navigate(new Uri("/enyblock_note;component/Mainpage.xaml", UriKind.Relative));
            NavigationService.GoBack();

            settings["state"] = "";
            settings["value"] = "";
            settings["file_name"] = "";

        }

        private void app_bar_back_click(object sender, EventArgs e)
        {
            navigate_back();      
        }

        private void app_bar_edit_click(object sender, EventArgs e)
        {
            if (display_note_textblock.Visibility == System.Windows.Visibility.Visible)
            {
                bind_edit(display_note_textblock.Text);
            }
        }

        private void app_bar_save_click(object sender, EventArgs e)
        {


            if (edit_note_textbox.Visibility == System.Windows.Visibility.Visible)
            {
                //save the date to isolated storage


                var app_storage = IsolatedStorageFile.GetUserStoreForApplication();

                using (var stream_writer = new StreamWriter(app_storage.OpenFile(file_name, FileMode.Create)))
                {
                    stream_writer.Write(edit_note_textbox.Text);

                }



                display_note_textblock.Text = edit_note_textbox.Text;

                display_note_textblock.Visibility = System.Windows.Visibility.Visible;
                edit_note_textbox.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void app_bar_delete_click(object sender, EventArgs e)
        {
            if (confirm_dialog_canvas.Visibility == System.Windows.Visibility.Collapsed)
            {
                confirm_dialog_canvas.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string temp_state = "";

            if (settings.Contains("state"))
            {
                if (settings.TryGetValue<string>("state", out temp_state))
                {
                    if (temp_state == "edit")
                    {
                        string temp_value = "";

                        if (settings.TryGetValue<string>("file_name", out temp_value))
                        {
                            file_name = temp_value;
                        }

                        
                        if (settings.TryGetValue<string>("value", out temp_value))
                        {
                            bind_edit(temp_value);
                        }


                    }
                    else
                    {
                        bind_view();
                    }
                }

            }
            else
            {
                bind_view();
            }


            settings["file_name"] = file_name;


        }

        private void bind_edit(string content)
        {
            edit_note_textbox.Text = content;

            edit_note_textbox.Visibility = System.Windows.Visibility.Visible;
            display_note_textblock.Visibility = System.Windows.Visibility.Collapsed;

            edit_note_textbox.Focus();
            edit_note_textbox.SelectionStart = edit_note_textbox.Text.Length;

            settings["state"] = "edit";
            settings["value"] = content;
            settings["file_name"] = file_name;
        }

        private void bind_view()
        {
            file_name = NavigationContext.QueryString["id"];

            var app_storage = IsolatedStorageFile.GetUserStoreForApplication();

            using (var stream_reader = new StreamReader(app_storage.OpenFile(file_name, FileMode.Open, FileAccess.Read)))
            {
                display_note_textblock.Text = stream_reader.ReadToEnd();

            }
        }

        private void cancle_delte_click(object sender, RoutedEventArgs e)
        {
            confirm_dialog_canvas.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ok_delete_click(object sender, RoutedEventArgs e)
        {
            var app_storage = IsolatedStorageFile.GetUserStoreForApplication();

            app_storage.DeleteFile(file_name);

            navigate_back();
        }

        private void edit_note_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings["value"] = edit_note_textbox.Text;
        }
    }
}