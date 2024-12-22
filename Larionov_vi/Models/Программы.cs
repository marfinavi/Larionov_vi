using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Программы
    {
        public Программы()
        {
            ПрограммыКурсовs = new HashSet<ПрограммыКурсов>();
        }

        public int КодПрограммы { get; set; }
        public string НазваниеПрограммы { get; set; } = null!;
        public int КодФакультета { get; set; }
        public int Продолжительность { get; set; }

        public virtual Факультеты КодФакультетаNavigation { get; set; } = null!;
        public virtual ICollection<ПрограммыКурсов> ПрограммыКурсовs { get; set; }
    }
}
