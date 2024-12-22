using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Студенты
    {
        public Студенты()
        {
            Зачисленияs = new HashSet<Зачисления>();
            Посещаемостьs = new HashSet<Посещаемость>();
            Работыs = new HashSet<Работы>();
        }

        public int КодСтудента { get; set; }
        public int КодПользователя { get; set; }
        public int ГодПоступления { get; set; }
        public int КодГруппы { get; set; }

        public virtual Группы КодГруппыNavigation { get; set; } = null!;
        public virtual Пользователи КодПользователяNavigation { get; set; } = null!;
        public virtual ICollection<Зачисления> Зачисленияs { get; set; }
        public virtual ICollection<Посещаемость> Посещаемостьs { get; set; }
        public virtual ICollection<Работы> Работыs { get; set; }
    }
}
