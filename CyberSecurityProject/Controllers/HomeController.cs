using AutoMapper;
using CyberSecurityProject.Data;
using CyberSecurityProject.Data.Entities;
using CyberSecurityProject.Helpers;
using CyberSecurityProject.Models;
using Humanizer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace CyberSecurityProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger,IMapper mapper, ApplicationDbContext context,IConfiguration configuration)
        {
            _logger = logger;
            this.mapper = mapper;
            this.context = context;
            this.configuration = configuration;
        }


        //Sql-injection Here
        public IActionResult Index(string searchValue)
        {
            List<Meal> meals = new List<Meal>();

            if (!string.IsNullOrEmpty(searchValue))
            {
                String connectionString = configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String command = "Select * From Meals Where Name like @Name";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@Name", searchValue);
                    connection.Open();
                    
                   SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Meal meal = new Meal();
                        meal.Name = reader["Name"].ToString();
                        meal.ImageUrl = reader["ImageUrl"].ToString();
                        meals.Add(meal);                    
                    }
                }
            }
            else
                meals = context.Set<Meal>().ToList();


              return View(meals);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Admin(MealViewModel model)
        {          
            if (ModelState.IsValid)
            {
                model.ImageUrl = DocumentSettings.UploadFile(model.Image, "images");
                Meal meal = new Meal()
                {
                    Name = model.Name,
                    ImageUrl= model.ImageUrl
                };
                await context.Set<Meal>().AddAsync(meal);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
