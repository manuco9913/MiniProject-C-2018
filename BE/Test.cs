using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        public string _testId { get; set; } //need to be with 8 digits
        public string _testerId { get; set; }
        public string _traineeId { get; set; }
        public string _date { get; set; } // just date
        public string _dateAndTime { get; set; } // date with hour
        public string _addressToStart { get; set; } // Test start from this address
        public string _gradeDistanceKeepping { get; set; } // לשמור מרחק
        public string _gradeReverseParking { get; set; } // חנייה ברוורס
        public string _gradeRegularParking { get; set; } // חניה רגילה
        public string _gradeMirrorLooking { get; set; } // הסתכלות במראות
        public string _gradeSpeedKeeping { get; set; } // שמירה על מהירות
        public string _gradeObeyedToSigns { get; set; } //Stop completely with a stop sign
        public string _gradeBlinkersUsed { get; set; } // שימוש באיתות
        public string _gradeGivingPriorityToPedestrians { get; set; } // מתן זכות קדימה להולכי רגל
        public string _gradeGearsUsage { get; set; } // שימוש בהילוכים
        public bool _finalGrade { get; set; }// Past or Failed
        public string _testerNotes { get; set; } //Notes by the Tester
        public override string ToString()
        {
            return _testId + " " + _testerId + " " + _traineeId + " " + _dateAndTime;
        }

    }
}
