using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Роли
    {
        public Роли()
        {
            Пользователиs = new HashSet<Пользователи>();
        }

        public int КодРоли { get; set; }
        public string НазваниеРоли { get; set; } = null!;

        public virtual ICollection<Пользователи> Пользователиs { get; set; }
    }
}
