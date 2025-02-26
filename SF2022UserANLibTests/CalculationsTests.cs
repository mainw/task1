using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022UserANLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022UserANLib.Tests
{
    [TestClass()]
    public class CalculationsTests
    {
        private bool AreEquals(string[] a, string[] b)
        {
            if(a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    Console.WriteLine(a[i] + " - " + b[i]);
                    if(b[i] != a[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        [TestMethod()]
        public void AvailablePeriods_initialTest()
        {
            string[] correctResult = new string[]
            {
                "08:00 - 08:30",
                "08:30 - 09:00",
                "09:00 - 09:30",
                "09:30 - 10:00",
                "11:30 - 12:00",
                "12:00 - 12:30",
                "12:30 - 13:00",
                "13:00 - 13:30",
                "13:30 - 14:00",
                "14:00 - 14:30",
                "14:30 - 15:00",
                "15:40 - 16:10",
                "16:10 - 16:40",
                "17:30 - 18:00"
            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };
            int[] w = new int[]
            {
                60,
                30,
                10,
                10,
                40
            };
            TimeSpan e = new TimeSpan(8,0,0);
            TimeSpan r = new TimeSpan(18,0,0);
            int t = 30;
            
            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_sinceBeginOfDay()
        {
            string[] correctResult = new string[]
            {
                "09:00 - 09:30",
                "09:30 - 10:00",
                "10:00 - 10:30",
                "10:30 - 11:00",
                "11:30 - 12:00",
                "12:00 - 12:30",
                "12:30 - 13:00",
                "13:00 - 13:30",
                "13:30 - 14:00",
                "14:00 - 14:30",
                "14:30 - 15:00",
                "15:40 - 16:10",
                "16:10 - 16:40",
                "17:30 - 18:00"
            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(8,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };
            int[] w = new int[]
            {
                60,
                30,
                10,
                10,
                40
            };
            TimeSpan e = new TimeSpan(8, 0, 0);
            TimeSpan r = new TimeSpan(18, 0, 0);
            int t = 30;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_insufficientTimeIntervals()
        {
            string[] correctResult = new string[]
            {

            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(8,0,0),
                new TimeSpan(9,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };
            int[] w = new int[]
            {
                60,
                30,
                180,
                10,
                40
            };
            TimeSpan e = new TimeSpan(8, 0, 0);
            TimeSpan r = new TimeSpan(18, 0, 0);
            int t = 120;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_consultationsAtTheEnd()
        {
            string[] correctResult = new string[]
            {
                "08:00 - 08:30",
                "08:30 - 09:00",
                "09:00 - 09:30",
                "09:30 - 10:00",
                "10:00 - 10:30",
                "10:30 - 11:00"
            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };
            int[] w = new int[]
            {
                240,
                30,
                80,
                70
            };
            TimeSpan e = new TimeSpan(8, 0, 0);
            TimeSpan r = new TimeSpan(18, 0, 0);
            int t = 30;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_consultationsAtTheBegin()
        {
            string[] correctResult = new string[]
            {
                "18:00 - 18:30",
                "18:30 - 19:00",
                "19:00 - 19:30",
                "19:30 - 20:00",
                "20:00 - 20:30",
                "20:30 - 21:00",
                "21:00 - 21:30",
                "21:30 - 22:00"
            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };
            int[] w = new int[]
            {
                240,
                30,
                80,
                70
            };
            TimeSpan e = new TimeSpan(11, 0, 0);
            TimeSpan r = new TimeSpan(22, 0, 0);
            int t = 30;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_intervalMoreThenWorkDay()
        {
            string[] correctResult = new string[]
            {

            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)
            };
            int[] w = new int[]
            {
                240,
                30,
                80,
                70
            };
            TimeSpan e = new TimeSpan(11, 0, 0);
            TimeSpan r = new TimeSpan(22, 0, 0);
            int t = 670;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_intervalEqualWorkDay_withoutConsultations()
        {
            string[] correctResult = new string[]
            {
                "11:00 - 22:00"
            };
            TimeSpan[] q = new TimeSpan[]
            {

            };
            int[] w = new int[]
            {

            };
            TimeSpan e = new TimeSpan(11, 0, 0);
            TimeSpan r = new TimeSpan(22, 0, 0);
            int t = 660;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_withoutConsultations()
        {
            string[] correctResult = new string[]
            {
                "11:00 - 12:00",
                "12:00 - 13:00",
                "13:00 - 14:00",
                "14:00 - 15:00",
                "15:00 - 16:00",
                "16:00 - 17:00",
                "17:00 - 18:00",
                "18:00 - 19:00",
                "19:00 - 20:00",
                "20:00 - 21:00",
                "21:00 - 22:00"
            };
            TimeSpan[] q = new TimeSpan[]
            {

            };
            int[] w = new int[]
            {

            };
            TimeSpan e = new TimeSpan(11, 0, 0);
            TimeSpan r = new TimeSpan(22, 0, 0);
            int t = 60;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_allDayInConsultations()
        {
            string[] correctResult = new string[]
            {

            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0)
            };
            int[] w = new int[]
            {
                240,
                30,
                30
            };
            TimeSpan e = new TimeSpan(11, 0, 0);
            TimeSpan r = new TimeSpan(16, 0, 0);
            int t = 10;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
        [TestMethod()]
        public void AvailablePeriods_consultationsLessThenInterval()
        {
            string[] correctResult = new string[]
            {

            };
            TimeSpan[] q = new TimeSpan[]
            {
                new TimeSpan(11,0,0),
                new TimeSpan(12,20,0),
                new TimeSpan(13,40,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0)
            };
            int[] w = new int[]
            {
                10,
                10,
                10,
                30,
                30
            };
            TimeSpan e = new TimeSpan(10, 30, 0);
            TimeSpan r = new TimeSpan(16, 0, 0);
            int t = 60;

            string[] testResult = Calculations.AvailablePeriods(q, w, e, r, t);
            Assert.IsTrue(AreEquals(correctResult, testResult));
        }
    }
}