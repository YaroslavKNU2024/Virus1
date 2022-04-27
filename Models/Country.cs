using System;
using System.Collections.Generic;

namespace NewVirusApp
{
    public partial class Country
    {
        public Country()
        {
            CountriesVariants = new HashSet<CountriesVariant>();
        }

        public int Id { get; set; }
        public string? CountryName { get; set; }

        public virtual ICollection<CountriesVariant> CountriesVariants { get; set; }
    }
}
