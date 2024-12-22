using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Задания
    {
        public Задания()
        {
            Работыs = new HashSet<Работы>();
        }

        public int КодЗадания { get; set; }
        public int КодКурса { get; set; }
        public string Название { get; set; } = null!;
        public string? Описание { get; set; }
        public DateTime ДатаСдачи { get; set; }

        public virtual Курсы КодКурсаNavigation { get; set; } = null!;
        public virtual ICollection<Работы> Работыs { get; set; }
    }
}
