namespace ATS.Classes
{
    #region Usings

    using System;

    #endregion

    public static class StaticTime
    {
        public static DateTime CurrentTime;

        public static void AddSeconds(int seconds)
        {
            CurrentTime = CurrentTime.AddSeconds(seconds);
        }

        public static void AddMinutes(int minutes)
        {
            CurrentTime = CurrentTime.AddMinutes(minutes);
        }

        public static void AddDays(int days)
        {
            CurrentTime = CurrentTime.AddDays(days);
        }
    }
}
