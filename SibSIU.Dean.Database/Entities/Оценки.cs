// Ignore Spelling: Ects

namespace SibSIU.Dean.Database.Entities
{
    public class Оценки
    {
        public int Код { get; set; }
        public int? КодСтудента { get; set; }
        public int? КодВедомости { get; set; }
        public short? ПроцентВыполнения { get; set; }
        public short? ИтоговыйПроцент { get; set; }
        public short? ОценкаПоРейтингу { get; set; }
        public short? ОценкаНаЭкзаменеЗачете { get; set; }
        public short? ИтоговаяОценка { get; set; }
        public short? Надбавка { get; set; }
        public DateTime? ДатаСдачи { get; set; }
        public short? Пересдача1 { get; set; }
        public DateTime? ДатаПересдачи1 { get; set; }
        public short? Пересдача2 { get; set; }
        public DateTime? ДатаПересдачи2 { get; set; }
        public short? Пересдача3 { get; set; }
        public DateTime? ДатаПересдачи3 { get; set; }
        public short? СтрокаВВедомости { get; set; }
        public short? НомерВВедомости { get; set; }
        public int? КонтрольнаяСумма { get; set; }
        public string? ТемаКурсовой { get; set; }
        public short? Итог { get; set; }
        public string? Протокол { get; set; }
        public short? Ects { get; set; }
        public short? ИтоговыйРейтинг { get; set; }
        public string? Протокол2 { get; set; }
        public string? Протокол3 { get; set; }
        public DateTime? ДатаИзменения { get; set; }
        public int? КодПользователя { get; set; }
        public bool? Скрыта { get; set; }
        public byte[]? Версия { get; set; }
    }
}
