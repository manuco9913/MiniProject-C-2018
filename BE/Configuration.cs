using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        //, , גיל נבחן  מינימלי, טווח זמן בין מבחן למבחן
        public static int MIN_LESSONS = 20; //ספר השיעורים המינימלי
        public static int MAX_TESTER_AGE = 40;
        public static int MIN_TRAINEE_AGE = 18;
        public static int MIN_GAP_TEST = 7;
        public static int TIME_BETWEEN_TESTS = 1;
    } 
}
