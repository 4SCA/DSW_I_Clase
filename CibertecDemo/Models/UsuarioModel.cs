using System.ComponentModel.DataAnnotations;

namespace CibertecDemo.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Los nombres son obligatorios")] //este campo es obligatorio cuando se agrega el decorador required
        public string Nombres {get; set;}

        [Required(ErrorMessage ="Los apellidos son obligatorios")]
        public string Apellidos { get; set;}

        [Required(ErrorMessage="El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage ="Correo electrónico inválido")]
        public string Correo { get; set;}

        [Required(ErrorMessage ="La contraseña es obligatoria")]
        [DataType(DataType.Password)] //Especifica el tipo de dato que se va a enviar en este caso contraseña o password en el input
        [Display(Name ="Contraseña")]
        public string Clave { get; set;}

        [StringLength(9, MinimumLength =9, ErrorMessage ="El télefono debe contener 9 carácteres")]
        [Display(Name ="Teléfono/Celular")]
        public string Telefono { get; set;}

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set;}
        
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
