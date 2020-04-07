namespace pampasoft6
{
    using Pampasoft6.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class localidades: TablaBase
    {

        [StringLength(100,ErrorMessage = "El {0} debe tener una longitud m�xima de {1}")]
        [Required (ErrorMessage = "El {0} es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "C�digo Postal")]
        public int CodigoPostal { get; set; }

        [StringLength(10)]
        [Display(Name = "Telediscado")]
        public string Discado { get; set; }

        /// <summary>
        /// [Key]
        /// [Column(Order = 2)]
        /// [DatabaseGenerated(DatabaseGeneratedOption.None)]
        /// public short R_PROVIN { get; set; }
        /// </summary>

    }
}
