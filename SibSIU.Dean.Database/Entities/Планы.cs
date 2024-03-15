// Ignore Spelling: Внагрузке Фио Фгос Утвер Сцос Студ Строкиs Спо Созд Рпд Препод Ооп Обуч Стреками Нп Ликвид Коэф Конт Каф Иуп Имца Изм Икэкурс Заоч Внеделе Вкредите Зетв Дв Гв Госа Вчасах Врпд Вариатив Академ

namespace SibSIU.Dean.Database.Entities
{
    public class Планы
    {
        public Планы()
        {
            ПланыСтрокиs = new HashSet<ПланыСтроки>();
        }

        public int Код { get; set; }
        public string ИмяФайла { get; set; } = null!;
        public string УчебныйГод { get; set; } = null!;
        public string? Специальность { get; set; }
        public int КодТипаПлана { get; set; }
        public string? Титул { get; set; }
        public string? Факультет { get; set; }
        public int? КодФакультета { get; set; }
        public int? КодСпециальности { get; set; }
        public int? Поколение { get; set; }
        public bool? АвтоПроверка { get; set; }
        public DateTime? ДатаУтверРектором { get; set; }
        public DateTime? ДатаУтверСоветом { get; set; }
        public string? НомПротокСовета { get; set; }
        public int? КодПрофКафедры { get; set; }
        public string? ИмяВуза { get; set; }
        public string? ИмяПодразделения { get; set; }
        public DateTime? ДатаИзмФайла { get; set; }
        public int? КодВуза { get; set; }
        public DateTime? ДатаАттестации { get; set; }
        public DateTime? ДатаВерсииПриложения { get; set; }
        public int? НомерВерсииПриложения { get; set; }
        public string? ИмяПриложения { get; set; }
        public int? НомерПользователя { get; set; }
        public string? ИмяПользователя { get; set; }
        public string? Утверждение { get; set; }
        public bool? АвтоРасчетНачалаСеместров { get; set; }
        public DateTime? ДатаГоса { get; set; }
        public int? ГодНачалаПодготовки { get; set; }
        public string? ИмяФайлаГоса { get; set; }
        public DateTime? ДатаСертификатаИмца { get; set; }
        public string? ШифрСертификата { get; set; }
        public int? КонтрольнаяСуммаСертификата { get; set; }
        public int? КредитовНаГод { get; set; }
        public float? КредитовНаВесьСрок { get; set; }
        public float? ЧасовВкредите { get; set; }
        public float? КредитовНаКонтроль { get; set; }
        public float? КредитовВнеделе { get; set; }
        public string? ГоловнаяОрганизация { get; set; }
        public int? Икэкурс { get; set; }
        public int? Идентификатор { get; set; }
        public int? ЗаочНедельНаДиплом { get; set; }
        public int? ЗаочНедельНаГэк { get; set; }
        public string? Резерв { get; set; }
        public DateTime? ДатаИзмененияФайла { get; set; }
        public string? НазваниеКредитов { get; set; }
        public short? ДнейНаСессию1 { get; set; }
        public short? ДнейНаСессию2 { get; set; }
        public short? ДнейНаСессию3 { get; set; }
        public string? ПолноеИмяФайла { get; set; }
        public int? КодФормыОбучения { get; set; }
        public short? СеместровНаКурсе { get; set; }
        public short? ЭлементовВнеделе { get; set; }
        public bool? Фгос { get; set; }
        public float? Трудоемкость { get; set; }
        public float? МаксУчНагрузка { get; set; }
        public float? ДоляЛекций { get; set; }
        public float? ДоляИнтерактив { get; set; }
        public float? ДоляВариатив { get; set; }
        public int? КодУровняОбразования { get; set; }
        public int? КодПодразделения { get; set; }
        public bool? Дистанционное { get; set; }
        public bool? Сокращённое { get; set; }
        public int? СтатусВнагрузке { get; set; }
        public DateTime? ИзмененияВнесены { get; set; }
        public decimal? НагрузкаКоэфСтудНаПрепод { get; set; }
        public bool? Корень { get; set; }
        public int? КодСтатуса { get; set; }
        public DateTime? ДатаПроверки { get; set; }
        public string? КурсыОбучения { get; set; }
        public bool? Военный { get; set; }
        public string? ШифрСпециальности { get; set; }
        public float? НедельОбучения { get; set; }
        public float? НедельНаПолевыеЗанятия { get; set; }
        public int? СтупеньКвалификации { get; set; }
        public int? СрокОбучения { get; set; }
        public int? СрокОбученияМесяцев { get; set; }
        public int? ОбъемУчНагрузки { get; set; }
        public string? УровеньПодготовкиСпо { get; set; }
        public string? БазаОбучения { get; set; }
        public int? ТипБазыОбучения { get; set; }
        public int? КодПрофиля { get; set; }
        public bool? НагрузкаВчасахНпо { get; set; }
        public bool? ВключатьЧасыНаЭкзамены { get; set; }
        public string? БазовоеОбразование { get; set; }
        public byte? ЧислоСеместровСессий { get; set; }
        public int? СтатусРпд { get; set; }
        public double? ТипГоса { get; set; }
        public bool? УчПрПлюсТо { get; set; }
        public int? КодБазПлана { get; set; }
        public int? КодОоп { get; set; }
        public int? КодАктивногоОоп { get; set; }
        public int? КодПрограммы { get; set; }
        public int? КодБазы { get; set; }
        public int? КодОрганизации { get; set; }
        public int? КодСтрЕдиницы { get; set; }
        public bool? Индивидуальный { get; set; }
        public string? Фио { get; set; }
        public bool? Военные { get; set; }
        public bool? ДляИностранных { get; set; }
        public string? Квалификация { get; set; }
        public string? Специализация { get; set; }
        public string? ВоеннаяСпециальность { get; set; }
        public string? Предназначение { get; set; }
        public int? ЧислоКурсов { get; set; }
        public int? ЧислоСессий { get; set; }
        public int? КодГрафика { get; set; }
        public string? НомерФгос { get; set; }
        public string? Примечание { get; set; }
        public double? ЗетвНеделю { get; set; }
        public double? Точность { get; set; }
        public bool? ДвИга { get; set; }
        public bool? ГвИга { get; set; }
        public string? ИмяКомпьютера { get; set; }
        public int? ТипПереаттестации { get; set; }
        public int? КодКафРукНп { get; set; }
        public string? ИзмененияВнёс { get; set; }
        public double? ИзученоКонтЧасов { get; set; }
        public bool? СкрытьВрпд { get; set; }
        public bool? СкрытьНаСайте { get; set; }
        public int? MinAppVersion { get; set; }
        public string? ПланОдобрен { get; set; }
        public bool? Стреками { get; set; }
        public int? СрокОбученияДней { get; set; }
        public byte[]? Версия { get; set; }
        public string? ФамилияСтуд { get; set; }
        public string? ИмяСтуд { get; set; }
        public string? ОтчествоСтуд { get; set; }
        public string? НомерПротокКомиссии { get; set; }
        public DateTime? ДатаПротокКомиссии { get; set; }
        public DateTime? ДатаЛиквидАкадемРазницы { get; set; }
        public int? ОснованиеИуп { get; set; }
        public int? КурсНачалаОбуч { get; set; }
        public int? СеместрНачалаОбуч { get; set; }
        public int? ПолСтудента { get; set; }
        public DateTime? ДатаПриказаСоздКомиссии { get; set; }
        public string? НомерПриказаСоздКомиссии { get; set; }
        public int? СрокОбученияНедель { get; set; }

        public virtual Специальности? КодСпециальностиNavigation { get; set; }
        public virtual Факультеты? КодФакультетаNavigation { get; set; }
        public virtual ICollection<ПланыСтроки> ПланыСтрокиs { get; set; }
    }
}
