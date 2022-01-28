using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIAutores.Entidades;

public class Autor 
{
    public int Id {get; set;}
    //Obliga a que el campo sea requerido. En este caso el nombre 
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [StringLength(maximumLength:5, ErrorMessage ="El campo {0} no debe tener mas de {1} caracteres")]
    public string Nombre {get; set;}
    [Range(18, 120)]
    
    public int Edad {get; set;}

    
    //Valida el formato del numero de la tarjeta
    [CreditCard]
    //Crea propiedades temporales
    [NotMapped]
    public string TarjetaDeCredito{get; set;}

    [Url]
    [NotMapped]
    public string URL {get; set;}

    public List<Libro> Libros {get; set;}

}
