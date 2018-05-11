using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;

namespace BandTracker.Controllers
{
    public class VenueController : Controller
    {
        [HttpGet("/all-venues")]
        public ActionResult AllVenues()
        {
            List<Venue> allVenues = Venue.GetAllVenues();
            return View(allVenues);
        }
        [HttpPost("/create-venue")]
        public ActionResult CreateVenue()
        {
            Venue newVenue = new Venue(Request.Form["venue-name"]);
            newVenue.SaveVenue();
            return RedirectToAction("AllVenues");
        }
        [HttpGet("/add-band-to-venue")]
        public ActionResult AddBandToVenue()
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            List<Band> allBands = Band.GetAllBands();
            model.Add("allBands", allBands);
            List<Venue> allVenues = Venue.GetAllVenues();
            model.Add("allVenues", allVenues);
            return View(model);
        }
        [HttpPost("/added-band-to-venue")]
        public ActionResult AddedBandToVenue()
        {
            Band newBand = Band.FindBand(Int32.Parse(Request.Form["band"]));
            Venue newVenue = Venue.FindVenue(Int32.Parse(Request.Form["venue"]));

            newBand.AddVenue(newVenue);
            return RedirectToAction("AddBandToVenue"); //i cannot figure out how to redirect the user back to the home page once they do this action.
        }
        [HttpGet("/bands-by-venue/{id}")]
        public ActionResult VenueBands(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            Venue selectedVenue = Venue.FindVenue(id);
            List<Band> venueBands = selectedVenue.GetBands();
            List<Band> allBands = Band.GetAllBands();
            model.Add("selectedVenue", selectedVenue);
            model.Add("venueBands", venueBands);
            model.Add("allBands", allBands);
            return View(model);
        }
        [HttpPost("/add-band-to-venue-list/{id}")]
        public ActionResult UpdateBandToVenueList(int id)
        {
            Band newBand = Band.FindBand(Int32.Parse(Request.Form["band"]));
            Venue newVenue = Venue.FindVenue(id);
            int newId = newVenue.GetVenueId();
            newVenue.AddBand(newBand);
            return RedirectToAction("VenueBands",  new { id = newId });
        }
    }
}
