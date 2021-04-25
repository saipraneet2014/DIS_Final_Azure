using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIS_Final_Azure.DataAccess;
using DIS_Final_Azure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DIS_Final_Azure.Controllers
{
    public class HomeController : Controller


    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            //_logger = logger;
            _context = context;
        }

                  
        public IActionResult Index()
        {
            return View("Home");
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew([Bind("ID, Station_name, Fuel_type, Owner_type_code, Street_address, City, State, Zip")] Fuel_Stations updated)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Fuel_Stations newStation = new Fuel_Stations(){
                        ID = updated.ID,
                        Station_name = updated.Station_name,
                        Fuel_type = updated.Fuel_type,
                        Owner_type_code = updated.Owner_type_code,
                        Street_address = updated.Street_address,
                        City = updated.City,
                        State = updated.State,
                        Zip = updated.Zip
                    };

                    _context.Fuel_Stations.Add(newStation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Thanks), new { message = "Thanks! The record has been added." });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                // Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(updated);
        }

        [Route("Search")]
        public async Task<IActionResult> Search(string fuelType, string city, string zipcode)
        {
            fuelType = (fuelType == null) ? "" : fuelType;
            city = (city == null) ? "" : city;
            zipcode = (zipcode == null) ? "" : zipcode;

            //var plist = new List<char>();
            var queryObject = from Fuel_Stations in _context.Fuel_Stations select Fuel_Stations;
            var fuelStations = queryObject.ToList();

            var plist = new List<Fuel_Stations>();
            var cityList = new List<string>();
            var resultList = new List<Fuel_Stations>();

            if (!(fuelType == "" && city == "" && zipcode == ""))
            {
                foreach (var fuelStation in fuelStations)
                {
                    var cityFlag = false;
                    var fuelTypeFlag = false;
                    var zipcodeFlag = false;
                    if (city == "")
                    {
                        cityFlag = true;
                    }
                    else
                    {
                        if (fuelStation.City.Equals(city))
                        {
                            cityFlag = true;
                        }
                    }
                    if (fuelType == "")
                    {
                        fuelTypeFlag = true;
                    }
                    else
                    {
                        if (fuelStation.Fuel_type.Equals(fuelType))
                        {
                            fuelTypeFlag = true;
                        }
                    }
                    if (zipcode == "")
                    {
                        zipcodeFlag = true;
                    }
                    else
                    {
                        if (fuelStation.Zip.Equals(zipcode))
                        {
                            zipcodeFlag = true;
                        }
                    }
                    if (cityFlag && fuelTypeFlag && zipcodeFlag)
                    {
                        resultList.Add(fuelStation);
                    }
                }
            }
            ViewBag.result = resultList;

            foreach (var fuelStation in fuelStations)
            {
                if (!cityList.Contains(fuelStation.City))
                {
                    cityList.Add(fuelStation.City);
                    plist.Add(new Fuel_Stations
                    {
                        City = fuelStation.City
                    });
                }
            }
            Console.WriteLine(plist);
            return await Task.FromResult(View(plist));
        }

        public async Task<IActionResult> Edit(string id)
        {
            Fuel_Stations fuelStation = _context.Fuel_Stations.Where(p => p.ID == id).FirstOrDefault();
            Fuel_Stations fsEdit = new Fuel_Stations()
            {
                ID = fuelStation.ID,
                Station_name = fuelStation.Station_name,
                Fuel_type = fuelStation.Fuel_type,
                Owner_type_code = fuelStation.Owner_type_code,
                Street_address = fuelStation.Street_address,
                City = fuelStation.City,
                State = fuelStation.State,
                Zip = fuelStation.Zip
            };

            return await Task.FromResult(View(fsEdit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Station_name, Fuel_type, Owner_type_code, Street_address, City, State, Zip")] Fuel_Stations updated)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Fuel_Stations fuelStations = _context.Fuel_Stations
                        .Where(p => p.ID == id)
                        .FirstOrDefault();
                    fuelStations.Station_name = updated.Station_name;
                    fuelStations.Fuel_type = updated.Fuel_type;
                    fuelStations.Owner_type_code = updated.Owner_type_code;
                    fuelStations.Street_address = updated.Street_address;
                    fuelStations.City = updated.City;
                    fuelStations.State = updated.State;
                    fuelStations.Zip = updated.Zip;
                    
                    _context.Update(fuelStations);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Thanks), new { message = "Thanks! The record has been edited." });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                // Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(updated);
        }

        public async Task<IActionResult> Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuelStations = await _context.Fuel_Stations
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID.Equals(id));

            if (fuelStations == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete: " + fuelStations.Station_name;

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(fuelStations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Fuel_Stations fuelStation = await _context.Fuel_Stations
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();

            if (fuelStation == null)
            {
                return RedirectToAction(nameof(Search));
            }

            try
            {
                _context.Fuel_Stations.Remove(fuelStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Thanks), new { message = "Thanks! The record has been deleted." });
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        public IActionResult Thanks(string message)
        {
            ViewBag.dispm = message;
            return View();
        }

        [Route("Datamodel")]
        public IActionResult Datamodel()
        {
            return View();
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("Stats")]
        public IActionResult Stats()
        {
            var queryObject = from EVs in _context.EVs join Fuel_Stations
                              in _context.Fuel_Stations on EVs.ID equals Fuel_Stations.ID
                              select new { EVid = EVs.Station_EV_ID, State = Fuel_Stations.State };
            var EVStations = queryObject.ToList();
            var evDict = new Dictionary<string, int>();
            foreach(var EVStation in EVStations)
            {
                if (evDict.ContainsKey(EVStation.State))
                {
                    evDict[EVStation.State] = evDict[EVStation.State] + 1;
                }
                else 
                {
                    evDict.Add(EVStation.State, 1);
                }
            }

            return View(evDict);
        }
        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*public ActionResult<Fuel_Stations> SearchFuelStation(string fuelType, string state, string city, string zipCode)
        {

            return null;
        }*/

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        /*public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
