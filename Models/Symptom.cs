using System;
using System.Collections.Generic;

namespace NewVirusApp
{
    public partial class Symptom
    {
        public Symptom()
        {
            SymptomsVariants = new HashSet<SymptomsVariant>();
        }

        public int Id { get; set; }
        public string? SymptomName { get; set; }

        public virtual ICollection<SymptomsVariant> SymptomsVariants { get; set; }
    }
}
