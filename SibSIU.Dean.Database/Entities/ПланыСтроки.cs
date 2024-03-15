// Ignore Spelling: Вблоке Госу Перезачтено Взет Спо Ооп Рассред Вплане Зетфакт Зетизучено Нестандарт Multiselect Перезачета Ауд Врпд Dvnot Kont Nagr Pract


// Ignore Spelling: Вблоке Госу Перезачтено Взет Спо Ооп Рассред Вплане Зетфакт Зетизучено Нестандарт Multiselect Перезачета Ауд Врпд Dvnot Kont Nagr Pract

namespace SibSIU.Dean.Database.Entities
{
    public class ПланыСтроки
    {
        public int Код { get; set; }
        public int КодПлана { get; set; }
        public short? ТипСтроки { get; set; }
        public string? Дисциплина { get; set; }
        public int? НомерСтроки { get; set; }
        public short? НомерБлока { get; set; }
        public short? НомерСтрокиВблоке { get; set; }
        public short? ЧасовПоГосу { get; set; }
        public short? ЧасовСамостоятельнаяРабота { get; set; }
        public int? КодКафедры { get; set; }
        public double? КоэффициентУникальности { get; set; }
        public short? ПризнакНазванияРаздела { get; set; }
        public short? ПризнакРаздела { get; set; }
        public short? ПерезачтеноКонтрольных { get; set; }
        public double? ПерезачтеноЧасов { get; set; }
        public double? ПодлежитИзучениюЧасов { get; set; }
        public float? ТрудоемкостьКредитов { get; set; }
        public string? ЦиклКод { get; set; }
        public string? ДисциплинаКод { get; set; }
        public int? КонтрольнаяСумма { get; set; }
        public string? Резерв { get; set; }
        public int? КодДисциплины { get; set; }
        public bool? Выборная { get; set; }
        public bool? ДополнительнаяВыборная { get; set; }
        public bool? Факультатив { get; set; }
        public string? Цикл { get; set; }
        public string? Компонент { get; set; }
        public string? Компетенции { get; set; }
        public short? ЧасовВзет { get; set; }
        public bool? ПолевыеЗанятия { get; set; }
        public bool? НеСчитатьЗачеты { get; set; }
        public bool? НеСчитатьКонтроль { get; set; }
        public int? МаксУчНагрузкаСпо { get; set; }
        public bool? Удалена { get; set; }
        public int? КодОоп { get; set; }
        public int? КодБлока { get; set; }
        public int? КодРодителя { get; set; }
        public int? ТипОбъекта { get; set; }
        public int? ВидОбъекта { get; set; }
        public int? ВидПрактики { get; set; }
        public bool? РассредПрактика { get; set; }
        public int? УровеньВложения { get; set; }
        public bool? ПризнакФизкультуры { get; set; }
        public bool? СчитатьБезЗет { get; set; }
        public bool? СчитатьВплане { get; set; }
        public bool? ЗаСчетПолевых { get; set; }
        public double? Зетфакт { get; set; }
        public double? Зетизучено { get; set; }
        public double? ЧасовПоЗет { get; set; }
        public double? ЧасовПоПлану { get; set; }
        public double? ЧасовИзучено { get; set; }
        public bool? НестандартНедПрактики { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? Свернуть { get; set; }
        public int? Multiselect { get; set; }
        public int? Оценка { get; set; }
        public int? ТипПерезачета { get; set; }
        public bool? Адаптационная { get; set; }
        public double? ЧасыВарМакс { get; set; }
        public double? ЧасыВарАуд { get; set; }
        public int? Номер { get; set; }
        public int? Порядок { get; set; }
        public bool? СкрытьВрпд { get; set; }
        public bool? DvnotEquals { get; set; }
        public int? Уровень { get; set; }
        public double? ПерезачтеноЧасовАуд { get; set; }
        public bool? NotCalcInSumKont { get; set; }
        public string? ЦелиОсвоения { get; set; }
        public bool? NotCalcInNagr { get; set; }
        public byte[]? Версия { get; set; }
        public bool? HideInTabPract { get; set; }
        public bool? Дистанционная { get; set; }

        public virtual Планы КодПланаNavigation { get; set; } = null!;
    }
}
