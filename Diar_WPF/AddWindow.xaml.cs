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
using System.Windows.Shapes;

namespace Diar_WPF
{
    /// <summary>
    /// Interakční logika pro AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private DiaryRecordManager eventManager;
        private MyDiary diar = null;
        public AddWindow(DiaryRecordManager eventManager)
        {
            InitializeComponent();
            this.eventManager = eventManager;
        }

        /// <summary>
        /// Adds or Edits Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButtonRecord_Click(object sender, RoutedEventArgs e)
        {

            //Call method for editing - if no diar selected for editing it will add record
            eventManager.EditRecord(diar);
            try
            {
                eventManager.AddEvent(NameOfEvent.Text, EventDatePick.SelectedDate, TextOfEvent.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        /// <summary>
        /// Closes the Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Gets selected record (as type MyDiary) from MainWindow and loads record data
        /// </summary>
        /// <param name="diary"></param>
        public void EditRecord(MyDiary diary)
        {
            NameOfEvent.Text = diary.Name;
            EventDatePick.SelectedDate = diary.DateOfEvent;
            TextOfEvent.Text = diary.EventText;

            this.diar = diary;

        }
    }
}
