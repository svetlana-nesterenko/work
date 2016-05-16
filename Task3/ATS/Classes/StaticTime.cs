namespace ATS.Classes
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    /// Class used for representing the acceleration time.
    /// </summary>
    public static class StaticTime
    {
        /// <summary>
        /// The current time.
        /// </summary>
        public static DateTime CurrentTime;

        /// <summary>
        /// Adds the seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public static void AddSeconds(int seconds)
        {
            CurrentTime = CurrentTime.AddSeconds(seconds);
        }

        /// <summary>
        /// Adds the minutes.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        public static void AddMinutes(int minutes)
        {
            CurrentTime = CurrentTime.AddMinutes(minutes);
        }

        /// <summary>
        /// Adds the days.
        /// </summary>
        /// <param name="days">The days.</param>
        public static void AddDays(int days)
        {
            CurrentTime = CurrentTime.AddDays(days);
        }
    }
}
