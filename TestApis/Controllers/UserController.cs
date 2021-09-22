using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApis.Contract;
using TestApis.Model;
using TestApis.Respository;

namespace TestApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUsers users;
        public UserController(IUsers _users)
        {
            users = _users;
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult Signup(Users user)
        {
           var r= users.SignUp(user);
            return Ok(r);
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin(LoginModel login)
        {
            var r = users.Signin(login);
            return Ok(r);
        }
    }
}