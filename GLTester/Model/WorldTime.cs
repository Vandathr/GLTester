using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class WorldTime
    {
        private int secondz;
        private int minutez;
        private int hourz;
        private int dayz;
        private int weekDay;
        private int weekz;
        private int monthDay;
        private int monthz;
        private int yearDay;
        private int yearz;

        private int increment = 1;

        public void Increment()
        {
            secondz += increment;

            if(secondz > 59)
            {
                minutez++;

                if(minutez > 59)
                {
                    hourz++;

                    if (hourz > 23)
                    {
                        dayz++;
                        weekDay++;
                        monthDay++;
                        yearDay++;

                        if(weekDay > 6)
                        {
                            weekz++;

                            weekDay = 0;
                        }

                        if (monthDay > 29)
                        {
                            monthz++;

                            monthDay = 0;
                        }

                        if(yearDay > 364)
                        {
                            yearz++;

                            weekz = 0;
                            monthz = 0;
                            
                            yearDay = 0;
                            monthDay = 0;

                        }
                        hourz = 0;
                    }

                    minutez = 0;
                }

                secondz = 0;
            }

        }





        public int GetSecondz() => secondz;
        public int GetMinutez() => minutez;
        public int GetHourz() => hourz;
        public int GetDayz() => dayz;
        public int GetWeekz() => weekz;
        public int GetMonthz() => monthz;
        public int GetYearz() => yearz;

        public int GetWeekDay() => weekDay;
        public int GetMonthDay() => monthDay;
        public int GetYearDay() => yearDay;


        public void SetSecondz(int value)
        {
            if (value > 0 && value < 60) secondz = value;
        }

        public void SetMinutez(int value)
        {
            if (value > 0 && value < 60) minutez = value;
        }

        public void SetHourz(int value)
        {
            if (value > 0 && value < 24) hourz = value;
        }

        public void SetWeekDay(int value)
        {
            if (value > 0 && value < 7) weekDay = value;
        }

        public void SetYearDay(int value)
        {
            if (value > 0 && value < 365) yearDay = value;
        }







        public WorldTime(int secondz, int minutez, int hourz)
        {
            SetSecondz(secondz);
            SetMinutez(minutez);
            SetHourz(hourz);
        }

        public WorldTime()
        {

        }


    }
}
