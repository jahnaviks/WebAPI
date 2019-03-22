using RegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryLayer.Repository_Interface
{
    interface IRegisterRepository
    {
        List<Registration> GetList();
        void Register(Registration regs);
        string ValidateLogin(string email);
        Registration GetUserDetails(string email);
        void UpdateClick(Registration regs);
        void DeleteClick(string email);
    }
}