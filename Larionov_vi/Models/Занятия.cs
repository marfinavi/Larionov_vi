using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Занятия
    {
        public Занятия()
        {
            Посещаемостьs = new HashSet<Посещаемость>();
        }

        public int КодЗанятия { get; set; }
        public int КодКурса { get; set; }
        public DateTime ДатаЗанятия { get; set; }
        public TimeSpan ВремяНачала { get; set; }
        public TimeSpan ВремяОкончания { get; set; }
        public string Аудитория { get; set; } = null!;

        public virtual Курсы КодКурсаNavigation { get; set; } = null!;
        public virtual ICollection<Посещаемость> Посещаемостьs { get; set; }
    }
}
