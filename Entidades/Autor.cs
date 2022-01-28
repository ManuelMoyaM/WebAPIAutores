using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.Entidades;

//Validacion por modelo
public class Autor: IValidatableObject //Para poder hacer las validaciones por modelo(Es decir, dentro de la clase Autor)
{
    public int Id {get; set;}
    //Obliga a que el campo sea requerido. En este caso el nombre 
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [StringLength(maximumLength:5, ErrorMessage ="El campo {0} no debe tener mas de {1} caracteres")]
    ///[PrimeraLetraMayuscula] Se desactiva esta validacion por atributo para poder probar la validacion por modelo
    public string Nombre {get; set;}
  /*   [Range(18, 120)]
    
    public int Edad {get; set;}

    
    //Valida el formato del numero de la tarjeta
    [CreditCard]
    //Crea propiedades temporales
    [NotMapped]
    public string TarjetaDeCredito{get; set;}

    [Url]
    [NotMapped]
    public string URL {get; set;} */

    public int Menor { get; set; }
    public int Mayor { get; set; }

    public List<Libro> Libros {get; set;}

    //Validacion por modelo

    //IEnumerable, Devuelve una colecion de resultados de validacion
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(!string.IsNullOrEmpty(Nombre))
        {
            var primeraLetra = Nombre[0].ToString();

            if (primeraLetra != primeraLetra.ToUpper())
            {

                yield return new ValidationResult("La primera letra debe ser mayuscula",
                                                    new string[] {nameof(Nombre)});
            }
        }

        if (Menor > Mayor)
        {
            yield return new ValidationResult("Este valor no puede ser mayor al campo Mayor",
                                                new string[] {nameof(Menor)});
            
        }





    }
}
