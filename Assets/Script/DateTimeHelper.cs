using System;

public static class DateTimeHelper
{
    /// <summary>
    /// Converts a given DateTime into a Unix timestamp
    /// </summary>
    /// <param name="value">Any DateTime</param>
    /// <returns>The given DateTime in Unix timestamp format</returns>
    public static long ToUnixTimestamp(this DateTime value)
    {
        return (long)(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    /// <summary>
    /// Gets a Unix timestamp representing the current moment
    /// </summary>
    /// <param name="ignored">Parameter ignored</param>
    /// <returns>Now expressed as a Unix timestamp</returns>
    public static long UnixTimestamp(this DateTime ignored)
    {
        return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    /// <summary>
    /// Returns a local DateTime based on provided unix timestamp
    /// </summary>
    /// <param name="timestamp">Unix/posix timestamp</param>
    /// <returns>Local datetime</returns>
    public static DateTime ParseUnixTimestamp(long timestamp)
    {
        return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(timestamp).ToUniversalTime();
    }
    public static DateTime ParseUnixTimestampUTC(long timestamp,float timezone)
    {
        return ParseUnixTimestampNormal(timestamp).AddHours(-timezone);
    }
    public static DateTime ParseUnixTimestampNormal(long timestamp)
    {
        return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(timestamp);
    }
    public static long ToUnixTimestampNormal(this DateTime value)
    {
        return (long)(value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }


    public static string TimeToString_HMS_TWO(TimeSpan t)
    {
        if (t.Ticks < 0)
            return string.Format("{0}:{1}", 0, 0);
        if (t.TotalDays >= 1)
        {
            return string.Format("{0}d:{1}h", t.Days, t.Hours);
        }
        else if (t.TotalHours >= 1)
        {
            return string.Format("{0}h:{1}m", t.Hours, t.Minutes);
        }
        else
        {
            return string.Format("{0}m:{1}s", t.Minutes, t.Seconds);
        }
    }
    public static string TimeToString_HMS(TimeSpan t)
    {
        if (t.Ticks < 0)
            return string.Format("{0}:{1}", 0, 0);
        if (t.TotalDays >= 1)
        {
            return string.Format("{0}d:{1}h:{2}m", t.Days, t.Hours, t.Minutes);
        }
        else if (t.TotalHours >= 1)
        {
            return string.Format("{0}h:{1}m", t.Hours, t.Minutes);
        }
        else
        {
            return string.Format("{0}m:{1}s", t.Minutes, t.Seconds);
        }
    }
    public static string TimeToString_HM(TimeSpan t)
    {
        if(t.Days >= 1)
        {
            return string.Format("{0}d:{1}h" , t.Days , t.Hours);
        }
        if (t.TotalHours >= 1)
        {
            return string.Format("{0}h:{1}m", t.Hours, t.Minutes);
        }
        if(t.TotalMinutes >= 1)
        {
            return string.Format("{0}m:{1}s", t.Minutes, t.Seconds);
        }
        if(t.TotalSeconds >= 1)
        {
            return string.Format("{0}m:{1}s", 0, t.Seconds);
        }
        return null;
    }
}