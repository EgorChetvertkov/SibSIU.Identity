namespace SibSIU.Dean.Database.Entities
{
    public class УровеньОбразования
    {
        public int КодЗаписи { get; set; }
        public string? Уровень { get; set; }
        public string? Категория { get; set; }
        public bool? Вывод { get; set; }
        public string? Префикс { get; set; }
        public string? Примечание { get; set; }
        public string? Шифр { get; set; }
        public string? ВидПлана { get; set; }
        public string? ВоенкоматНаименование { get; set; }
        public string? ВоенкоматКод { get; set; }
    }
}
