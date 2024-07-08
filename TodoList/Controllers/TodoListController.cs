using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListController(TodoContext context)
        {
            _context = context;
        }

        // Get dell'intera TodoList
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return _context.TodoItems.ToList();
        }

        // Get del singolo item della TodoList con l'id
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(long id)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null)
            {
                return NotFound(); // Errore 404
            }

            return todoItem;
        }

        // Aggiunta di un item alla TodoList
        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem); // Risposta 201 (Header con url dell'item creato)
        }

        // Modifica di un item della TodoList
        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // Cancellazione di un item della TodoList
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();

            return NoContent();
        }

        // Marcare un item della TodoList come completato
        [HttpPut("{id}/markcomplete")]
        public IActionResult MarkAsComplete(long id, [FromBody] bool isComplete)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.IsComplete = isComplete;

            _context.Entry(todoItem).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
    }
}