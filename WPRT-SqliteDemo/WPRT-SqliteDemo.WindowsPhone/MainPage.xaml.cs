using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WPRT_SqliteDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SqlHelper _helper = new SqlHelper();
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
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
    }
}
