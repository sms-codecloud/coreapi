using ARJAPIADo.DataAccess;
using ARJAPIADo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARJAPIADo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/Products
        [HttpGet]
        [Route("api/EmployeeList")]
        public ActionResult<IEnumerable<Employee>> Get()

        {
            return Ok(_employeeRepository.GetAllEmployees());
        }

        //// GET api/Products/5
        [HttpGet]
        [Route("api/EmployeeById/{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var product = _employeeRepository.GetEmployeeById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/Products
        [HttpPost]
        [Route("api/EmployeeSave")]
        public ActionResult Post([FromBody] Employee employee)
        {
            if (_employeeRepository.AddEmployee(employee))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //// PUT api/Products/5
        [HttpPost]
        [Route("api/EmployeeUpdate/{id}")]
        public ActionResult Put(int id, [FromBody] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            _employeeRepository.UpdateEmployee(employee);
            return NoContent();
        }

        ////// DELETE api/Products/5
        [HttpPost]
        [Route("api/EmployeeDelete/{id}")]
        public ActionResult Delete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return NoContent();
        }
        [HttpGet]
        [Route("api/HealthAPI")]
        public ActionResult HealthAPI()
        {
            return Ok("Runnig shankar");
        }
}
}
