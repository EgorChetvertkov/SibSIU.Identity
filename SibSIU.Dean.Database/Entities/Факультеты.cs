// Ignore Spelling: Сцос Планыs Группыs Иофр Иоф Зф Вобр


// Ignore Spelling: Сцос Планыs Группыs Иофр Иоф Зф Вобр

namespace SibSIU.Dean.Database.Entities
{
    public class Факультеты
    {
        public Факультеты()
        {
            ВсеГруппыs = new HashSet<ВсеГруппы>();
            Планыs = new HashSet<Планы>();
        }

        public int Код { get; set; }
        public string? Факультет { get; set; }
        public string? Сокращение { get; set; }
        public string? Псевдоним { get; set; }
        public int? ТипОбучения { get; set; }
        public bool? Пк { get; set; }
        public string? Декан { get; set; }
        public string? Телефон { get; set; }
        public string? Аудитория { get; set; }
        public bool? Сайт { get; set; }
        public string? ВнутрТелефон { get; set; }
        public string? Email { get; set; }
        public string? Url { get; set; }
        public string? ФайлСтиля { get; set; }
        public string? Шифр { get; set; }
        public string? Описание { get; set; }
        public int? Номер { get; set; }
        public int? КодФилиала { get; set; }
        public string? РодПадеж { get; set; }
        public string? Секретарь { get; set; }
        public string? ЗамДекана { get; set; }
        public bool? ЕстьДо { get; set; }
        public string? Иоф { get; set; }
        public string? Иофр { get; set; }
        public string? Тип { get; set; }
        public string? Подпись1 { get; set; }
        public string? Подпись2 { get; set; }
        public string? Подпись3 { get; set; }
        public string? Подпись4 { get; set; }
        public string? ПодписьЗф1 { get; set; }
        public string? ПодписьЗф2 { get; set; }
        public string? ПодписьЗф3 { get; set; }
        public string? ПодписьЗф4 { get; set; }
        public int? СправкиМинНомер { get; set; }
        public bool? СправкиПриёмСправок { get; set; }
        public int? МаксСправокВобр { get; set; }
        public int? МаксСправокСтудентаВобр { get; set; }
        public string? СправкиИндекс { get; set; }
        public int? КодПреподавателяДекана { get; set; }
        public string? ПодписьВедомости { get; set; }
        public bool? СкрытьСцос { get; set; }
        public bool? ЖурналыПриватныйРежим { get; set; }
        public int? КодЭлитногоОбразования { get; set; }

        public virtual ICollection<ВсеГруппы> ВсеГруппыs { get; set; }
        public virtual ICollection<Планы> Планыs { get; set; }
    }
}
