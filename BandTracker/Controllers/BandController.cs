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
    }
}
