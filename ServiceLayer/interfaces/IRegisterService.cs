using RegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.interfaces
{
    interface IRegisterService
    {
        List<Registration> GetAccountDetails();
        void RegisterDetails(Registration regs);
        string ValidateLogin(string email);
        Registration GetUserDetails(string email);
        void UpdateClick(Registration regs);
        void DeleteClick(string email);
    }
}