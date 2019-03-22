using RegisterAPI.Models;
using RegisterAPI.RegisterRepository;
using ServiceLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer
{
    public class RegisterService : IRegisterService
    {
        public List<Registration> GetAccountDetails()
        {
            RegisterMapper regMap = new RegisterMapper();
            return regMap.GetList();
        }

        public void RegisterDetails(Registration regs)
        {
            RegisterMapper regMap = new RegisterMapper();
            regMap.Register(regs);
        }

        public string ValidateLogin(string email)
        {
            RegisterMapper regMap = new RegisterMapper();
            return regMap.ValidateLogin(email);
        }

        public Registration GetUserDetails(string email)
        {
            RegisterMapper regMap = new RegisterMapper();
            return regMap.GetUserDetails(email);
        }

        public void UpdateClick(Registration regs)
        {
            RegisterMapper regMap = new RegisterMapper();
            regMap.UpdateClick(regs);
        }

        public void DeleteClick(string email)
        {
            RegisterMapper regMap = new RegisterMapper();
            regMap.DeleteClick(email);
        }
    }
}