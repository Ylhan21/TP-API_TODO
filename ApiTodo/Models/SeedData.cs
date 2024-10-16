
public class SeedData
{
    public static void Init ()
    {
        TodoContext context = new TodoContext();
        // Add students
        Todo T1 = new Todo
        {
            Task = "Task 1",
            Completed = false,
            Deadline = DateTime.Now
        };

        Todo T2 = new Todo
        {
            Task = "Task 2",
            Completed = false
        };

        Todo T3 = new Todo
        {
            Task = "Task 3",
            Completed = true
        };
        context.Todo.AddRange(T1, T2, T3);
        context.SaveChanges();
    }

    public static void InitAgenda()
    {
        TodoContext context = new TodoContext();
        var T1 = context.Todo.FirstOrDefault(t => t.Task == "Task 1");
        var T2 = context.Todo.FirstOrDefault(t => t.Task == "Task 2");
        // Add agenda
        Agenda A1 = new Agenda
        {
            Name = "Chores",
            Todos = new List<Todo>
            {T1!, T2!}
        };
    
        Agenda A2 = new Agenda
        {
            Name = "Hollidays",
            Todos = new List<Todo>
            {}
        };
        context.Agendas.AddRange(A1, A2);
        context.SaveChanges();
    }
                
}
