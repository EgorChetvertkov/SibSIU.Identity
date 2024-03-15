// Ignore Spelling: Втаблица Ауд Кт Незаполнена Незаполнен Ved Kaf Диффенцированный

namespace SibSIU.Dean.Database.Entities
{
    public class ВсеВедомости
    {
        public int Код { get; set; }
        public string? Название { get; set; }
        public string? ИмяПлана { get; set; }
        public int? ТипВедомости { get; set; }
        public string? Год { get; set; }
        public int? Семестр { get; set; }
        public int? Курс { get; set; }
        public int КодГруппы { get; set; }
        public int КодФакультета { get; set; }
        public string? Преподаватель { get; set; }
        public string? Цикл { get; set; }
        public int? КолВоКт { get; set; }
        public DateTime? ДатаЭкзамена { get; set; }
        public string? Дисциплина { get; set; }
        public int? Часов { get; set; }
        public string? Специальность { get; set; }
        public int КодКафедры { get; set; }
        public string? Пароль { get; set; }
        public bool? Закрыта { get; set; }
        public DateTime? ДатаЗакрытия { get; set; }
        public string? Приложение { get; set; }
        public int? ВерсияVedKaf { get; set; }
        public string? ДатаВерсииVedKaf { get; set; }
        public string? ПоследнийПользователь { get; set; }
        public string? ПоследнийКомпьютер { get; set; }
        public DateTime? ДатаПоследнегоСохранения { get; set; }
        public int? РейтингНа2 { get; set; }
        public int? РейтингНа3 { get; set; }
        public int? РейтингНа4 { get; set; }
        public int? РейтингНа5 { get; set; }
        public bool? ЗачетПрактика { get; set; }
        public bool? ДиффенцированныйЗачет { get; set; }
        public string? ПредседательКомиссии { get; set; }
        public DateTime? ДатаНачалаСессии { get; set; }
        public bool? СтатНезакрыта { get; set; }
        public bool? СтатНезаполненРейтинг { get; set; }
        public bool? СтатАктивна { get; set; }
        public bool? СтатПустая { get; set; }
        public bool? СтатНезаполненаКт { get; set; }
        public bool? Выборная { get; set; }
        public short? Сессия { get; set; }
        public byte? КонтрольныеРаботы { get; set; }
        public int? КодТеста { get; set; }
        public byte? ВсегоСтудентов { get; set; }
        public byte? Оценок0 { get; set; }
        public byte? Оценок1 { get; set; }
        public byte? Оценок2 { get; set; }
        public byte? Оценок3 { get; set; }
        public byte? Оценок4 { get; set; }
        public byte? Оценок5 { get; set; }
        public bool? СданаОсновная { get; set; }
        public bool? СданаПересдачи { get; set; }
        public DateTime? ДатаСдачи { get; set; }
        public float? Зет { get; set; }
        public int? КодПланыСтроки { get; set; }
        public string? МестоПрактики { get; set; }
        public float? ЧасовАуд { get; set; }
        public float? ЧасовКонтакт { get; set; }
        public int? КодСтрокиНагрузки { get; set; }
        public string? ТаблицаДисциплины { get; set; }
        public int? КодВтаблицаДисциплины { get; set; }
        public int? НомерКомиссии { get; set; }
    }
}
