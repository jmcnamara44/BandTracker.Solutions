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
        // [HttpGet("/bands-by-venues/{id}")]
        // public ActionResult BandByVenue(int id)
        // {
        //     List<Venue> allVenues = List.GetAllVenues();
        //     return View(allVenues);
        // }
    }
}
