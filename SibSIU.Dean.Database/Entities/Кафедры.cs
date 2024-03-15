// Ignore Spelling: Эфф Мпгу Цмк

namespace SibSIU.Dean.Database.Entities
{
    public class Кафедры
    {
        public int Код { get; set; }
        public string? Название { get; set; }
        public string? Сокращение { get; set; }
        public int Номер { get; set; }
        public int? КодФакультета { get; set; }
        public string? ЗавКафедрой { get; set; }
        public string? Аудитория { get; set; }
        public string? Телефон { get; set; }
        public string? ВнутрТелефон { get; set; }
        public string? Email { get; set; }
        public string? Url { get; set; }
        public bool? Удалена { get; set; }
        public string? ПрефиксКафедры { get; set; }
        public bool? Цмк { get; set; }
        public string? Примечание { get; set; }
        public bool? Выпускающая { get; set; }
        public double? КоэффициентМпгу { get; set; }
        public bool? ИгнорироватьЭффКонтракт { get; set; }
    }
}
