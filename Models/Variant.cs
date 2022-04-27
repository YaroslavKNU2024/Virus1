using System;
using System.Collections.Generic;

namespace NewVirusApp
{
    public partial class Variant
    {
        public Variant()
        {
            CountriesVariants = new HashSet<CountriesVariant>();
            SymptomsVariants = new HashSet<SymptomsVariant>();
        }

        public int Id { get; set; }
        public string? VariantName { get; set; }
        public string? VariantOrigin { get; set; }
        public DateTime? VariantDateDiscovered { get; set; }
        public int? VirusId { get; set; }

        public virtual Virus? Virus { get; set; }
        public virtual ICollection<CountriesVariant> CountriesVariants { get; set; }
        public virtual ICollection<SymptomsVariant> SymptomsVariants { get; set; }
    }
}
