using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsProject.BussinessEntities
{
    public class AdImage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Ad")]
        [Required(ErrorMessage = "La referencia del anuncio es requerida")]
        [Display(Name = "Anuncio")]
        public int IdAd { get; set; }

        [Required(ErrorMessage = "La ruta de la imagen es requerida")]
        [MaxLength(4000, ErrorMessage = "Máximo 4000 caracteres")]
        [Display(Name = "Imagen")]
        public string Path { get; set; } = string.Empty;

        [NotMapped]
        public int Top_Aux { get; set; } // propiedad auxiliar
        public Ad Ad { get; set; } = new Ad(); // propiedad de navegación

    }
}
