using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class События
    {
        public События()
        {
            УчастникиСобытийs = new HashSet<УчастникиСобытий>();
        }

        public int КодСобытия { get; set; }
        public string НазваниеСобытия { get; set; } = null!;
        public DateTime ДатаСобытия { get; set; }
        public string МестоПроведения { get; set; } = null!;
        public string? Описание { get; set; }

        public virtual ICollection<УчастникиСобытий> УчастникиСобытийs { get; set; }
    }
}
