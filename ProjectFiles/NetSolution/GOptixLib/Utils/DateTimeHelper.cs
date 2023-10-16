using System;

namespace GOptixLib.Utils{


public class DateTimeHelper
{
    public static DateTime GetWeekFirstDaySun(DateTime datetime)
    {
        int num = Convert.ToInt32(datetime.DayOfWeek);
        int num2 = -1 * num;
        string value = datetime.AddDays(num2).ToString("yyyy-MM-dd");
        return Convert.ToDateTime(value);
    }

    public static DateTime GetWeekFirstDayMon(DateTime datetime)
    {
        int num = Convert.ToInt32(datetime.DayOfWeek);
        num = ((num == 0) ? 6 : (num - 1));
        int num2 = -1 * num;
        string value = datetime.AddDays(num2).ToString("yyyy-MM-dd");
        return Convert.ToDateTime(value);
    }

    public static DateTime GetWeekLastDaySat(DateTime datetime)
    {
        int num = Convert.ToInt32(datetime.DayOfWeek);
        int num2 = 7 - num - 1;
        string value = datetime.AddDays(num2).ToString("yyyy-MM-dd");
        return Convert.ToDateTime(value);
    }

    public static DateTime GetWeekLastDaySun(DateTime datetime)
    {
        int num = Convert.ToInt32(datetime.DayOfWeek);
        num = ((num == 0) ? 7 : num);
        int num2 = 7 - num;
        string value = datetime.AddDays(num2).ToString("yyyy-MM-dd");
        return Convert.ToDateTime(value);
    }

    public static DateTime GetMonthLastDay(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1,0,0,0).AddMonths(1).AddDays(-1);
    }

    public static DateTime GetMonthFirstDay(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1,0,0,0);
    }
}
}