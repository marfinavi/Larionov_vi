using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Пользователи
    {
        public Пользователи()
        {
            Курсыs = new HashSet<Курсы>();
            Студентыs = new HashSet<Студенты>();
            УчастникиСобытийs = new HashSet<УчастникиСобытий>();
        }

        public int КодПользователя { get; set; }
        public string Имя { get; set; } = null!;
        public string Фамилия { get; set; } = null!;
        public string ЭлектроннаяПочта { get; set; } = null!;
        public string ХэшПароля { get; set; } = null!;
        public int КодРоли { get; set; }

        public virtual Роли КодРолиNavigation { get; set; } = null!;
        public virtual ICollection<Курсы> Курсыs { get; set; }
        public virtual ICollection<Студенты> Студентыs { get; set; }
        public virtual ICollection<УчастникиСобытий> УчастникиСобытийs { get; set; }
    }
}
