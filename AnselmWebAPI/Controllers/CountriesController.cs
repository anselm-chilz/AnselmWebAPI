using AnselmWebAPI.Models;
using AnselmWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnselmWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly CountryService _countryService;

        private readonly ILogger<CountriesController> _logger;

        public CountriesController(CountryService countryService, ILogger<CountriesController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }
        
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Country>> Get() =>
            _countryService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCountry")]
        public ActionResult<Country> Get(string id)
        {
            var country = _countryService.Get(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        [HttpPost]
        public ActionResult<Country> Create(Country country)
        {
            _countryService.Create(country);

            return CreatedAtRoute("GetCountry", new { id = country.Id.ToString() }, country);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Country countryIn)
        {
            var country = _countryService.Get(id);

            if (country == null)
            {
                return NotFound();
            }

            _countryService.Update(id, countryIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var country = _countryService.Get(id);

            if (country == null)
            {
                return NotFound();
            }

            _countryService.Remove(country.Id);

            return NoContent();
        }
    }
}
