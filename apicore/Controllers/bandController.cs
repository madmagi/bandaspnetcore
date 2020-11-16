using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using apicore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apicore.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class bandController : ControllerBase
    {

        static readonly IBandRepository bandRepository = new BandRepository();

        // GET: api/<bandController>
       // [HttpGet]
        
//        public IEnumerable<Band> Get()
//        {
//            return bandRepository.GetAllBands();
//        }

        // GET api/<bandController>/5
        [HttpGet("{id}")]
        public Band Get(string id)
        {
            return bandRepository.GetBand(id);
        }


        [HttpGet]
        public IEnumerable<Band> Get([FromQuery(Name = "rating")] int rating,[FromQuery(Name = "year")] int year)
        {
            if (rating == 0 && year == 0)
                return bandRepository.GetAllBands();
            else
                return bandRepository.GetBandByQuery(rating, year);
        }


        // POST api/<bandController>
        [HttpPost]
        public ActionResult Post([FromBody] Band value)
        {
            Band band;
            band = bandRepository.AddBand(value);
            if (band != null)
            {
                return StatusCode((int)HttpStatusCode.Created,band);
            }
            return BadRequest();
        }

        // PUT api/<bandController>/5
        [HttpPut]
        public ActionResult Put([FromBody] Band value)
        {
            Band band;
            band = bandRepository.UpdateBand(value);
            if (band != null)
            {
                return StatusCode((int)HttpStatusCode.Accepted, band);
            }

            return BadRequest();
        }

        // DELETE api/<bandController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (bandRepository.DeleteBand(id))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("{band}/{id}")]
        public Band Patch(string band, int id)
        {
            return bandRepository.UpdateRating(band,id);
        }

    }
}
