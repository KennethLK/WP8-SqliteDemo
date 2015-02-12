using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8_SqliteDemo.Resources;

namespace WP8_SqliteDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        SqlHelper _helper = new SqlHelper();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            string dbPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\album.db";

            long tick1 = DateTime.Now.Ticks;
            bool result = _helper.OpenDb(dbPath);
            long tick2 = DateTime.Now.Ticks;
            if (result)
            {
                txt_create.Text = "Table Created; used time: " + (tick2 - tick1) / 10000 + "ms";
                //txt_create.Text = "Database Created";
                btn_createTable.IsEnabled = true;
            }
            else
            {
                txt_create.Text = "Database Failed";
            }
        }

        private void btn_createTable_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            bool result = _helper.CreateTable();
            long tick2 = DateTime.Now.Ticks;
            if (result)
            {
                txt_createTable.Text = "Table Created; used time: " + (tick2 - tick1) / 10000 + "ms";
                btn_insertRecord.IsEnabled = true;
                btn_deleteRecord.IsEnabled = true;
                btn_getCount.IsEnabled = true;
            }
            else
            {
                txt_createTable.Text = "Table Failed";
            }
        }

        private void btn_insertRecord_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            _helper.InsertOne();
            long tick2 = DateTime.Now.Ticks;
            txt_insertRecord.Text = "used time: " + (tick2 - tick1) / 10000 + "ms";
        }

        private void btn_getCount_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            int count = _helper.GetCount();
            long tick2 = DateTime.Now.Ticks;
            txt_getCount.Text = count + "条 used time: " + (tick2 - tick1) / 10000 + "ms";
        }

        private void btn_deleteRecord_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            bool result = _helper.DeleteOne();
            long tick2 = DateTime.Now.Ticks;
            if (result)
            {
                txt_deleteRecord.Text = "已删除 used time: " + (tick2 - tick1) / 10000 + "ms";
            }
            else
            {
                txt_deleteRecord.Text = "删除失败";
            }
        }

        private void btn_deleteRecordBat_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = _helper.GetCount();
            int failedCount = 0;
            long tick1 = DateTime.Now.Ticks;
            for (int i = 0; i < rowCount; i++)
            {
                bool result = _helper.DeleteOne();
                if (!result) failedCount++;
            }
            long tick2 = DateTime.Now.Ticks;
            txt_deleteRecordBat.Text = "共有记录" + rowCount + "条， 删除失败：" + failedCount + "条 used time: " + (tick2 - tick1) / 10000 + "ms";
        }

        private void btn_insertRecordBat_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = 1000;
            int failedCount = 0;
            long tick1 = DateTime.Now.Ticks;
            for (int i = 0; i < rowCount; i++)
            {
                bool result = _helper.InsertOne();
                if (!result) failedCount++;
            }
            long tick2 = DateTime.Now.Ticks;
            txt_insertRecordBat.Text = "计划插入" + rowCount + "条， 插入失败：" + failedCount + " used time: " + (tick2 - tick1) / 10000 + "ms";
        }

        private void btn_insertRecord2_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            _helper.InsertOne2();
            long tick2 = DateTime.Now.Ticks;
            txt_insertRecord2.Text = "used time: " + (tick2 - tick1) / 10000 + "ms";
        }

        private void btn_deleteRecord2_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            bool result = _helper.DeleteOne2();
            long tick2 = DateTime.Now.Ticks;
            if (result)
            {
                txt_deleteRecord2.Text = "已删除 used time: " + (tick2 - tick1) / 10000 + "ms";
            }
            else
            {
                txt_deleteRecord2.Text = "删除失败";
            }
        }

        private void btn_upgradeStructure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_createTable2_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            bool result = _helper.CreateTable2();
            long tick2 = DateTime.Now.Ticks;
            if (result)
            {
                txt_createTable2.Text = "Table Created; used time: " + (tick2 - tick1) / 10000 + "ms";
            }
            else
            {
                txt_createTable2.Text = "Table Failed";
            }
        }

        private void btn_joinQuery2_Click(object sender, RoutedEventArgs e)
        {
            long tick1 = DateTime.Now.Ticks;
            int result = _helper.GetCrossTable();
            long tick2 = DateTime.Now.Ticks;
            txt_joinQuery2.Text = result + "条; used time: " + (tick2 - tick1) / 10000 + "ms";
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}