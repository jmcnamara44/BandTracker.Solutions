using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;
using System.Collections.Generic;
using System;



namespace BandTracker.Tests
{

    [TestClass]
    public class VenueTests : IDisposable
    {
        public void Dispose()
        {

        }
        public VenueTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
        }
        [TestMethod]
        public void GetAllVenues_DbStartsEmpty_0()
        {
          //Arrange
          //Act
          int result = Venue.GetAllVenues().Count;

          //Assert
          Assert.AreEqual(0, result);
        }

    }
}
