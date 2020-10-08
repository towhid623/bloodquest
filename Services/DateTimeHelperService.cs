using Service.Helper;
using System;

namespace Service.Helper
{
    public class DateTimeHelperService
    {
        public static string ConvertBDDateStringToDateTimeObject(string dateStringBDFormat)
        {
            string convertedDate = string.Empty;
            int month = 0;
            int day = 0;
            int year = 0;
            var currentTime = BdDateTime.Now();
            convertedDate = new DateTime(Convert.ToInt32(currentTime.Year), Convert.ToInt32(currentTime.Month), Convert.ToInt32(currentTime.Day)).ToString("dd/MM/yyyy");
            if (dateStringBDFormat != null && dateStringBDFormat != "")
            {
                string[] splitData = dateStringBDFormat.Split('/', '-');
                if (splitData != null && splitData.Length > 1)
                {
                    day = int.Parse(splitData[0]);
                    month = int.Parse(splitData[1]);
                    if (splitData[2].Contains(" "))
                    {
                        string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
                        if (splitYearTime != null && splitYearTime.Length > 1)
                            year = int.Parse(splitYearTime[0]);
                    }
                    else
                    {
                        year = int.Parse(splitData[2]);
                    }

                }
                else
                {
                    day = int.Parse(dateStringBDFormat.Split('-')[0]);
                    month = int.Parse(dateStringBDFormat.Split('-')[1]);
                    if (splitData[2].Contains(" "))
                    {
                        string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
                        if (splitYearTime != null && splitYearTime.Length > 1)
                            year = int.Parse(splitYearTime[0]);
                    }
                    else
                    {
                        year = int.Parse(splitData[2]);
                    }
                }
                DateTime newDate = new DateTime(year, month, day);
                convertedDate = newDate.ToString();
            }
            return convertedDate;
        }
        //public static string ConvertBDDateStringToDateTimeObject(string dateStringBDFormat)
        //{
        //    string convertedDate = string.Empty;
        //    int month = 0;
        //    int day = 0;
        //    int year = 0;
        //    var currentTime = BdDateTime.Now();
        //    convertedDate = new DateTime(Convert.ToInt32(currentTime.Year), Convert.ToInt32(currentTime.Month), Convert.ToInt32(currentTime.Day)).ToString("dd/MM/yyyy");
        //    if (dateStringBDFormat != null && dateStringBDFormat != "")
        //    {
        //        string[] splitData = dateStringBDFormat.Split('/','-');
        //        if (splitData != null && splitData.Length > 1)
        //        {
        //            day = int.Parse(splitData[0]);
        //            month = int.Parse(splitData[1]);
        //            if(splitData[2].Contains(" "))
        //            {
        //                string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
        //                if (splitYearTime != null && splitYearTime.Length > 1)
        //                    year = int.Parse(splitYearTime[0]);
        //            }
        //            else
        //            {
        //                year = int.Parse(splitData[2]);
        //            }

        //        }
        //        else
        //        {
        //            day = int.Parse(dateStringBDFormat.Split('-')[0]);
        //            month = int.Parse(dateStringBDFormat.Split('-')[1]);
        //            if (splitData[2].Contains(" "))
        //            {
        //                string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
        //                if (splitYearTime != null && splitYearTime.Length > 1)
        //                    year = int.Parse(splitYearTime[0]);
        //            }
        //            else
        //            {
        //                year = int.Parse(splitData[2]);
        //            }
        //        }
        //        DateTime newDate = new DateTime(year, month, day);
        //        convertedDate = newDate.ToString();
        //    }
        //    return convertedDate;
        //}
    }
}
