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
        // [HttpGet("/bands-by-venues/{id}")]
        // public ActionResult BandByVenue(int id)
        // {
        //     List<Venue> allVenues = List.GetAllVenues();
        //     return View(allVenues);
        // }
        [HttpPost("/create-venue")]
        public ActionResult CreateVenue()
        {
            Venue newVenue = new Venue(Request.Form["venue-name"]);
            newVenue.SaveVenue();
            return RedirectToAction("AllVenues");
        }
    }
}
