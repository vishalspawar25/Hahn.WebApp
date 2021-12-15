using Hahn.ApplicatonProcess.Data.models;
using Hahn.ApplicatonProcess.Domain.services;
using Hahn.ApplicatonProcess.Web.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.Web.Controllers
{
    [Route("api/applicant")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ILogger<ApplicantController> _logger;
        public ApplicantController(IApplicantService applicantService, ILogger<ApplicantController> logger)        
        {
            _applicantService = applicantService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Applicants.
        /// </summary>
        /// <remarks>
        ///Gets all Applicants.
        /// </remarks>
        /// <returns>Collection of Applicant</returns>

        // GET: api/<ApplicantController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            Log.Information("get called");
            try
            {
                var data = _applicantService.GetAll();
                return Ok(data);
                //if (data.Any())
                //    return Ok(data);
                //else
                //    return Ok("No applicant found");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("An error occurred during processing this request");
            }           
        }

        /// <summary>
        /// Gets Applicant matching with specific id .
        /// </summary>       
        /// <param name="id">1</param>
        /// <returns>Applicant</returns>
        // GET api/<ApplicantController>/5
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           
            try
            {
                var data = _applicantService.GetById(id);
                if (data!=null)
                    return Ok(data);
                else
                    return Ok("No applicant found");

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("An error occurred during processing this request");
            }

        }

        /// <summary>
        /// Creates new Applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Applicant
        ///     {
        ///        
        ///        "name":"vishal",
        ///        "familyName":"pawar",
        ///        "email":"vishal@gmail.com",
        ///        "age":30,
        ///        "countryOfOrigin":"India",
        ///        "address":"1203,Kasarvadavli,Mumbai",
        ///        "isHired":true
        ///     }
        ///
        /// </remarks>
        /// <param name="value">Applicant object</param>
        /// <returns>void</returns>
        // POST api/<ApplicantController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Post([FromBody] Applicant value)
        {
            try
            {
                var validator = new ApplicantValidator();
                var result = validator.Validate(value);
                if (result.IsValid)
                {
                    _applicantService.Insert(value);
                    return Ok("Applicant created successfully.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("An error occurred during processing this request");
            }          
        }

        /// <summary>
        /// Updates Applicant
        /// </summary>         
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Applicant
        ///     {
        ///        "id":1,
        ///        "name":"vishal",
        ///        "familyName":"pawar",
        ///        "email":"vishal@gmail.com",
        ///        "age":31,
        ///        "countryOfOrigin":"Germany",
        ///        "address":"1203,Kasarvadavli,Mumbai",
        ///        "isHired":true
        ///     }
        ///
        /// </remarks>
        /// <param name="id">1</param>
        /// <param name="value">applicant object</param>
        /// <returns>void</returns>
        // PUT api/<ApplicantController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] Applicant value)
        {
            try
            {
                var validator = new ApplicantValidator();
                var result = validator.Validate(value);
                _applicantService.Update(value);
                return Ok("Applicant updated successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("An error occurred during processing this request");
            }
            
        }

        /// <summary>
        /// Deletes Applicant
        /// </summary>     
        ///  <remarks>
        /// Deletes Applicant
        ///  </remarks>
        /// <param name="id">1</param>
        /// <returns>void</returns>

        // DELETE api/<ApplicantController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            try
            {
                _applicantService.Delete(id);
                return Ok("Applicant deleted successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("An error occurred during processing this request");
            }
           
        }
    }
}
