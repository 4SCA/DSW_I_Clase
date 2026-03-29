using System.ComponentModel.DataAnnotations;

namespace CibertecDemo.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Ingrese un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Ingrese un valor a precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage ="Ingrese cantidad")]
        public int Cantidad { get; set; }

        public bool Estado { get; set; }
    }
}
