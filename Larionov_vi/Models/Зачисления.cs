using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Зачисления
    {
        public int КодЗачисления { get; set; }
        public int КодСтудента { get; set; }
        public int КодКурса { get; set; }
        public DateTime ДатаЗачисления { get; set; }

        public virtual Курсы КодКурсаNavigation { get; set; } = null!;
        public virtual Студенты КодСтудентаNavigation { get; set; } = null!;
    }
}
