using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/list")]
public class ListController : ControllerBase
{
    private readonly TodoContext _context;

    public ListController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/list
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetLists()
    {
        // Inclure les todos associés lors de la récupération des listes
        var lists = _context.Agendas
                            .Include(l => l.Todos);  // Charge les todos associés à chaque liste

        return await lists.ToListAsync();
    }

    // GET: api/list/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Agenda>> GetList(int id)
    {
        // Récupère une liste spécifique avec les todos associés
        var list = await _context.Agendas
                                 .Include(l => l.Todos)  // Charge également les todos associés
                                 .FirstOrDefaultAsync(l => l.Id == id);

        if (list == null)
        {
            return NotFound();
        }

        return list;
    }
}
