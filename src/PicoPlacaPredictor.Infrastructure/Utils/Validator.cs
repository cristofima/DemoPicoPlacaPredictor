using System;
using System.Text.RegularExpressions;

namespace PicoPlacaPredictor.Infrastructure.Utils
{
    public class Validator
    {
        /// <summary>
        /// Validates a PlateNumber
        /// </summary>
        /// <param name="plateNumber">Plate Number</param>
        /// <example>PFT-0512</example>
        public static bool IsValidPlateNumber(string plateNumber)
        {
            if (string.IsNullOrWhiteSpace(plateNumber))
            {
                return false;
            }

            var regexPlateNumber = new Regex(@"^[A-Z]{3}-[0-9]{3,4}$");
            if (!regexPlateNumber.IsMatch(plateNumber))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a string with the format yyyy-MM-dd
        /// </summary>
        /// <param name="date">Date</param>
        public static bool IsValidDate(string date)
        {
            var canParseDate = DateTime.TryParse(date, out _);

            return canParseDate;
        }

        /// <summary>
        /// Converts the specified string (in the format HH:mm:ss) representation of a time to its <see cref="TimeSpan"/> equivalent and returns a value that indicates whether the conversion succeeded
        /// </summary>
        /// <param name="time">A string representation in the format HH:mm:ss</param>
        /// <param name="result">If the conversion was succeeded, in this variable is going to save the conversion result</param>
        public static bool TryParseTime(string time, out TimeSpan result)
        {
            if (time == null)
            {
                return false;
            }

            var regexTime = new Regex(@"^([0-1][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$");
            if (!regexTime.IsMatch(time))
            {
                return false;
            }

            var arrayTime = time.Split(':');
            var hours = int.Parse(arrayTime[0]);
            var minutes = int.Parse(arrayTime[1]);
            var seconds = int.Parse(arrayTime[2]);

            result = new TimeSpan(hours, minutes, seconds);

            return true;
        }

        /// <summary>
        /// Indicates if the given time it's in the "Pico y Placa" 's range
        /// </summary>
        /// <param name="time">Time</param>
        public static bool IsTimeInPicoPlaca(TimeSpan time)
        {
            return time >= Constants.INIT_TIME && time <= Constants.FINAL_TIME;
        }

        /// <summary>
        /// Indicates if a car with a given plate number can drive in Quito in a given date and time
        /// </summary>
        /// <param name="plateNumber">Plate Number</param>
        /// /// <param name="date">Date in format yyyy-MM-dd</param>
        /// /// <param name="time">Time in format HH:mm:ss</param>
        /// <exception cref="InvalidCastException">Thrown when one parameter is invalid.</exception>
        public static bool CanDriveCar(string plateNumber, string date, string time)
        {
            if (!IsValidPlateNumber(plateNumber))
            {
                throw new InvalidCastException($"Invalid plate number for '{plateNumber}'");
            }

            if (!IsValidDate(date))
            {
                throw new InvalidCastException($"Invalid date for '{date}'");
            }

            TimeSpan timeSpan;

            if (!TryParseTime(time, out timeSpan))
            {
                throw new InvalidCastException($"Invalid time for '{time}'");
            }

            if (!IsTimeInPicoPlaca(timeSpan))
            {
                return true;
            }

            var lastDigit = int.Parse(plateNumber[plateNumber.Length - 1].ToString());
            var dayWeekToDrive = DateTime.Parse(date).DayOfWeek;
            var listDigitInDayWeek = Constants.DAYS_PICO_PLACA[dayWeekToDrive];

            return !listDigitInDayWeek.Contains(lastDigit);
        }
    }
}