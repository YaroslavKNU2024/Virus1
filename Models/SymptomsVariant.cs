using System;
using System.Collections.Generic;

namespace NewVirusApp
{
    public partial class SymptomsVariant
    {
        public int VariantId { get; set; }
        public int SymptomId { get; set; }
        public int? Cases { get; set; }

        public virtual Symptom Symptom { get; set; } = null!;
        public virtual Variant Variant { get; set; } = null!;
    }
}
