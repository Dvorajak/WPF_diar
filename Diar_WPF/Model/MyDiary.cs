using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diar_WPF.Model
{
    /// <summary>
    /// Model class for Diary records
    /// </summary>
    public class MyDiary
    {
        public DateTime DateOfEvent { get; set; }
        public string Name { get; set; }
        public string EventText { get; set; }

        public TimeSpan TimeLeft
        {
            get
            {
                DateTime dateNow = DateTime.Now;
                return DateOfEvent - dateNow;
            }
        }

        public MyDiary(DateTime dateOfEvent, string name, string eventText)
        {
            DateOfEvent = dateOfEvent;
            Name = name;
            EventText = eventText;
        }

        public MyDiary()
        {

        }

        public override string ToString()
        {
            return Name;
        }

    }
}
