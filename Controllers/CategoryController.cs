using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewApp.Models.DTOs;
using NewApp.Entities;
using NewApp.Services.Generic;
using NewApp.Controllers;

namespace my_new_app.Controllers
{

    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : GenericCRUDController<Category, CategoryModel, IService<Category, CategoryModel>>
    {

        private List<CategoryModel> categories = new List<CategoryModel>();

        public CategoryController(IService<Category, CategoryModel> service): base(service)
        {
        }
    }
}
