namespace pampasoft6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class localidades
    {
        [Key]
        public int id { get; set; }

        [StringLength(30)]
        [Required (ErrorMessage = "El {0} es obligatorio")]
        [Display (Name="Nombre")]
        public string N_LOCAL { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [Display(Name = "Código Postal")]
        public int CP { get; set; }

        [StringLength(10)]
        [Display(Name = "Telediscado")]
        public string C_DISCADO { get; set; }

        /// <summary>
        /// [Key]
        /// [Column(Order = 2)]
        /// [DatabaseGenerated(DatabaseGeneratedOption.None)]
        /// public short R_PROVIN { get; set; }
        /// </summary>

    }
}
