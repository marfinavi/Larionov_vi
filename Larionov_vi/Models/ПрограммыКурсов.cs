using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class ПрограммыКурсов
    {
        public int КодПрограммыКурса { get; set; }
        public int КодКурса { get; set; }
        public int КодПрограммы { get; set; }

        public virtual Курсы КодКурсаNavigation { get; set; } = null!;
        public virtual Программы КодПрограммыNavigation { get; set; } = null!;
    }
}
