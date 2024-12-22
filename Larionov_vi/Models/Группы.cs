using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Группы
    {
        public Группы()
        {
            Студентыs = new HashSet<Студенты>();
        }

        public int КодГруппы { get; set; }
        public string НазваниеГруппы { get; set; } = null!;
        public string Специализация { get; set; } = null!;
        public int ГодКурса { get; set; }

        public virtual ICollection<Студенты> Студентыs { get; set; }
    }
}
