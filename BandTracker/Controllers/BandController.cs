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
        // [HttpGet("/venues-by-band/{id}")]
        // public ActionResult BandByVenue(int id)
        // {
        //     List<Venue> allVenues = List.GetAllVenues();
        //     return View(allVenues);
        // }
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
    }
}
