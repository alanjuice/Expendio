﻿using Expendio.Data;
using Expendio.DTO;
using Expendio.Mappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Expendio.Controllers
{
    public class AccountController : Controller
    {
        private readonly ExpendioDbContext _context;
        public AccountController(ExpendioDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupUserDto signupUser)
        {
            if (!ModelState.IsValid)
            {
                return View(signupUser);
            }
            var user = signupUser.ToUser();
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                return NoContent();
            }            
        }


        [HttpPost]
        public  async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email==loginUserDto.Email);
            if (user == null)
            {
                return Unauthorized("User doesn't exist");
            }
            if (loginUserDto.Password != user.Password)
            {
                return Unauthorized("Incorrect Password");
            }

            var claims = new List<Claim>
            {
                new Claim("ID", user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),             
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

            return RedirectToAction("Index", "User");
        }
    }
}
