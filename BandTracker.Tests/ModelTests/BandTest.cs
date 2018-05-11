using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;
using System.Collections.Generic;
using System;



namespace BandTracker.Tests
{

    [TestClass]
    public class BandTests : IDisposable
    {
        public void Dispose()
        {

        }
        public BandTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
        }
        [TestMethod]
        public void GetAllBands_DbStartsEmpty_0()
        {
          //Arrange
          //Act
          int result = Band.GetAllBands().Count;

          //Assert
          Assert.AreEqual(0, result);
        }

    }
}
