using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Курсы
    {
        public Курсы()
        {
            Заданияs = new HashSet<Задания>();
            Занятияs = new HashSet<Занятия>();
            Зачисленияs = new HashSet<Зачисления>();
            ПрограммыКурсовs = new HashSet<ПрограммыКурсов>();
        }

        public int КодКурса { get; set; }
        public string НазваниеКурса { get; set; } = null!;
        public string? Описание { get; set; }
        public int Кредиты { get; set; }
        public int КодПреподавателя { get; set; }

        public virtual Пользователи КодПреподавателяNavigation { get; set; } = null!;
        public virtual ICollection<Задания> Заданияs { get; set; }
        public virtual ICollection<Занятия> Занятияs { get; set; }
        public virtual ICollection<Зачисления> Зачисленияs { get; set; }
        public virtual ICollection<ПрограммыКурсов> ПрограммыКурсовs { get; set; }
    }
}
