using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApis.Contract;
using TestApis.Model;

namespace TestApis.Respository
{
  public interface IUsers
    {
        dynamic Signin(LoginModel login);
        dynamic SignUp(Users user);
    }
}
