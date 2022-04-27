using System;
using System.Collections.Generic;

namespace NewVirusApp
{
    public partial class VirusGroup
    {
        public VirusGroup()
        {
            Viruses = new HashSet<Virus>();
        }

        public int Id { get; set; }
        public string? GroupName { get; set; }
        public string? GroupInfo { get; set; }
        public DateTime? DateDiscovered { get; set; }

        public virtual ICollection<Virus> Viruses { get; set; }
    }
}
