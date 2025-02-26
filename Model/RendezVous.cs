using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGroupe2.Model
{
    public class RendezVous
    {
        [Key]
        public int IdRv { get; set; }
        [Required]
        public DateTime DateRv { get; set; }
        [MaxLength(10)]
        public string Statut { get; set; }
        public int? IdSoin {  get; set; }
        [ForeignKey("IdSoin")]
        public virtual Soin Soin { get; set; }

        public int? IdPatient { get; set; }
        [ForeignKey(" IdPatient")]
        public virtual Patient patient { get; set; }

        public int? IdMedecin { get; set; }
        [ForeignKey("IdMedecin")]
        public virtual Medecin Medecin { get; set; }

    }
}
