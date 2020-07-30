using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Supermarket.API.Domain.Models;
using Supermarket.API.Models;

namespace Supermarket.API.Controllers  
{  
    [Route("api/[controller]")]    
    [ApiController]     
    public class AccountController : BaseController  
    {     
        private readonly UserManager<ApplicationUser> userManager;  
        private readonly SignInManager<ApplicationUser> signInManager;
        private Microsoft.AspNetCore.Identity.SignInResult result;

        public AccountController(UserManager<ApplicationUser> userManager,  
            SignInManager<ApplicationUser> signInManager, IConfiguration _config) : base(_config) 
        {  
            this.userManager = userManager;  
            this.signInManager = signInManager;  
            _config = base._config;
        }  
        
        [Route("Register")]    
        [HttpPost]  
        //[ValidateAntiForgeryToken]  
        public async Task<IActionResult> Register([FromBody]RegisteredUser model)  
        {  
            DateTime serviceStartTime = DateTime.Now;
            if (ModelState.IsValid)  
            {  
                var user = new ApplicationUser  
                {  
                    FirstName = model.FirstName,  
                    LastName = model.LastName,  
                    PhoneNumber = model.PhoneNumber,  
                    Email = model.Email,
                    UserName = string.IsNullOrEmpty(model.Email) ? model.PhoneNumber : model.Email,  
                    AadharNumber = model.AadharNumber,
                    Address = model.Address
                };  
                var result = await userManager.CreateAsync(user, model.Password);  
                if (result.Succeeded)  
                {  
                    await signInManager.PasswordSignInAsync(user, model.Password,  false, false); 
                }  

                foreach (var error in result.Errors)  
                {  
                    ModelState.AddModelError("", error.Description); 
                }  
                if (ModelState.ErrorCount>0)
                {
                    return ErrorAsync(result.Errors,this.Request.Method, result);
                }
            }  
            return Ok(new Result{ Status = HttpStatusCode.OK.ToString(), StartTime = serviceStartTime, EndTime = DateTime.Now });  
        }

        [Route("Login")]  
        [AllowAnonymous]    
        [HttpPost]    
        public async Task<IActionResult> Login([FromBody]LoginUser login)    
        {    
            DateTime serviceStartTime = DateTime.Now;
            IActionResult response = Unauthorized();    
            if (ModelState.IsValid)  
            {  
                var user = await AuthenticateUser(login);   
                if (user != null)    
                {    
                    var tokenString = GenerateJSONWebToken(user);    
                    response = Ok(new Result{ token = "bearer" + " "+ tokenString });    
                }    
            } 
    
    
            return  response;    
        }    
    
        private string GenerateJSONWebToken(ApplicationUser userInfo)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],    
              _config["Jwt:Issuer"],    
              null,    
              expires: DateTime.Now.AddMinutes(120),    
              signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }    
    
        private async Task<ApplicationUser> AuthenticateUser(RegisteredUser login)    
        {    
            ApplicationUser user = null; 
            ApplicationUser phoneUser = null; 
            var users = await userManager.Users.ToListAsync();

            
            if(!string.IsNullOrEmpty(login.PhoneNumber))
                phoneUser = users.Find(p => p.PhoneNumber == login.PhoneNumber ) ; 

             if(!string.IsNullOrEmpty(login.Email))  
                result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
                else if(!string.IsNullOrEmpty(login.PhoneNumber))
                    result = await signInManager.PasswordSignInAsync(phoneUser.Email, login.Password, false, false);
                    else
                    result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            //Validate the User Credentials     
            if (result.Succeeded)    
            {    
               user = users.Find(p => p.Email == login.Email ) ;
               if(user == null)
                user = users.Find(p => p.PhoneNumber == login.PhoneNumber ) ;
                if(user == null)
                user = users.Find(p => p.UserName == login.UserName ) ;
            }    
            return user ;    
        }  
    }  
}  