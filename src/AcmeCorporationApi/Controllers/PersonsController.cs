using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeCorporationApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net;
using AcmeCorporationApi.DTOs;

namespace AcmeCorporationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly AcmeCorporationContext _dbContext;

        public PersonsController(ILogger<PersonsController> logger, AcmeCorporationContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _dbContext.Persons.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<Person> Get(int id)
        {
            return await _dbContext.Persons.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Post(PersonDTO personDto)
        {
            Person person = new Person();
            person.Name = personDto.Name;
            person.Document = personDto.Document;
            person.Age = personDto.Age;
            person.DocumentType = personDto.DocumentType;

            await _dbContext.Persons.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);


        }
    }
}
