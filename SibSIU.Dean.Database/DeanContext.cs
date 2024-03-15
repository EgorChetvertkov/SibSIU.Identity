// Ignore Spelling: Часыs Строкиs Планыs Практикs Обученияs Образованияs Оценкиs Кафедрыs Факультетыs Специальностиs Ведомостиs Студентыs Группыs

using Microsoft.EntityFrameworkCore;
using SibSIU.Dean.Database.Entities;

namespace SibSIU.Dean.Database
{
    public class DeanContext(DbContextOptions<DeanContext> options) : DbContext(options)
    {
        public virtual DbSet<ВсеГруппы> ВсеГруппыs { get; set; } = null!;
        public virtual DbSet<ВсеСтуденты> ВсеСтудентыs { get; set; } = null!;
        public virtual DbSet<ВсеВедомости> ВсеВедомостиs { get; set; } = null!;
        public virtual DbSet<Специальности> Специальностиs { get; set; } = null!;
        public virtual DbSet<Факультеты> Факультетыs { get; set; } = null!;
        public virtual DbSet<Кафедры> Кафедрыs { get; set; } = null!;
        public virtual DbSet<Оценки> Оценкиs { get; set; } = null!;
        public virtual DbSet<УровеньОбразования> УровеньОбразованияs { get; set; } = null!;
        public virtual DbSet<ФормаОбучения> ФормаОбученияs { get; set; } = null!;
        public virtual DbSet<СправочникВидыПрактик> СправочникВидыПрактикs { get; set; } = null!;
        public virtual DbSet<Планы> Планыs { get; set; } = null!;
        public virtual DbSet<ПланыСтроки> ПланыСтрокиs { get; set; } = null!;
        public virtual DbSet<ПланыНовыеЧасы> ПланыНовыеЧасыs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<ВсеГруппы>(entity =>
            {
                _ = entity.HasKey(e => e.Код)
                    .HasName("PK_Группы");

                _ = entity.ToTable("Все_Группы");

                _ = entity.HasIndex(e => e.Название, "IX_Группы")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодОоп, "IX_Код_ООП");

                _ = entity.HasIndex(e => new { e.Название, e.КодФакультета, e.УчебныйГод }, "IX_УникальностьГрупп")
                    .IsUnique()
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодПлана, "IX_кодПлана");

                _ = entity.Property(e => e.IdГруппы).HasMaxLength(300).IsUnicode(false).HasColumnName("ID_Группы");

                _ = entity.Property(e => e.КодОбучения).HasColumnName("Код_обучения");

                _ = entity.Property(e => e.КодОоп).HasColumnName("КодООП");

                _ = entity.Property(e => e.КодПодразделения).HasColumnName("Код_Подразделения");

                _ = entity.Property(e => e.КодПрофиль).HasColumnName("Код_Профиль");

                _ = entity.Property(e => e.КодСпециальности).HasColumnName("Код_Специальности");

                _ = entity.Property(e => e.КодФакультета).HasColumnName("Код_Факультета");

                _ = entity.Property(e => e.Название).HasMaxLength(50).IsUnicode(false);

                _ = entity.Property(e => e.Направление).HasMaxLength(300).IsUnicode(false);

                _ = entity.Property(e => e.ПредседательГак).HasMaxLength(50).IsUnicode(false).HasColumnName("ПредседательГАК");

                _ = entity.Property(e => e.Примечание).HasMaxLength(100).IsUnicode(false);

                _ = entity.Property(e => e.Псевдоним).HasMaxLength(50).IsUnicode(false);

                _ = entity.Property(e => e.Секретарь).HasMaxLength(50).IsUnicode(false);

                _ = entity.Property(e => e.Специализации).HasMaxLength(300).IsUnicode(false);

                _ = entity.Property(e => e.Специальность).HasMaxLength(300).IsUnicode(false);

                _ = entity.Property(e => e.СрокОбучения).HasMaxLength(50).IsUnicode(false);

                _ = entity.Property(e => e.УчебныйГод).HasMaxLength(50).IsUnicode(false);

                _ = entity.Property(e => e.УчебныйПлан).HasMaxLength(255).IsUnicode(false).HasColumnName("Учебный_План");

                _ = entity.Property(e => e.ФормаОбучения).HasColumnName("Форма_Обучения");

                _ = entity.Property(e => e.ШколаХКодПапки).HasColumnName("ШколаХ_КодПапки");

                _ = entity.Property(e => e.ШколаХКодПроверяющего).HasColumnName("ШколаХ_КодПроверяющего");

                _ = entity.Property(e => e.ШколаХНеУчебная).HasColumnName("ШколаХ_НеУчебная");
            });

            _ = modelBuilder.Entity<ФормаОбучения>(entity =>
            {
                _ = entity.HasKey(e => e.Код);

                _ = entity.ToTable("ФормаОбучения");

                _ = entity.Property(e => e.Префикс)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Сокращение)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ТекстДляПриказа)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ФормаОбучения1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ФормаОбучения");
            });

            _ = modelBuilder.Entity<ВсеВедомости>(entity =>
            {
                _ = entity.HasKey(e => e.Код)
                    .HasName("PK_Ведомости");

                _ = entity.ToTable("Все_Ведомости");

                _ = entity.HasIndex(e => e.ТипВедомости, "IX_Ведомости")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.Год, "IX_Ведомости_2")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.Название, "IX_Все_Ведомости")
                    .IsUnique();

                _ = entity.HasIndex(e => e.КодГруппы, "IX_Все_Ведомости_1")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.Цикл, "IX_Все_Ведомости_2");

                _ = entity.HasIndex(e => e.КодТеста, "IX_Все_Ведомости_3")
                    .HasFillFactor(90);

                _ = entity.Property(e => e.ВерсияVedKaf).HasColumnName("Версия_VedKaf");

                _ = entity.Property(e => e.Год).HasMaxLength(10);

                _ = entity.Property(e => e.ДатаВерсииVedKaf)
                    .HasMaxLength(50)
                    .HasColumnName("Дата_Версии_VedKaf");

                _ = entity.Property(e => e.ДатаЗакрытия)
                    .HasColumnType("datetime")
                    .HasColumnName("Дата_Закрытия");

                _ = entity.Property(e => e.ДатаНачалаСессии).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаПоследнегоСохранения)
                    .HasColumnType("datetime")
                    .HasColumnName("Дата_Последнего_Сохранения");

                _ = entity.Property(e => e.ДатаСдачи).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаЭкзамена)
                    .HasColumnType("datetime")
                    .HasColumnName("Дата_Экзамена");

                _ = entity.Property(e => e.Дисциплина).HasMaxLength(250);

                _ = entity.Property(e => e.Зет).HasColumnName("ЗЕТ");

                _ = entity.Property(e => e.ИмяПлана)
                    .HasMaxLength(255)
                    .HasColumnName("Имя_Плана");

                _ = entity.Property(e => e.КодВтаблицаДисциплины).HasColumnName("КодВТаблицаДисциплины");

                _ = entity.Property(e => e.КодГруппы).HasColumnName("Код_Группы");

                _ = entity.Property(e => e.КодКафедры).HasColumnName("Код_Кафедры");

                _ = entity.Property(e => e.КодФакультета).HasColumnName("Код_Факультета");

                _ = entity.Property(e => e.КолВоКт).HasColumnName("Кол-во_КТ");

                _ = entity.Property(e => e.МестоПрактики)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Название).HasMaxLength(300);

                _ = entity.Property(e => e.Пароль).HasMaxLength(20);

                _ = entity.Property(e => e.ПоследнийКомпьютер)
                    .HasMaxLength(50)
                    .HasColumnName("Последний_Компьютер");

                _ = entity.Property(e => e.ПоследнийПользователь)
                    .HasMaxLength(50)
                    .HasColumnName("Последний_Пользователь");

                _ = entity.Property(e => e.ПредседательКомиссии)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Преподаватель)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Приложение)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.РейтингНа2).HasColumnName("Рейтинг_на_2");

                _ = entity.Property(e => e.РейтингНа3).HasColumnName("Рейтинг_на_3");

                _ = entity.Property(e => e.РейтингНа4).HasColumnName("Рейтинг_на_4");

                _ = entity.Property(e => e.РейтингНа5).HasColumnName("Рейтинг_на_5");

                _ = entity.Property(e => e.Специальность)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СтатНезаполненаКт).HasColumnName("СтатНезаполненаКТ");

                _ = entity.Property(e => e.ТаблицаДисциплины)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ТипВедомости).HasColumnName("Тип_Ведомости");

                _ = entity.Property(e => e.Цикл)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            _ = modelBuilder.Entity<УровеньОбразования>(entity =>
            {
                _ = entity.HasKey(e => e.КодЗаписи);

                _ = entity.ToTable("Уровень_образования");

                _ = entity.Property(e => e.КодЗаписи).HasColumnName("Код_записи");

                _ = entity.Property(e => e.ВидПлана)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенкоматКод)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенкоматНаименование)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Категория)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Префикс)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Примечание)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Уровень)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Шифр)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            _ = modelBuilder.Entity<ВсеСтуденты>(entity =>
            {
                _ = entity.HasKey(e => e.Код);

                _ = entity.ToTable("Все_Студенты");

                _ = entity.HasIndex(e => e.КодГруппы, "IX_Все_Студенты")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодОригинала, "IX_Все_Студенты_1");

                _ = entity.HasIndex(e => e.Статус, "IX_Все_Студенты_2")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.ЧитательскийБилет, "IX_Все_Студенты_3")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.Город, "Город");

                _ = entity.Property(e => e.Код).HasComment("Уникальный код студента");

                _ = entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("E_Mail")
                    .HasComment("Адрес электронной почты");

                _ = entity.Property(e => e.EmailForTeams)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.GoogleId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("GoogleID");

                _ = entity.Property(e => e.Guid).HasColumnName("GUID");

                _ = entity.Property(e => e.Icq)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ICQ");

                _ = entity.Property(e => e.Instagram)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.IsInAssociation).HasColumnName("isInAssociation");

                _ = entity.Property(e => e.IsInSchoolX).HasColumnName("isInSchoolX");

                _ = entity.Property(e => e.OldUid).HasColumnName("OldUID");

                _ = entity.Property(e => e.Skype)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.TikTok)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Uid1c).HasColumnName("uid_1c");

                _ = entity.Property(e => e.Uid1cStud).HasColumnName("uid_1c_stud");

                _ = entity.Property(e => e.АкадемСправка)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.АкадемСтипендияПо).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.АкадемСтипендияС).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.АрхивДопЕдХр)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                _ = entity.Property(e => e.АрхивДопЕдХрОбщ)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                _ = entity.Property(e => e.АрхивЗаметки)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.АрхивЛ)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.АрхивЛа)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("АрхивЛА");

                _ = entity.Property(e => e.АрхивПримечание)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.АспДатаПротоколаКафедра).HasColumnType("datetime");

                _ = entity.Property(e => e.АспДатаПротоколаФакультет).HasColumnType("datetime");

                _ = entity.Property(e => e.АспНомерПротоколаКафедра).HasMaxLength(20);

                _ = entity.Property(e => e.АспНомерПротоколаФакультет).HasMaxLength(20);

                _ = entity.Property(e => e.Аттестация1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Результаты аттестаци 1 года(Асп)");

                _ = entity.Property(e => e.Аттестация2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Результаты аттестаци 2 года(Асп)");

                _ = entity.Property(e => e.Аттестация3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Результаты аттестаци 3 года(Асп)");

                _ = entity.Property(e => e.Аттестация4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Результаты аттестаци 4 года(Асп)");

                _ = entity.Property(e => e.БывшаяФамилия)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.БывшееИмя)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Версия)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                _ = entity.Property(e => e.ВконтактеId).HasColumnName("ВКонтактеID");

                _ = entity.Property(e => e.ВоенДолжность)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенЗвание)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенМестоСлужбы)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенОрган)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Военкомат).HasMaxLength(800);

                _ = entity.Property(e => e.ВоенкоматВидУчета)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоеннГодность)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоеннСпецУчет)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоеннСпециальность)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенныйБилет)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВоенныйБилетДата).HasColumnType("datetime");

                _ = entity.Property(e => e.ВоинскоеЗвание)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВремяБлокировки).HasColumnType("datetime");

                _ = entity.Property(e => e.ВремяДляТимс).HasColumnType("datetime");

                _ = entity.Property(e => e.ВтороеОбразование).HasColumnName("Второе_Образование");

                _ = entity.Property(e => e.ВыпускникиВк)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Выпускники_ВК");

                _ = entity.Property(e => e.ВыпускникиПримечанияАдминистратора)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("Выпускники_Примечания_Администратора");

                _ = entity.Property(e => e.ВыпускникиФакультет)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Выпускники_Факультет");

                _ = entity.Property(e => e.ВысшееОбразованиеПолучаемоеВпервые)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Высшее образование, получаемое впервые");

                _ = entity.Property(e => e.ГдеНаходитсяУз)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Где_Находится_УЗ")
                    .HasComment("Местонахождение учебного заведения");

                _ = entity.Property(e => e.ГисДата)
                    .HasColumnType("datetime")
                    .HasColumnName("ГИС_Дата");

                _ = entity.Property(e => e.ГисСтатус).HasColumnName("ГИС_Статус");

                _ = entity.Property(e => e.ГодОкончания).HasColumnName("Год окончания");

                _ = entity.Property(e => e.ГодОкончанияУз)
                    .HasColumnName("Год_Окончания_УЗ")
                    .HasComment("Год окончания учебного заведения");

                _ = entity.Property(e => e.ГодПоступления)
                    .HasColumnName("Год_Поступления")
                    .HasComment("Год Поступления в ВУЗ");

                _ = entity.Property(e => e.ГодПоступления1).HasColumnName("Год поступления");

                _ = entity.Property(e => e.ГодРождения).HasComment("Год Рождения(Асп)");

                _ = entity.Property(e => e.ГородПп)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("Город_ПП")
                    .HasComment("Город текущего проживания");

                _ = entity.Property(e => e.ГородРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Город_Родители")
                    .HasComment("Город проживания родителей (если студент приезжий)");

                _ = entity.Property(e => e.Гражданство)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ГруппаУчета)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ГупМср).HasColumnName("ГУП_МСР");

                _ = entity.Property(e => e.ГупМсрДата)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("ГУП_МСР_Дата");

                _ = entity.Property(e => e.ДатаАкадОтпуск).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ДатаАкадОтпускС).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ДатаВыдачи)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Дата_Выдачи")
                    .HasComment("Дата выдачи паспорта");

                _ = entity.Property(e => e.ДатаВыдачи1)
                    .HasColumnType("datetime")
                    .HasColumnName("ДатаВыдачи");

                _ = entity.Property(e => e.ДатаВыдачи2)
                    .HasColumnType("datetime")
                    .HasColumnName("Дата выдачи");

                _ = entity.Property(e => e.ДатаВыдачиОригинала)
                    .HasColumnType("datetime")
                    .HasColumnName("Дата выдачи (оригинала)");

                _ = entity.Property(e => e.ДатаЗачисления)
                    .HasColumnType("datetime")
                    .HasComment("ДатаЗачисления(Асп)");

                _ = entity.Property(e => e.ДатаКонСессии).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ДатаНачСессии).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ДатаОкончания)
                    .HasColumnType("datetime")
                    .HasComment("Дата Окончания Обучения(Асп)");

                _ = entity.Property(e => e.ДатаПодачиДокументов)
                    .HasColumnType("datetime")
                    .HasComment("Дата подачи документов абитуриентом");

                _ = entity.Property(e => e.ДатаПоследнегоВхода).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаПросмотраЛенты).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаРешения).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаРождения)
                    .HasColumnType("datetime")
                    .HasColumnName("Дата_Рождения")
                    .HasComment("Дата рождения");

                _ = entity.Property(e => e.ДатаСменыПароля).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаСозданияПароля).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатыВыдачиАттестата)
                    .HasColumnType("datetime")
                    .HasColumnName("Даты_Выдачи_Аттестата")
                    .HasComment("дата выдачи аттестатта");

                _ = entity.Property(e => e.Долг).HasComment("Является должником (для ИДЗО)");

                _ = entity.Property(e => e.Должность)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Должность в настоящее время (заочник)");

                _ = entity.Property(e => e.ДолжностьМатери)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Должность_Матери")
                    .HasComment("Должность матери");

                _ = entity.Property(e => e.ДолжностьОтца)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Должность_Отца")
                    .HasComment("Должность отца");

                _ = entity.Property(e => e.ДомКвПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Дом_Кв_ПП")
                    .HasComment("Дом/Квартира текущего проживания");

                _ = entity.Property(e => e.ДомКвРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Дом_Кв_Родители")
                    .HasComment("Дом/Квартира проживания родителей (если студент приезжий)");

                _ = entity.Property(e => e.ДополнОтпуск)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Дополнительный оплачиваемы отпуск");

                _ = entity.Property(e => e.ИдентификаторScopus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИзучаемыйЯзык)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Изучаемый_Язык")
                    .HasComment("Изучаемый язык в университете");

                _ = entity.Property(e => e.Имя)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Имя студента");

                _ = entity.Property(e => e.ИмяПолучателяОригинала)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Имя получателя (оригинала)");

                _ = entity.Property(e => e.ИмяСканаФло)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ИмяСканаФЛО");

                _ = entity.Property(e => e.ИндексПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Индекс_ПП")
                    .HasComment("Почтовый индекс текущего проживания");

                _ = entity.Property(e => e.ИндексРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Индекс_Родители")
                    .HasComment("Почтовый индек родителей (если студент приезжий)");

                _ = entity.Property(e => e.Инн)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ИНН");

                _ = entity.Property(e => e.КандИностранный)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Кандидатский экзамен по иностранному языку(Асп)");

                _ = entity.Property(e => e.КандСпециальность)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Кандидатский экзамен по специальности(Асп)");

                _ = entity.Property(e => e.КандФилософия)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Кандидатский экзамен по философии(Асп)");

                _ = entity.Property(e => e.КатегорияУчета)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КвалификацияСтудента)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Квалификация под диплому (заочник)");

                _ = entity.Property(e => e.КвартираПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Квартира_ПП");

                _ = entity.Property(e => e.КвартираРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Квартира_Родители");

                _ = entity.Property(e => e.КемВыдан)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Кем_Выдан")
                    .HasComment("Отделение, которым выдан паспорт");

                _ = entity.Property(e => e.КладрПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Кладр_ПП");

                _ = entity.Property(e => e.КладрРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Кладр_Родители");

                _ = entity.Property(e => e.КлючАвторизации)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Книга)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КодГруппы)
                    .HasColumnName("Код_Группы")
                    .HasComment("Код Группы, в которой учится студент");

                _ = entity.Property(e => e.КодОксm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("КодОКСM");

                _ = entity.Property(e => e.КодПодраздел)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КодФрдо2019видыДокумента).HasColumnName("КодФРДО2019ВидыДокумента");

                _ = entity.Property(e => e.КодФрдо2019видыДокументаСпо).HasColumnName("КодФРДО2019ВидыДокументаСПО");

                _ = entity.Property(e => e.КодФрдо2019иностранец).HasColumnName("КодФРДО2019Иностранец");

                _ = entity.Property(e => e.КодФрдо2019кодыНпс).HasColumnName("КодФРДО2019КодыНПС");

                _ = entity.Property(e => e.КодФрдо2019кодыНпсспо).HasColumnName("КодФРДО2019КодыНПССПО");

                _ = entity.Property(e => e.КодФрдо2019пол).HasColumnName("КодФРДО2019Пол");

                _ = entity.Property(e => e.КодФрдо2019статусыДокумента).HasColumnName("КодФРДО2019СтатусыДокумента");

                _ = entity.Property(e => e.КодФрдо2019уровниОбразования).HasColumnName("КодФРДО2019УровниОбразования");

                _ = entity.Property(e => e.КодФрдо2019уровниОбразованияСпо).HasColumnName("КодФРДО2019УровниОбразованияСПО");

                _ = entity.Property(e => e.КодФрдо2019финансированиеОбучения).HasColumnName("КодФРДО2019ФинансированиеОбучения");

                _ = entity.Property(e => e.КодФрдо2019формаОбучения).HasColumnName("КодФРДО2019ФормаОбучения");

                _ = entity.Property(e => e.КодФрдо2021финансированиеОбучения).HasColumnName("КодФРДО2021ФинансированиеОбучения");

                _ = entity.Property(e => e.КодФрдо2021формаПолученияОбразования).HasColumnName("КодФРДО2021ФормаПолученияОбразования");

                _ = entity.Property(e => e.КодФрдонаименованиеПрофессийРабочих).HasColumnName("КодФРДОНаименованиеПрофессийРабочих");

                _ = entity.Property(e => e.КодФрдоподтверждениеОбмена).HasColumnName("КодФРДОПодтверждениеОбмена");

                _ = entity.Property(e => e.КодФрдоподтверждениеУтраты).HasColumnName("КодФРДОПодтверждениеУтраты");

                _ = entity.Property(e => e.КодФрдопрограммаПрофессиональногоОбучения).HasColumnName("КодФРДОПрограммаПрофессиональногоОбучения");

                _ = entity.Property(e => e.КодФрдоразрядКлассКатегория).HasColumnName("КодФРДОРазрядКлассКатегория");

                _ = entity.Property(e => e.Командировки)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Командировки");

                _ = entity.Property(e => e.ЛицСчетБанк)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Логин)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Льготы)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("Льготы при поступлении");

                _ = entity.Property(e => e.МестоРабМатери)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Место_Раб_Матери")
                    .HasComment("Место работы матери");

                _ = entity.Property(e => e.МестоРабОтца)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Место_Раб_Отца")
                    .HasComment("Место работы отца");

                _ = entity.Property(e => e.МестоРаботы)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasComment("МестоРаботы(Асп)");

                _ = entity.Property(e => e.МестоРождения)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Мобильный)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НазваниеДокумента)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Название документа");

                _ = entity.Property(e => e.НаименованиеДокументаОбОбразованииОригинала)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Наименование документа об образовании (оригинала)");

                _ = entity.Property(e => e.НаименованиеКвалификации)
                    .HasMaxLength(1250)
                    .IsUnicode(false)
                    .HasColumnName("Наименование квалификации");

                _ = entity.Property(e => e.НаименованиеСпециальностиНаправленияПодготовки)
                    .HasMaxLength(1250)
                    .IsUnicode(false)
                    .HasColumnName("Наименование специальности, направления подготовки");

                _ = entity.Property(e => e.НаличиеМатери)
                    .HasColumnName("Наличие_Матери")
                    .HasComment("Наличие матери");

                _ = entity.Property(e => e.НаличиеОтца)
                    .HasColumnName("Наличие_Отца")
                    .HasComment("Наличие отца");

                _ = entity.Property(e => e.НаучнаяСпециальность)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Научная Специальность(Асп)");

                _ = entity.Property(e => e.НаучныйРуководитель)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Научный Руководитель(Консультант)");

                _ = entity.Property(e => e.Национальность)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Национальность студента");

                _ = entity.Property(e => e.НеМестный).HasComment("Если адрес родителей не совпалает с текущим");

                _ = entity.Property(e => e.НомерАттестата)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Номер_Аттестата")
                    .HasComment("Номер аттестата(диплома)");

                _ = entity.Property(e => e.НомерДиплома)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НомерДоговора)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Номер_договора");

                _ = entity.Property(e => e.НомерДокумента).HasColumnName("Номер документа");

                _ = entity.Property(e => e.НомерДокументаДляИзменения).HasColumnName("Номер документа для изменения");

                _ = entity.Property(e => e.НомерЗачетнойКнижки)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Номер_Зачетной_Книжки")
                    .HasComment("Номер зачетной книжки");

                _ = entity.Property(e => e.НомерКомнаты)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Номер_Комнаты")
                    .HasComment("Номер комнаты в общежитии");

                _ = entity.Property(e => e.НомерЛд)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("НомерЛД")
                    .HasComment("Номер личного дела(Абит)");

                _ = entity.Property(e => e.НомерМедПолиса)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Номер_Мед_Полиса")
                    .HasComment("Номер медицинского полиса");

                _ = entity.Property(e => e.НомерОбщежития)
                    .HasColumnName("Номер_Общежития")
                    .HasComment("Номер общежития");

                _ = entity.Property(e => e.НомерОригинала).HasColumnName("Номер (оригинала)");

                _ = entity.Property(e => e.НомерПаспорта)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Номер_Паспорта")
                    .HasComment("Номер паспорта");

                _ = entity.Property(e => e.НомерПриказаОзачислении)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("НомерПриказаОЗачислении")
                    .HasComment("Номер Приказа О Зачислении(Асп)");

                _ = entity.Property(e => e.НомерСоцСтрахования)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НуждаетсяВобщежитии).HasColumnName("НуждаетсяВОбщежитии");

                _ = entity.Property(e => e.ОбразовательнаяПрограмма)
                    .HasMaxLength(1250)
                    .IsUnicode(false)
                    .HasColumnName("Образовательная программа");

                _ = entity.Property(e => e.Общежитие).HasComment("Живет в общежитии");

                _ = entity.Property(e => e.ОбщежитиеДата).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ОбщежитиеДоговор)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ОбщежитиеПл)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Озо).HasColumnName("ОЗО");

                _ = entity.Property(e => e.Опекунство).HasComment("Наличие опекунства");

                _ = entity.Property(e => e.Основания)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Основания обучения (ОО, ЦН,)");

                _ = entity.Property(e => e.ОтличникУз)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Отличник_УЗ")
                    .HasComment("Отличия при окончании учебного заведения");

                _ = entity.Property(e => e.Отчество)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Отчество студента");

                _ = entity.Property(e => e.ОтчествоПолучателяОригинала)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Отчество получателя (оригинала)");

                _ = entity.Property(e => e.Пароль).HasMaxLength(500);

                _ = entity.Property(e => e.ПереводДокументов)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Причина перевода документов абитуриентом");

                _ = entity.Property(e => e.ПодтверждениеОбмена)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Подтверждение обмена");

                _ = entity.Property(e => e.ПодтверждениеУничтожения)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Подтверждение уничтожения");

                _ = entity.Property(e => e.ПодтверждениеУтраты)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Подтверждение утраты");

                _ = entity.Property(e => e.Пол)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasComment("Под студента");

                _ = entity.Property(e => e.ПолученДипломСотличием).HasColumnName("ПолученДипломСОтличием");

                _ = entity.Property(e => e.ПоследнийIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ПоследнийIP");

                _ = entity.Property(e => e.Пособие).HasComment("Выдается пособие");

                _ = entity.Property(e => e.ПостоянноПроживаетВобщежити).HasColumnName("ПостоянноПроживаетВОбщежити");

                _ = entity.Property(e => e.ПрикрепКафедра)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasComment("Прикрепленность к кафедре(Асп)");

                _ = entity.Property(e => e.Примечание)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasComment("Примечание");

                _ = entity.Property(e => e.ПриписноеСвидетельство)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПродленаСессия).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ПродлениеСессии).HasColumnType("datetime");

                _ = entity.Property(e => e.ПроживаетВобщежитииПо)
                    .HasColumnType("datetime")
                    .HasColumnName("ПроживаетВОбщежитииПО");

                _ = entity.Property(e => e.ПроживаетВобщежитииС)
                    .HasColumnType("datetime")
                    .HasColumnName("ПроживаетВОбщежитииС");

                _ = entity.Property(e => e.ПротоколВыдачиДиплома).HasMaxLength(50);

                _ = entity.Property(e => e.РайонПп)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Район_ПП");

                _ = entity.Property(e => e.РайонРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Район_Родители");

                _ = entity.Property(e => e.РегНомерДиплома)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.РегионПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Регион_ПП")
                    .HasComment("Регион текущего проживания");

                _ = entity.Property(e => e.РегионРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Регион_Родители")
                    .HasComment("Регион проживания родителей (если студент приезжий)");

                _ = entity.Property(e => e.РегистрационныйNОригинала)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Регистрационный N (оригинала)");

                _ = entity.Property(e => e.РегистрационныйНомер)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                _ = entity.Property(e => e.РегистрационныйНомер1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Регистрационный номер");

                _ = entity.Property(e => e.Сан)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Село).HasComment("Абитуриент из сельской местности");

                _ = entity.Property(e => e.СерияАттестата)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Серия_Аттестата")
                    .HasComment("Серия аттестата(диплома)");

                _ = entity.Property(e => e.СерияДокумента)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Серия документа");

                _ = entity.Property(e => e.СерияИномерПриложенияКдиплому)
                    .HasMaxLength(20)
                    .HasColumnName("СерияИНомерПриложенияКДиплому");

                _ = entity.Property(e => e.СерияМедПолиса)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Серия_Мед_Полиса")
                    .HasComment("Серия медицинского полиса");

                _ = entity.Property(e => e.СерияОригинала)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Серия (оригинала)");

                _ = entity.Property(e => e.Слушатель).HasComment("Является студент слушателем");

                _ = entity.Property(e => e.Снилс)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("СНИЛС");

                _ = entity.Property(e => e.СоответсвиеВпо)
                    .HasColumnName("СоответсвиеВПО")
                    .HasComment("Соответствует ранее полученному ВПО");

                _ = entity.Property(e => e.Состав)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СостоитВБраке)
                    .HasColumnName("Состоит_В_Браке")
                    .HasComment("Состоит в браке");

                _ = entity.Property(e => e.СоцСтипендияПо).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.СоцСтипендияС).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.СрБаллЕгэ).HasColumnName("СрБаллЕГЭ");

                _ = entity.Property(e => e.СрокОбученияЛет).HasColumnName("Срок обучения, лет");

                _ = entity.Property(e => e.СрокОбученияЧасов).HasColumnName("Срок обучения, часов");

                _ = entity.Property(e => e.Стаж)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Стаж работы (заочник)");

                _ = entity.Property(e => e.Статус).HasComment("Статус (учится, отчислен, абитуриент)");

                _ = entity.Property(e => e.СтранаПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Страна_ПП")
                    .HasComment("Страна текущего проживания");

                _ = entity.Property(e => e.СтранаРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Страна_Родители")
                    .HasComment("Страна проживания родителей (если студент приезжий)");

                _ = entity.Property(e => e.ТелефонПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Телефон_ПП")
                    .HasComment("Телефон текущего проживания");

                _ = entity.Property(e => e.ТелефонРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Телефон_Родители")
                    .HasComment("Телефон родителей (если студент приезжий)");

                _ = entity.Property(e => e.ТемаДиплома)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ТемаДипломаПеревод).HasColumnType("text");

                _ = entity.Property(e => e.Тимя)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ТИмя");

                _ = entity.Property(e => e.ТипОбразДокумента)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Тип_Образ_Документа")
                    .HasComment("Тип документа о образовании");

                _ = entity.Property(e => e.ТипУдостоверения)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Тип_Удостоверения")
                    .HasComment("Тип документа, удостоверяющий личность");

                _ = entity.Property(e => e.ТипУз)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Тип_УЗ");

                _ = entity.Property(e => e.Тотчество)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ТОтчество");

                _ = entity.Property(e => e.Тфамилия)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ТФамилия");

                _ = entity.Property(e => e.УлицаПп)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Улица_ПП")
                    .HasComment("Улица текущего проживания");

                _ = entity.Property(e => e.УлицаРодители)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Улица_Родители")
                    .HasComment("Улица проживания родителей (если студент приезжий)");

                _ = entity.Property(e => e.УслОбучения).HasComment("Код условия обучения по справочнику Ув");

                _ = entity.Property(e => e.УчЗаведение)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Уч_Заведение")
                    .HasComment("Учебное заведение, которое окончил студент");

                _ = entity.Property(e => e.УчебныйПлан)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Фамилия)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Фамилия студента");

                _ = entity.Property(e => e.ФамилияПолучателяОригинала)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Фамилия получателя (оригинала)");

                _ = entity.Property(e => e.ФиоМатери)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ФИО_Матери")
                    .HasComment("ФИО Матери");

                _ = entity.Property(e => e.ФиоОтца)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ФИО_Отца")
                    .HasComment("ФИО отца");

                _ = entity.Property(e => e.ФлоПо)
                    .HasColumnType("datetime")
                    .HasColumnName("ФЛО_ПО");

                _ = entity.Property(e => e.ФлоС)
                    .HasColumnType("datetime")
                    .HasColumnName("ФЛО_С");

                _ = entity.Property(e => e.ФормаДокумента)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Форма_Документа");

                _ = entity.Property(e => e.ФормаОбучения)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Форма Обучения(Асп)");

                _ = entity.Property(e => e.Фото)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasComment("Имя файла фотографии");

                _ = entity.Property(e => e.ЦерковноеИмя)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Церковное_имя");

                _ = entity.Property(e => e.ЧислоБратьевИСестер)
                    .HasColumnName("Число_Братьев_И_Сестер")
                    .HasComment("Число братьев и сестер");

                _ = entity.Property(e => e.ЧислоДетей)
                    .HasColumnName("Число_Детей")
                    .HasComment("Число детей");

                _ = entity.Property(e => e.ЧитательскийБилет)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ШколаХДоступКчату).HasColumnName("ШколаХ_ДоступКЧату");

                _ = entity.Property(e => e.ШколаХМодератор).HasColumnName("ШколаХ_Модератор");
            });

            _ = modelBuilder.Entity<Специальности>(entity =>
            {
                _ = entity.HasKey(e => e.Код)
                    .HasName("PK__Специальности__170E59B8");

                _ = entity.ToTable("Специальности");

                _ = entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                _ = entity.Property(e => e.IsDelite).HasColumnName("isDelite");

                _ = entity.Property(e => e.OldId17).HasColumnName("OldID17");

                _ = entity.Property(e => e.Квалификация)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КодКафедры).HasColumnName("Код_Кафедры");

                _ = entity.Property(e => e.КодНаправления)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КодОоп).HasColumnName("КодООП");

                _ = entity.Property(e => e.КодФакультета).HasColumnName("Код_Факультета");

                _ = entity.Property(e => e.Название)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НазваниеСпец)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Название_Спец");

                _ = entity.Property(e => e.Оксо)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ОКСО");

                _ = entity.Property(e => e.Оо).HasColumnName("ОО");

                _ = entity.Property(e => e.Описание).HasColumnType("text");

                _ = entity.Property(e => e.Основания)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПереводКвалификация)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПереводНазвание)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Префикс)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Сн).HasColumnName("СН");

                _ = entity.Property(e => e.Специальность)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СрокОбучения)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Срок_Обучения");

                _ = entity.Property(e => e.СтараяСпециальность)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Цена1к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цена2к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цена3к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цена4к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цена5к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цена6к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цена7к).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.Цн).HasColumnName("ЦН");

                _ = entity.Property(e => e.Шифр)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Экзамены)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            _ = modelBuilder.Entity<Факультеты>(entity =>
            {
                _ = entity.HasKey(e => e.Код);

                _ = entity.ToTable("Факультеты");

                _ = entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                _ = entity.Property(e => e.Url)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                _ = entity.Property(e => e.Аудитория)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВнутрТелефон)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Декан)
                    .HasMaxLength(110)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ЕстьДо).HasColumnName("ЕстьДО");

                _ = entity.Property(e => e.ЗамДекана)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Иоф)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ИОФ");

                _ = entity.Property(e => e.Иофр)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ИОФР");

                _ = entity.Property(e => e.КодФилиала).HasColumnName("Код_филиала");

                _ = entity.Property(e => e.МаксСправокВобр).HasColumnName("МаксСправокВОбр");

                _ = entity.Property(e => e.МаксСправокСтудентаВобр).HasColumnName("МаксСправокСтудентаВОбр");

                _ = entity.Property(e => e.Описание).HasColumnType("text");

                _ = entity.Property(e => e.Пк).HasColumnName("ПК");

                _ = entity.Property(e => e.Подпись1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Подпись2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Подпись3)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Подпись4)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПодписьВедомости)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПодписьЗф1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ПодписьЗФ1");

                _ = entity.Property(e => e.ПодписьЗф2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ПодписьЗФ2");

                _ = entity.Property(e => e.ПодписьЗф3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ПодписьЗФ3");

                _ = entity.Property(e => e.ПодписьЗф4)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ПодписьЗФ4");

                _ = entity.Property(e => e.Псевдоним)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                _ = entity.Property(e => e.РодПадеж)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Секретарь)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СкрытьСцос).HasColumnName("СкрытьСЦОС");

                _ = entity.Property(e => e.Сокращение)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СправкиИндекс)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Телефон)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Тип)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ТипОбучения).HasColumnName("Тип_Обучения");

                _ = entity.Property(e => e.ФайлСтиля)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Факультет)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Шифр)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            _ = modelBuilder.Entity<Кафедры>(entity =>
            {
                _ = entity.HasKey(e => e.Код);

                _ = entity.ToTable("Кафедры");

                _ = entity.Property(e => e.Код).ValueGeneratedNever();

                _ = entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                _ = entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                _ = entity.Property(e => e.Аудитория)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ВнутрТелефон)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ЗавКафедрой)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КодФакультета).HasColumnName("Код_Факультета");

                _ = entity.Property(e => e.КоэффициентМпгу).HasColumnName("КоэффициентМПГУ");

                _ = entity.Property(e => e.Название).HasMaxLength(300);

                _ = entity.Property(e => e.ПрефиксКафедры)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Примечание)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Сокращение)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Телефон)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Цмк).HasColumnName("ЦМК");
            });

            _ = modelBuilder.Entity<Оценки>(entity =>
            {
                _ = entity.HasKey(e => e.Код);

                _ = entity.ToTable("Оценки");

                _ = entity.HasIndex(e => e.КодВедомости, "IX_Оценки")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодСтудента, "IX_Оценки_1")
                    .HasFillFactor(90);

                _ = entity.Property(e => e.Ects).HasColumnName("ECTS");

                _ = entity.Property(e => e.Версия)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                _ = entity.Property(e => e.ДатаИзменения).HasColumnType("smalldatetime");

                _ = entity.Property(e => e.ДатаПересдачи1)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Дата_Пересдачи1");

                _ = entity.Property(e => e.ДатаПересдачи2)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Дата_Пересдачи2");

                _ = entity.Property(e => e.ДатаПересдачи3)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Дата_Пересдачи3");

                _ = entity.Property(e => e.ДатаСдачи)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Дата_Сдачи");

                _ = entity.Property(e => e.ИтоговаяОценка).HasColumnName("Итоговая_Оценка");

                _ = entity.Property(e => e.ИтоговыйПроцент).HasColumnName("Итоговый_Процент");

                _ = entity.Property(e => e.КодВедомости).HasColumnName("Код_Ведомости");

                _ = entity.Property(e => e.КодСтудента).HasColumnName("Код_Студента");

                _ = entity.Property(e => e.КонтрольнаяСумма).HasColumnName("Контрольная_Сумма");

                _ = entity.Property(e => e.НомерВВедомости).HasColumnName("Номер_В_Ведомости");

                _ = entity.Property(e => e.ОценкаНаЭкзаменеЗачете).HasColumnName("Оценка_На_Экзамене(Зачете)");

                _ = entity.Property(e => e.ОценкаПоРейтингу).HasColumnName("Оценка_По_Рейтингу");

                _ = entity.Property(e => e.Протокол)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Протокол2)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Протокол3)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПроцентВыполнения).HasColumnName("Процент_Выполнения");

                _ = entity.Property(e => e.СтрокаВВедомости).HasColumnName("Строка_В_Ведомости");

                _ = entity.Property(e => e.ТемаКурсовой)
                    .HasMaxLength(1200)
                    .IsUnicode(false);
            });

            _ = modelBuilder.Entity<СправочникВидыПрактик>(entity =>
            {
                _ = entity.HasKey(e => e.Код);

                _ = entity.ToTable("СправочникВидыПрактик");

                _ = entity.Property(e => e.Код).ValueGeneratedNever();

                _ = entity.Property(e => e.КодВграфике).HasColumnName("КодВГрафике");

                _ = entity.Property(e => e.Наименование)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Префикс)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            _ = modelBuilder.Entity<Планы>(entity =>
            {
                _ = entity.HasKey(e => e.Код)
                    .HasName("PK__Планы__039170F0");

                _ = entity.ToTable("Планы");

                _ = entity.HasIndex(e => e.ИмяФайла, "IX_Планы");

                _ = entity.HasIndex(e => e.УчебныйГод, "IX_Планы_1")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодСпециальности, "IX_Планы_2");

                _ = entity.HasIndex(e => e.КодГрафика, "IX_Планы_3");

                _ = entity.HasIndex(e => e.КодОоп, "IX_Планы_4");

                _ = entity.HasIndex(e => new { e.Код, e.УчебныйГод }, "Код_УчебныйГод");

                _ = entity.Property(e => e.БазаОбучения)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.БазовоеОбразование)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Версия)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                _ = entity.Property(e => e.ВоеннаяСпециальность)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ГвИга).HasColumnName("ГвИГА");

                _ = entity.Property(e => e.ГоловнаяОрганизация)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ДатаАттестации).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаВерсииПриложения).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаГоса)
                    .HasColumnType("datetime")
                    .HasColumnName("ДатаГОСа");

                _ = entity.Property(e => e.ДатаИзмФайла).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаИзмененияФайла).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаЛиквидАкадемРазницы).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаПриказаСоздКомиссии).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаПроверки).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаПротокКомиссии).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаСертификатаИмца)
                    .HasColumnType("datetime")
                    .HasColumnName("ДатаСертификатаИМЦА");

                _ = entity.Property(e => e.ДатаУтверРектором).HasColumnType("datetime");

                _ = entity.Property(e => e.ДатаУтверСоветом).HasColumnType("datetime");

                _ = entity.Property(e => e.ДвИга).HasColumnName("ДвИГА");

                _ = entity.Property(e => e.ЗаочНедельНаГэк).HasColumnName("ЗаочНедельНаГЭК");

                _ = entity.Property(e => e.ЗетвНеделю).HasColumnName("ЗЕТвНеделю");

                _ = entity.Property(e => e.ИзмененияВнесены).HasColumnType("datetime");

                _ = entity.Property(e => e.ИзмененияВнёс)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Икэкурс).HasColumnName("ИКЭКурс");

                _ = entity.Property(e => e.ИмяВуза)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяКомпьютера)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяПодразделения)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяПользователя)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяПриложения)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяСтуд)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяФайла)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ИмяФайлаГоса)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ИмяФайлаГОСа");

                _ = entity.Property(e => e.Квалификация)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.КодАктивногоОоп).HasColumnName("КодАктивногоООП");

                _ = entity.Property(e => e.КодКафРукНп).HasColumnName("КодКафРукНП");

                _ = entity.Property(e => e.КодОоп).HasColumnName("КодООП");

                _ = entity.Property(e => e.КредитовВнеделе).HasColumnName("КредитовВНеделе");

                _ = entity.Property(e => e.КурсыОбучения)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НагрузкаВчасахНпо).HasColumnName("НагрузкаВЧасахНПО");

                _ = entity.Property(e => e.НагрузкаКоэфСтудНаПрепод).HasColumnType("decimal(18, 0)");

                _ = entity.Property(e => e.НазваниеКредитов)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НомПротокСовета)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НомерПриказаСоздКомиссии)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НомерПротокКомиссии)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.НомерФгос)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("НомерФГОС");

                _ = entity.Property(e => e.ОснованиеИуп).HasColumnName("ОснованиеИУП");

                _ = entity.Property(e => e.ОтчествоСтуд)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПланОдобрен)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ПолноеИмяФайла)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Предназначение)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Примечание).HasColumnType("text");

                _ = entity.Property(e => e.Резерв)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СкрытьВрпд).HasColumnName("СкрытьВРПД");

                _ = entity.Property(e => e.Специализация)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Специальность)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СтатусВнагрузке).HasColumnName("СтатусВНагрузке");

                _ = entity.Property(e => e.СтатусРпд).HasColumnName("СтатусРПД");

                _ = entity.Property(e => e.Стреками).HasColumnName("СТреками");

                _ = entity.Property(e => e.ТипГоса).HasColumnName("ТипГОСа");

                _ = entity.Property(e => e.Титул)
                    .HasMaxLength(2100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.УровеньПодготовкиСпо)
                    .HasMaxLength(15)
                    .HasColumnName("УровеньПодготовкиСПО");

                _ = entity.Property(e => e.Утверждение)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                _ = entity.Property(e => e.УчПрПлюсТо).HasColumnName("УчПрПлюсТО");

                _ = entity.Property(e => e.УчебныйГод)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Факультет)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ФамилияСтуд)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Фгос).HasColumnName("ФГОС");

                _ = entity.Property(e => e.Фио)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ФИО");

                _ = entity.Property(e => e.ЧасовВкредите).HasColumnName("ЧасовВКредите");

                _ = entity.Property(e => e.ШифрСертификата)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                _ = entity.Property(e => e.ШифрСпециальности)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ЭлементовВнеделе).HasColumnName("ЭлементовВНеделе");

                _ = entity.HasOne(d => d.КодСпециальностиNavigation)
                    .WithMany(p => p.Планыs)
                    .HasForeignKey(d => d.КодСпециальности)
                    .HasConstraintName("FK_Планы_Специальности");

                _ = entity.HasOne(d => d.КодФакультетаNavigation)
                    .WithMany(p => p.Планыs)
                    .HasForeignKey(d => d.КодФакультета)
                    .HasConstraintName("FK_Планы_Факультеты");
            });

            _ = modelBuilder.Entity<ПланыСтроки>(entity =>
            {
                _ = entity.HasKey(e => e.Код)
                    .HasName("PK__ПланыСтроки__188C8DD6");

                _ = entity.ToTable("ПланыСтроки");

                _ = entity.HasIndex(e => e.КодПлана, "IX_ПланыСтроки")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодКафедры, "IX_ПланыСтроки_1");

                _ = entity.HasIndex(e => e.НомерСтроки, "IX_ПланыСтроки_2")
                    .HasFillFactor(90);

                _ = entity.HasIndex(e => e.КодДисциплины, "IX_ПланыСтроки_3");

                _ = entity.HasIndex(e => e.Дисциплина, "IX_ПланыСтроки_4");

                _ = entity.HasIndex(e => e.КодОоп, "IX_ПланыСтроки_5");

                _ = entity.HasIndex(e => new { e.КодПлана, e.КодКафедры }, "КодКафедры_КодПлана");

                _ = entity.Property(e => e.DvnotEquals).HasColumnName("DVnotEquals");

                _ = entity.Property(e => e.Версия)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                _ = entity.Property(e => e.Дисциплина)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ДисциплинаКод)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Зетизучено).HasColumnName("ЗЕТизучено");

                _ = entity.Property(e => e.Зетфакт).HasColumnName("ЗЕТфакт");

                _ = entity.Property(e => e.КодОоп).HasColumnName("КодООП");

                _ = entity.Property(e => e.Компетенции)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Компонент)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                _ = entity.Property(e => e.МаксУчНагрузкаСпо).HasColumnName("МаксУчНагрузкаСПО");

                _ = entity.Property(e => e.НомерСтрокиВблоке).HasColumnName("НомерСтрокиВБлоке");

                _ = entity.Property(e => e.Резерв)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                _ = entity.Property(e => e.СкрытьВрпд).HasColumnName("СкрытьВРПД");

                _ = entity.Property(e => e.СчитатьБезЗет).HasColumnName("СчитатьБезЗЕТ");

                _ = entity.Property(e => e.СчитатьВплане).HasColumnName("СчитатьВПлане");

                _ = entity.Property(e => e.ЦелиОсвоения)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                _ = entity.Property(e => e.Цикл)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ЦиклКод)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                _ = entity.Property(e => e.ЧасовВзет).HasColumnName("ЧасовВЗЕТ");

                _ = entity.Property(e => e.ЧасовПоГосу).HasColumnName("ЧасовПоГОСу");

                _ = entity.Property(e => e.ЧасовПоЗет).HasColumnName("ЧасовПоЗЕТ");

                _ = entity.HasOne(d => d.КодПланаNavigation)
                    .WithMany(p => p.ПланыСтрокиs)
                    .HasForeignKey(d => d.КодПлана)
                    .HasConstraintName("FK_ПланыСтроки_Планы");
            });

            _ = modelBuilder.Entity<ПланыНовыеЧасы>(entity =>
            {
                _ = entity.HasKey(e => e.Код)
                    .HasName("PK__ПланыНов__AE76132E164452B1");

                _ = entity.ToTable("ПланыНовыеЧасы");

                _ = entity.HasIndex(e => e.КодОбъекта, "IX_ПланыНовыеЧасы");
            });
        }
    }
}
