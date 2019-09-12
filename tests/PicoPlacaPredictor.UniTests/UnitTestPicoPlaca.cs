using Microsoft.VisualStudio.TestTools.UnitTesting;
using PicoPlacaPredictor.Infrastructure.Utils;
using System;

namespace PicoPlacaPredictor.UniTests
{
    [TestClass]
    public class UnitTestPicoPlaca
    {
        [TestMethod]
        [DataRow("2019-09-09", "08:47:12")]
        [DataRow("2019-09-02", "05:59:47")]
        [DataRow("2019-09-16", "20:00:00")]
        public void PlateNumberWithDateTimePicoPlaca(string date, string time)
        {
            // Arrange
            var plateNumber = "PCQ-8381";

            // Act
            bool canDrive = Validator.CanDriveCar(plateNumber, date, time);

            // Assert
            Assert.AreEqual(canDrive, false);
        }

        [TestMethod]
        [DataRow("2019-09-10", "08:47:39")]
        [DataRow("2019-09-16", "04:59:59")]
        [DataRow("2019-09-16", "20:00:01")]
        public void PlateNumberWithNotDateTimePicoPlaca(string date, string time)
        {
            // Arrange
            var plateNumber = "PCQ-8381";

            // Act
            bool canDrive = Validator.CanDriveCar(plateNumber, date, time);

            // Assert
            Assert.AreEqual(canDrive, true);
        }

        [TestMethod]
        [DataRow("PCQ-84")]
        [DataRow("PCQ-8d381")]
        [DataRow("PC-8381")]
        [DataRow("")]
        [DataRow(null)]
        public void InvalidPlateNumberShouldThrowInvalidCastException(string plateNumber)
        {
            // Arrange
            var date = "2019-09-09";
            var time = "08:47:16";

            // Act and Assert
            Assert.ThrowsException<InvalidCastException>(() => Validator.CanDriveCar(plateNumber, date, time));
        }

        [TestMethod]
        [DataRow("2019-09d-09")]
        [DataRow("2019-09-32")]
        [DataRow("2019-13-04")]
        [DataRow("")]
        [DataRow(null)]
        public void InvalidDateShouldThrowInvalidCastException(string date)
        {
            // Arrange
            var plateNumber = "PCQ-8381";
            var time = "08:47:23";

            // Act and Assert
            Assert.ThrowsException<InvalidCastException>(() => Validator.CanDriveCar(plateNumber, date, time));
        }

        [TestMethod]
        [DataRow("-10:15:15")]
        [DataRow("00:60:00")]
        [DataRow("23:69:11")]
        [DataRow("24:00:00")]
        [DataRow("141148")]
        [DataRow(":")]
        [DataRow("1:1:1")]
        [DataRow("1::")]
        [DataRow("::1")]
        [DataRow("")]
        [DataRow(null)]
        public void InvalidTimeShouldThrowInvalidCastException(string time)
        {
            // Arrange
            var platNumber = "PCQ-8381";
            var date = "2019-09-09";

            // Act and Assert
            Assert.ThrowsException<InvalidCastException>(() => Validator.CanDriveCar(platNumber, date, time));
        }
    }
}