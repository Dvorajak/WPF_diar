using Diar_WPF.Model;
using Diar_WPF.ModelView;
using System;
using System.Collections.Generic;
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

namespace Diar_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Main ModelView for manage diary records
        private DiaryRecordManager recordManager = new DiaryRecordManager();
        AddWindow addWindow;
        DateTime date;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = recordManager;

            //Loads data from Records.xml file
            recordManager.Load();

            date = DateTime.Now;
            RecordCalendar.DisplayDateStart = new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Opens up window for adding record and send instance of RecordManager
        /// </summary>
        /// <param name="event"></param>
        /// <param name="e"></param>
        private void AddButtonWindow_Click(object sender, RoutedEventArgs e)
        {
            addWindow = new AddWindow(recordManager);
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Updates ObservableCollection in DiaryRecerodManager and search for text from searchbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            recordManager.SearchRecords(SearchBox.Text);
        }

        /// <summary>
        /// Deletes selected item from DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItem != null)
                recordManager.Remove((MyDiary)DataGridRecord.SelectedItem);
        }

        /// <summary>
        /// Open AddWindow with data form selected item - record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButonWinow_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItem != null)
            {
                addWindow = new AddWindow(recordManager);
                addWindow.EditRecord((MyDiary)DataGridRecord.SelectedItem);
                addWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vyberte prosím událost");
            }

        }

        private void RefreshButtonClick(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            recordManager.SearchRecords(SearchBox.Text);
        }
    }
}
