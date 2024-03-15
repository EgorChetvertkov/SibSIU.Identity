namespace SibSIU.Dean.Database.Entities
{
    public class ПланыНовыеЧасы
    {
        public int Код { get; set; }
        public int? КодОбъекта { get; set; }
        public int? КодВидаРаботы { get; set; }
        public int? КодТипаЧасов { get; set; }
        public short? Курс { get; set; }
        public byte? Семестр { get; set; }
        public byte? Сессия { get; set; }
        public double? Количество { get; set; }
        public int? Недель { get; set; }
        public int? Дней { get; set; }
        public bool? Переаттестовано { get; set; }
        public int? ТипКомиссии { get; set; }
    }
}
