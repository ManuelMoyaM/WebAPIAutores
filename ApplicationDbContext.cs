using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;

namespace WebAPIAutores;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    //Esta linea de codigo se crea para que se cree una tabla en sql server con las propiedades de la clase autor
    public DbSet<Autor> Autores  {get; set;}
    
}
