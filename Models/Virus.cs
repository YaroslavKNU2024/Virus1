using System;
using System.Collections.Generic;

namespace NewVirusApp
{
    public partial class Virus
    {
        public Virus()
        {
            Variants = new HashSet<Variant>();
        }

        public int Id { get; set; }
        public string? VirusName { get; set; }
        public DateTime? VirusDateDiscovered { get; set; }
        public int? GroupId { get; set; }

        public virtual VirusGroup? Group { get; set; }
        public virtual ICollection<Variant> Variants { get; set; }
    }
}
