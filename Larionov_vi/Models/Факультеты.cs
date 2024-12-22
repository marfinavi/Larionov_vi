using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Факультеты
    {
        public Факультеты()
        {
            Программыs = new HashSet<Программы>();
        }

        public int КодФакультета { get; set; }
        public string НазваниеФакультета { get; set; } = null!;
        public string Декан { get; set; } = null!;

        public virtual ICollection<Программы> Программыs { get; set; }
    }
}
