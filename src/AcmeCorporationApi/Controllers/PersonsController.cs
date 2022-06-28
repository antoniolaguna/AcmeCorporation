using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeCorporationApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net;
using AcmeCorporationApi.DTOs;
using System;

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
        public async Task<IEnumerable<PersonDto>> Get()
        {
            return await _dbContext.Persons.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<PersonDto> Get(int id)
        {
            return await _dbContext.Persons.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Post(PersonDTO personDto)
        {
            PersonDto person = new PersonDto();
            person.Name = personDto.Name;
            person.Document = personDto.Document;
            person.Age = personDto.Age;
            person.DocumentType = personDto.DocumentType;
            bool validDocument = ValidateDocument(personDto.DocumentType, personDto.Document);
            bool validName = VaidateUniqueName(personDto.Name);

            if (validDocument )
            {
                if (validName)
                {
                    await _dbContext.Persons.AddAsync(person);
                    await _dbContext.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
                }
                else
                {
                    var result = new ObjectResult(new { error = "Invalid name. Not unique" })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    return result;
                }
               
            }
            else
            {
                var result = new ObjectResult(new { error = "Invalid document number" })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return result;
            }
           


        }

        private bool VaidateUniqueName(string name)
        {
           IEnumerable<PersonDto> personas = this.Get().Result;
            foreach(PersonDto person in personas)
            {
                if(person.Name == name)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateDocument(string documentType, string document)
        {
            if (documentType == null || document == null)
            {
                return false;
            }
            else
            {
                DocumentValidator dv = new DocumentValidator(documentType, document);
                return dv.isValid();
            }
            
        }
    }
}
