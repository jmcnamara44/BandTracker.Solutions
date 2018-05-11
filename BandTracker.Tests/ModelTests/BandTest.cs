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
          Band.DeleteAllBands();
          Venue.DeleteAllVenues();
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
        [TestMethod]
        public void SaveBand_SaveBandToDb_True()
        {
          //Arrange
          Band testBand = new Band("Lord Huron");
          //Act
          testBand.SaveBand();
          List<Band> allBands = Band.GetAllBands();
          List<Band> manualBandList = new List<Band>{testBand};

          //Assert
          CollectionAssert.AreEqual(manualBandList, allBands);
        }
        [TestMethod]
        public void GetVenues_GetVenueFromDbForASpecificBand_True()
        {
          //Arrange
          Band testBand = new Band("Pink Floyd");
          //Act
          testBand.SaveBand();
          int venueCount = testBand.GetVenues().Count;

          //Assert
          Assert.AreEqual(0, venueCount);
        }
        [TestMethod]
        public void AddVenue_SaveVenueToBand_True()
        {
          //Arrange
          Band testBand = new Band("Explosions In The Sky");
          Venue testVenue = new Venue("Gillete");
          Venue testVenue1 = new Venue("Oakdale Theater");

          //Act
          testBand.SaveBand();
          testVenue.SaveVenue();
          testVenue1.SaveVenue();
          testBand.AddVenue(testVenue);
          testBand.AddVenue(testVenue1);
          List<Venue> bandVenues = testBand.GetVenues();
          List<Venue> manualVenueList = new List<Venue>{testVenue, testVenue1};

          //Assert
          CollectionAssert.AreEqual(manualVenueList, bandVenues);
        }

    }
}
