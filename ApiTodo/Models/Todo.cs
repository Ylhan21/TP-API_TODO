public class Todo{
    public int Id { get; set; }
    public string? Task { get; set; }    
    public DateTime? Deadline { get; set; }
    public int? AgendaId { get; set; }  
    public Agenda? Agenda { get; set; }
    public Statuts Status{get;set;}
       
}