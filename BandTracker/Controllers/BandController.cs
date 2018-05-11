using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;

namespace BandTracker.Controllers
{
    public class BandController : Controller
    {
        [HttpGet("/all-bands")]
        public ActionResult AllBands()
        {
            List<Band> allBands = Band.GetAllBands();
            return View(allBands);
        }
        [HttpPost("/create-band")]
        public ActionResult CreateBand()
        {
            Band newBand = new Band(Request.Form["band-name"]);
            newBand.SaveBand();
            return RedirectToAction("AllBands");
        }
        [HttpGet("/add-venue-to-band")]
        public ActionResult AddVenueToBand()
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            List<Band> allBands = Band.GetAllBands();
            model.Add("allBands", allBands);
            List<Venue> allVenues = Venue.GetAllVenues();
            model.Add("allVenues", allVenues);
            return View(model);
        }
        [HttpPost("/added-venue-to-band")]
        public ActionResult AddedVenueToBand()
        {
            Band newBand = new Band(Request.Form["band"]);
            newBand.SaveBand();
            Venue newVenue = new Venue(Request.Form["venue"]);
            newVenue.SaveVenue();
            newBand.AddVenue(newVenue);
            return RedirectToAction("AddVenueToBand"); //i cannot figure out how to redirect the user back to the home page once they do this action.
        }
        [HttpGet("/venues-by-band/{id}")]
        public ActionResult BandVenues(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            Band selectedBand = Band.FindBand(id);
            List<Venue> bandVenues = selectedBand.GetVenues();
            List<Venue> allVenues = Venue.GetAllVenues();
            model.Add("selectedBand", selectedBand);
            model.Add("bandVenues", bandVenues);
            model.Add("allVenues", allVenues);
            return View(model);
        }
        [HttpPost("/add-venue-to-band-list/{id}")]
        public ActionResult UpdateVenueToBandList(int id)
        {
            Venue newVenue = Venue.FindVenue(Int32.Parse(Request.Form["venue"]));
            Band newBand = Band.FindBand(id);
            int newId = newBand.GetBandId();
            newBand.AddVenue(newVenue);
            return RedirectToAction("BandVenues",  new { id = newId });
        }
    }
}
