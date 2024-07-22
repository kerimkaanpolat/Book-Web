using Book_Web.Data;
using Book_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpPost]
        [Route("Registeration")]
        public IActionResult Registeration(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objUser = dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (objUser == null)
            {

                dbContext.Users.Add(new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                });
                dbContext.SaveChanges();
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User already exists with the same email adress");
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult IsLogin(LoginDTO loginDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (user != null)
            {
                return Ok("Login success");
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int id)
        {
            var User = dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (User != null)
                return Ok();
            else
                return NoContent();
        }
    }
}
