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
          Band.DeleteAllBands();
          Venue.DeleteAllVenues();
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
        [TestMethod]
        public void SaveVenue_SaveVenueToDb_True()
        {
          //Arrange
          Venue testVenue = new Venue("Fenway");
          //Act
          testVenue.SaveVenue();
          List<Venue> allVenues = Venue.GetAllVenues();
          List<Venue> manualVenueList = new List<Venue>{testVenue};

          //Assert
          CollectionAssert.AreEqual(manualVenueList, allVenues);
        }
        [TestMethod]
        public void GetBands_GetBandFromDbForASpecificVenue_True()
        {
          //Arrange
          Venue testVenue = new Venue("MSG");
          //Act
          testVenue.SaveVenue();
          int bandCount = testVenue.GetBands().Count;

          //Assert
          Assert.AreEqual(0, bandCount);
        }
        [TestMethod]
        public void AddBand_SaveBandToVenue_True()
        {
          //Arrange
          Band testBand = new Band("My Morning Jacket");
          Band testBand1 = new Band("Cat Power");
          Venue testVenue = new Venue("DCU Center");

          //Act
          testBand.SaveBand();
          testBand1.SaveBand();
          testVenue.SaveVenue();
          testVenue.AddBand(testBand);
          testVenue.AddBand(testBand1);
          List<Band> venueBands = testVenue.GetBands();
          List<Band> manualBandList = new List<Band>{testBand, testBand1};

          //Assert
          CollectionAssert.AreEqual(manualBandList, venueBands);
        }
    }
}
