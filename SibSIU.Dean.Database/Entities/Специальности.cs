// Ignore Spelling: Планыs Оксо Ооп Delite


// Ignore Spelling: Планыs Оксо Ооп Delite

namespace SibSIU.Dean.Database.Entities
{
    public class Специальности
    {
        public Специальности()
        {
            Планыs = new HashSet<Планы>();
        }

        public int Код { get; set; }
        public string? Специальность { get; set; }
        public string? СтараяСпециальность { get; set; }
        public string? Оксо { get; set; }
        public string? Название { get; set; }
        public bool? Прием { get; set; }
        public int? КодФакультета { get; set; }
        public int? КодКафедры { get; set; }
        public string? Квалификация { get; set; }
        public string? НазваниеСпец { get; set; }
        public string? СрокОбучения { get; set; }
        public decimal? Цена1к { get; set; }
        public decimal? Цена2к { get; set; }
        public decimal? Цена3к { get; set; }
        public decimal? Цена4к { get; set; }
        public decimal? Цена5к { get; set; }
        public decimal? Цена6к { get; set; }
        public string? Шифр { get; set; }
        public int? Оо { get; set; }
        public int? Цн { get; set; }
        public int? Сн { get; set; }
        public int? Всего { get; set; }
        public string? Основания { get; set; }
        public string? Экзамены { get; set; }
        public string? Описание { get; set; }
        public string? КодНаправления { get; set; }
        public decimal? Цена7к { get; set; }
        public int? КодУровня { get; set; }
        public int? КодФормыОбучения { get; set; }
        public string? Префикс { get; set; }
        public bool Профиль { get; set; }
        public int? КодРодителя { get; set; }
        public int? OldId17 { get; set; }
        public string? ПереводНазвание { get; set; }
        public string? ПереводКвалификация { get; set; }
        public int? КодОоп { get; set; }
        public bool? IsDelite { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Планы> Планыs { get; set; }
    }
}
