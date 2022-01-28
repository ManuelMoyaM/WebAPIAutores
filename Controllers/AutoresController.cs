using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers;

[ApiController]
//Tambien se puede encontrar de la siguente manera o placeholder
//[Route("api/[controller])]
[Route("api/autores")]

public class AutoresController: ControllerBase
{
    private readonly ApplicationDbContext context;

    public AutoresController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]//api/autores
    [HttpGet("listado")] //api/autores/listado
    [HttpGet("/listado")]//listado
    public List<Autor> Get()
    {
        return context.Autores.Include(x => x.Libros).ToList();
    }



    [HttpGet("{nombre}")]
                                                //[FromRoute] Nos indica que rescatamos la variable desde la ruta en este caso
    public async Task<ActionResult<Autor>> Get([FromRoute] string nombre)
    {

        var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

        if (autor == null)
        {
            return NotFound();
            
        }
        return autor;
    }



    [HttpPost]

    public async Task<ActionResult> Post(Autor autor)
    {
        context.Add(autor);
        await context.SaveChangesAsync();
        return Ok();
    }
    //PUT se entiende como un método para "cargar" cosas en un URI particular, o sobrescribir lo que ya está en ese URI. POST, 
    //por otro lado, es una forma de enviar datos RELACIONADOS con un URI determinado. Por lo que sé, PUT se utiliza principalmente para actualizar los registros.

    //PUT pone un recurso en la dirección especificada en la URL. Exactamente en esa dirección. Si no existe, lo crea, si existe lo reemplaza.
    //POST envía datos a una URL para que el recurso en esa URI los maneje.

    //Se le agrega un parametro de ruta para que el id que se coloque apunte al que este creado
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(Autor autor, int id)
    {

        if (autor.Id != id)
        {
            return BadRequest("El id del autor no coincide con el id de la URL");
        }

        var existe =  await context.Autores.AnyAsync(x => x.Id == id);

        if (!existe)
        {
            return NotFound();
        }

        context.Update(autor);
        //Para que se actualice a nivel de base de datos
        await context.SaveChangesAsync();
        return Ok();

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        //Consultamos si existe el dato en la base de datos
        var existe = await context.Autores.AnyAsync(x => x.Id == id);

        if (!existe)
        {
            return NotFound();
        }
        context.Remove(new Autor() {Id =id});
        await context.SaveChangesAsync();
        return Ok();

    }

}


