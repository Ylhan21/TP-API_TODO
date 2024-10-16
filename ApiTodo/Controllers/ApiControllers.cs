using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly TodoContext _context;
    public TodoController(TodoContext context)
    {
        _context = context;
    }


    // GET: api/todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        // Get items
        var todos = _context.Todo.Include(t => t.Agenda);
        return await todos.ToListAsync();
    }

    // GET: api/todo/2
    [HttpGet("{id}")]
    [SwaggerOperation(
    Summary = "Get an item by id",
    Description = "Returns a specific item targeted by its identifier")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Item found", typeof(Todo))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Item not found")]



    public async Task<ActionResult<Todo>> GetItem(int id)
    {
        // Find a specific item
        // SingleAsync() throws an exception if no item is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var item = await _context.Todo.Include(t => t.Agenda).SingleOrDefaultAsync(t => t.Id == id);


        if (item == null)
            return NotFound();


        return item;
    }

    // POST: api/item
    [HttpPost]
    public async Task<ActionResult<Todo>> PostItem(Todo item)
    {
        _context.Todo.Add(item);
        await _context.SaveChangesAsync();


        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    // PUT: api/item/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(int id, Todo item)
    {
        if (id != item.Id)
            return BadRequest();


        _context.Entry(item).State = EntityState.Modified;


        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Todo.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }


        return NoContent();
    }

    // DELETE: api/item/2
    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _context.Todo.FindAsync(id);


        if (item == null)
            return NotFound();


        _context.Todo.Remove(item);
        await _context.SaveChangesAsync();


        return NoContent();
    }

}

