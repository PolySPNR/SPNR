using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scopus.Api.Client.Models.Common;
using SPNR.Core.Models.AuthorInfo;
using SPNR.Core.Services.Data;
using SPNR.Core.Services.Data.Contexts;

namespace SPNR.REST.Controllers
{
    [ApiController]
    [Route("/api/data")]
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;

        public DataController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        /// <summary>
        /// List all organizations
        /// </summary>
        /// <returns>List of all organizations</returns>
        [HttpGet("list/orgs")]
        public async Task<List<Organization>> ListOrganizations()
        {
            return await _dataService.GetOrganizations();
        }

        /// <summary>
        /// List all faculties of organization
        /// </summary>
        /// <param name="organizationId">Numeric ID of organization</param>
        /// <returns>Faculties of organization</returns>
        [HttpGet("list/faculties")]
        public async Task<List<Faculty>> ListFaculties([FromQuery] int organizationId)
        {
            return await _dataService.GetFaculties(organizationId);
        }

        /// <summary>
        /// List all departments of faculty
        /// </summary>
        /// <param name="facultyId">Numeric ID of faculty</param>
        /// <returns>Departments of faculty</returns>
        [HttpGet("list/departments")]
        public async Task<List<Department>> ListDepartments([FromQuery] int facultyId)
        {
            return await _dataService.GetDepartments(facultyId);
        }

        /// <summary>
        /// List all authors of department
        /// </summary>
        /// <param name="departmentId">Numeric ID of department</param>
        /// <returns>Authors of department</returns>
        [HttpGet("list/authors")]
        public async Task<List<Author>> ListAuthors([FromQuery] int departmentId)
        {
            return await _dataService.GetAuthors(departmentId);
        }

        [HttpPost]
        public async Task<HttpStatusCode> UpdateAuthor([FromBody] Author author)
        {
            await _dataService.UpdateAuthor(author);
            return HttpStatusCode.OK;
        }
    }
}