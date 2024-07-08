using Microsoft.EntityFrameworkCore; // Contiene le classi per lavorare con i db e le entità
using TodoList.Models;

namespace TodoList.Data
{
    // Ereditare da DbContext
    // DbContext gestisce la connessione al db e traccia le entità
    public class TodoContext : DbContext
    {
        // Il tipo DbContextOptions<TodoContext> contiene 
        // la configurazione necessaria per il contesto del database
        // come la stringa di connessione e altri parametri di configurazione

        // Le opzioni vengono passate al costruttore della
        // classe base DbContext
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        // Tabella della TodoList nel database
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}