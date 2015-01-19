using CheckMapp.Model;
using CheckMapp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckMapp.Model.Tables;

namespace CheckMapp.KeyGroup
{
    public class TimeKeyGroup<T> : List<T>
    {
        private IGrouping<string, Note> photosByMonth;

        /// <summary>
        /// The Key of this group.
        /// </summary>
        public string Key { get; private set; }

        public TimeKeyGroup(string key)
        {
            Key = key;
        }

        public static List<TimeKeyGroup<Note>> CreateGroups(IEnumerable<Note> Notes)
        {
            // Create List to hold each item
            List<TimeKeyGroup<Note>> groupedNote = new List<TimeKeyGroup<Note>>();

            // Create a TimeKeyGroup for each group I want
            TimeKeyGroup<Note> LastSeven = new TimeKeyGroup<Note>(AppResources.LastSevenDays);
            TimeKeyGroup<Note> LastTwoWeeks = new TimeKeyGroup<Note>(AppResources.LastTwoWeeks);
            TimeKeyGroup<Note> LastMonth = new TimeKeyGroup<Note>(AppResources.LastMonth);
            TimeKeyGroup<Note> LastSixMonths = new TimeKeyGroup<Note>(AppResources.LastSixMonths);
            TimeKeyGroup<Note> LastYear = new TimeKeyGroup<Note>(AppResources.LastYear);
            TimeKeyGroup<Note> AllTime = new TimeKeyGroup<Note>(AppResources.AllTime);

            // Fill each list with the appropriate Notes
            foreach (Note w in Notes)
            {
                if (w.Date > DateTime.Now.AddDays(-7))
                {
                    LastSeven.Add(w);
                    continue;
                }
                else if (w.Date > DateTime.Now.AddDays(-14))
                {
                    LastTwoWeeks.Add(w);
                    continue;
                }
                else if (w.Date > DateTime.Now.AddMonths(-1))
                {
                    LastMonth.Add(w);
                    continue;
                }
                else if (w.Date > DateTime.Now.AddMonths(-6))
                {
                    LastSixMonths.Add(w);
                    continue;
                }
                else if (w.Date > DateTime.Now.AddMonths(-12))
                {
                    LastYear.Add(w);
                    continue;
                }
                else
                {
                    AllTime.Add(w);
                }
            }

            // Add each TimeKeyGroup to the overall list
            groupedNote.Add(LastSeven);
            groupedNote.Add(LastTwoWeeks);
            groupedNote.Add(LastMonth);
            groupedNote.Add(LastSixMonths);
            groupedNote.Add(LastYear);
            groupedNote.Add(AllTime);

            return groupedNote;
        }

    }
}
