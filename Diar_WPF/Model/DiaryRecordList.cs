using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diar_WPF.Model
{
    /// <summary>
    /// Model class for main manipulating records
    /// </summary>
    public class DiaryRecordList
    {
        private List<MyDiary> Records;

        public DiaryRecordList()
        {
            Records = new List<MyDiary>();
        }

        public void AddRecord(MyDiary record)
        {
            Records.Add(record);
        }
        public void RemoveRecord(MyDiary record)
        {
            Records.Remove(record);
        }
        public void EditRecord(int indexOFrecord, MyDiary editedRecord)
        {
            Records[indexOFrecord] = editedRecord;
        }
        public void SaveList(List<MyDiary> list)
        {
            Records = list;
        }
        public List<MyDiary> GetAllRecords()
        {
            return Records;
        }

    }
}
