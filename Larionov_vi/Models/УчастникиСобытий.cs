using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class УчастникиСобытий
    {
        public int КодУчастника { get; set; }
        public int КодПользователя { get; set; }
        public int КодСобытия { get; set; }

        public virtual Пользователи КодПользователяNavigation { get; set; } = null!;
        public virtual События КодСобытияNavigation { get; set; } = null!;
    }
}
