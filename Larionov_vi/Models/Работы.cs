using System;
using System.Collections.Generic;

namespace Larionov_vi.Models
{
    public partial class Работы
    {
        public int КодРаботы { get; set; }
        public int КодЗадания { get; set; }
        public int КодСтудента { get; set; }
        public DateTime ДатаСдачи { get; set; }
        public string? Оценка { get; set; }
        public string? Комментарии { get; set; }

        public virtual Задания КодЗаданияNavigation { get; set; } = null!;
        public virtual Студенты КодСтудентаNavigation { get; set; } = null!;
    }
}
