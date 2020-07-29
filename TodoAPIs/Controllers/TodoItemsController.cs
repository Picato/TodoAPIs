using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoAPIs.Models;

namespace TodoAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly AppDbContext db = null;
        public TodoItemsController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public List<TodoItem> Get()
        {
            return db.TodoItems.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var todoItem = await db.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        [HttpPost]
        public void Post([FromBody]TodoItem item)
        {
            if (ModelState.IsValid)
            {
                db.TodoItems.Add(item);
                db.SaveChanges();
            }
        }
    }
}
