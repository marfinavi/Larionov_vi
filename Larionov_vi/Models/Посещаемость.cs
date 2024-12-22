using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Посещаемость
    {
        public int КодПосещаемости { get; set; }
        public int КодЗанятия { get; set; }
        public int КодСтудента { get; set; }
        public string СтатусПосещаемости { get; set; } = null!;

        public virtual Занятия КодЗанятияNavigation { get; set; } = null!;
        public virtual Студенты КодСтудентаNavigation { get; set; } = null!;
    }
}
