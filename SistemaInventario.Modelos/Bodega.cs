using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Bodega
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre de la bodega es requerido.")]
        [MaxLength(60,ErrorMessage ="El nombre de la bodega no puede superar los 60 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion de la bodega es requerida.")]
        [MaxLength(100, ErrorMessage = "La descripcion de la bodega no puede superar los 100 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado de la bodega es requerida.")]
        public bool Estado { get; set; }
    }
}
