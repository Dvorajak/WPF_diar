using Diar_WPF.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using System.IO;
using System.Xml.Serialization;

namespace Diar_WPF.ModelView
{
    // Main logic of managing records
    public class DiaryRecordManager : ViewModelEvent
    {
        private MyDiary diaryRecord;
        private DiaryRecordList diaryRecordList;
        public ObservableCollection<MyDiary> ActualDiaryEvents { get; set; }
        public DateTime TestDate
        {
            get => DateTime.Now;
        }
        public string ActualDate
        {
            get
            {
                return DateTime.Now.ToString("dd.MM.yyyy - dddd");
            }
        }

        public string DateWeek
        {
            get
            {
                var date = DateTime.Now;
                return (ISOWeek.GetWeekOfYear(date)).ToString();
            }
        }

        private SearchRecords SeachEvents;

        public MyDiary NearestRecord { get; set; }
        private int RecordEditIndex;
        private bool IsEditedRecord = false;

        string path = "Records.xml";

        public DiaryRecordManager()
        {
            SeachEvents = new SearchRecords();
            diaryRecordList = new DiaryRecordList();
            ActualDiaryEvents = new ObservableCollection<MyDiary>(diaryRecordList.GetAllRecords());
        }

        /// <summary>
        /// Gets data form AddWindow and create instance of MyDiary if data is valid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddEvent(string name, DateTime? date, string text)
        {

            if (date == null)
            {
                throw new ArgumentException("Zadejte prosím správné datum");
            }
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                throw new ArgumentException("Prosím zadejte název delší než 3 znaky");
            }

            if (date.Value.Date < DateTime.Now)
            {
                throw new ArgumentException("Nelze zadávat události do minulosti");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Zadejte prosím alespoň nějaký popis události");
            }

            // create instance
            diaryRecord = new MyDiary(date.Value.Date, name, text);

            // if is no record for editing
            if (!IsEditedRecord)
                // adding new record
                diaryRecordList.AddRecord(diaryRecord);
            else
                // edits existing record
                diaryRecordList.EditRecord(RecordEditIndex, diaryRecord);

            // Load new data to ObservableCollection, updates nearest record and saves file
            LoadActual();
            FindNearestRecord();
            Save();
        }

        /// <summary>
        /// Method for remove specific record
        /// </summary>
        /// <param name="diary"></param>
        public void Remove(MyDiary diary)
        {
            diaryRecordList.RemoveRecord(diary);
            LoadActual();
            FindNearestRecord();
            Save();
        }

        /// <summary>
        /// Gets all record and find the nearest by time left
        /// </summary>
        public void FindNearestRecord()
        {
            List<MyDiary> nearest = new List<MyDiary>();
            var records = diaryRecordList.GetAllRecords();

            if (records.Count != 0)
            {
                nearest = (records.OrderBy(r => r.TimeLeft)).ToList<MyDiary>();
            }

            if (nearest.Count() == 0 || nearest == null)
                NearestRecord = null;
            else
                NearestRecord = nearest.First();

            // Calls INotifyPropertyChanged from parent class to update closest record
            OnPropertyChanged(nameof(NearestRecord));

        }

        /// <summary>
        /// Clears ObservableCollection and loads all saved records
        /// </summary>
        public void LoadActual()
        {
            ActualDiaryEvents.Clear();
            foreach (MyDiary record in diaryRecordList.GetAllRecords())
            {
                ActualDiaryEvents.Add(record);
            }
        }

        /// <summary>
        /// Search text from the textbox. If texbox is empty, returns all saved records
        /// </summary>
        /// <param name="searchText"></param>
        public void SearchRecords(string searchText)
        {
            ActualDiaryEvents.Clear();
            var SeachRecord = SeachEvents.SearchDiaryRecords(searchText, diaryRecordList.GetAllRecords());

            foreach (MyDiary record in SeachRecord)
            {
                ActualDiaryEvents.Add(record);
            }
        }

        /// <summary>
        /// Gets record, if its null sets record as new. Else get index from saved records and sets it for edit
        /// </summary>
        /// <param name="editRecord"></param>
        public void EditRecord(MyDiary editRecord)
        {
            var records = diaryRecordList.GetAllRecords();

            if (editRecord == null)
                this.IsEditedRecord = false;
            else
            {
                for (int x = 0; x < records.Count; x++)
                {
                    if (records[x] == editRecord)
                    {
                        this.RecordEditIndex = x;
                        this.IsEditedRecord = true;
                    }
                }

            }

        }

        /// <summary>
        /// Saves all records to Records.xml file
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(diaryRecordList.GetAllRecords().GetType());
            using (StreamWriter sw = new StreamWriter(path))
            {
                serializer.Serialize(sw, diaryRecordList.GetAllRecords());
            }
        }

        /// <summary>
        /// Loads records from file
        /// </summary>
        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(diaryRecordList.GetAllRecords().GetType());
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    diaryRecordList.SaveList((List<MyDiary>)serializer.Deserialize(sr));
                    LoadActual();
                }
            }
            else
                diaryRecordList = new DiaryRecordList();
            LoadActual();
            FindNearestRecord();
        }


    }
}
