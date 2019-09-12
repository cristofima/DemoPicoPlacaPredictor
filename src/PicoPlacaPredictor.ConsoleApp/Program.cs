using PicoPlacaPredictor.Infrastructure.Utils;
using System;

namespace PicoPlacaPredictor.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var plateNumber = "PCQ-8381";
            var datr = "2019-09-09";
            var time = "09:58:12";

            bool canDrive = Validator.CanDriveCar(plateNumber, datr, time);

            Console.WriteLine($"PLATE_NUMBER {plateNumber} in DATE {datr} in TIME {time} can drive: {canDrive}");
        }
    }
}