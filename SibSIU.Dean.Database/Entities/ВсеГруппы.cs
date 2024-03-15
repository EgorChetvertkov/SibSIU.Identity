// Ignore Spelling: Ооп Ин

namespace SibSIU.Dean.Database.Entities
{
    public class ВсеГруппы
    {
        public int Код { get; set; }
        public string Название { get; set; } = null!;
        public string? УчебныйПлан { get; set; }
        public int? КодСпециальности { get; set; }
        public int? Курс { get; set; }
        public int? КодФакультета { get; set; }
        public string? IdГруппы { get; set; }
        public string? УчебныйГод { get; set; }
        public string? Направление { get; set; }
        public string? Специальность { get; set; }
        public string? Специализации { get; set; }
        public int? КодПодразделения { get; set; }
        public bool? ДистанционноеОбучение { get; set; }
        public bool? СокращеннаяФорма { get; set; }
        public bool? Удалена { get; set; }
        public string? СрокОбучения { get; set; }
        public int? ПланНабора { get; set; }
        public int? ПланНабораКом { get; set; }
        public int? ПланНабораИностранцы { get; set; }
        public int? КодОбучения { get; set; }
        public int НомерГруппы { get; set; }
        public int? Уровень { get; set; }
        public int? ФормаОбучения { get; set; }
        public bool? Скрыть { get; set; }
        public int? КодПлана { get; set; }
        public int? КодПрофиль { get; set; }
        public string? Секретарь { get; set; }
        public string? ПредседательГак { get; set; }
        public int? ПланНабораИнДог { get; set; }
        public int? КодОоп { get; set; }
        public int? КодКуратора { get; set; }
        public int? ПланНабораБ { get; set; }
        public int? КодТипаВременнойГруппы { get; set; }
        public int? ШколаХКодПроверяющего { get; set; }
        public int? ШколаХКодПапки { get; set; }
        public bool? ШколаХНеУчебная { get; set; }
        public string? Примечание { get; set; }
        public string? Псевдоним { get; set; }
    }
}
