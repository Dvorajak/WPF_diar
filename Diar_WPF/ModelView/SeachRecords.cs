using Diar_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diar_WPF.ModelView
{
    //Class for Search records in saved collection
    public class SearchRecords
    {

        private ObservableCollection<MyDiary> ActualDiaryRecords;

        public SearchRecords()
        {
            ActualDiaryRecords = new ObservableCollection<MyDiary>();
        }

        /// <summary>
        /// Search text and record collection and return find record. If searched text is empty, return all saved record collection
        /// </summary>
        /// <param name="searchedText"></param>
        /// <param name="SavedDiaryRecordsCollection"></param>
        /// <returns></returns>
        public ObservableCollection<MyDiary> SearchDiaryRecords(string searchedText, List<MyDiary> SavedDiaryRecordsCollection)
        {

            if (string.IsNullOrEmpty(searchedText))
            {
                return ActualDiaryRecords = new ObservableCollection<MyDiary>(SavedDiaryRecordsCollection);
            }
            else
            {
                ActualDiaryRecords.Clear();
                foreach (MyDiary de in SavedDiaryRecordsCollection)
                {
                    if (de.EventText.ToLower().Contains(searchedText.ToLower()) || de.Name.ToLower().Contains(searchedText.ToLower()))
                    {
                        ActualDiaryRecords.Add(de);
                    }
                }
                return ActualDiaryRecords;
            }

        }
    }
}
