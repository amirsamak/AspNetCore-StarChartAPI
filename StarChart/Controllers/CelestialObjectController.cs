using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}" , Name="GetById")]
        public IActionResult GetById(int id)
        {
            var celestialobject = _context.CelestialObjects.Find(id);
            if (celestialobject==null)
            {
                return NotFound();
            }
            celestialobject.Satellites = _context.CelestialObjects.
                Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celestialobject);
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects.
                Where(e => e.Name == name).ToList();
            if (!celestialObjects.Any())
            {
                return NotFound();
            }
            foreach (var item in celestialObjects)
            {
                item.Satellites = _context.CelestialObjects.
                Where(e => e.OrbitedObjectId == item.Id).ToList();
            }
            
            return Ok(celestialObjects);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialobjects = _context.CelestialObjects.ToList();
            foreach (var item in celestialobjects)
            {
                item.Satellites=_context.CelestialObjects.
                Where(e => e.OrbitedObjectId == item.Id).ToList();
            }
            return Ok(celestialobjects);
        }
    }
}
