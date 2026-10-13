namespace SUR_Integrator.Web.Services
{
    public class Deal
    {
        public string Id { get; set; } = "";
        public string OwnerLogin { get; set; } = "";
        public string Sender { get; set; } = "";
        public string Destination { get; set; } = "";
        public string Product { get; set; } = "";
        public string TnvedCode { get; set; } = "";
        public decimal Value { get; set; }
        public decimal DutyRate { get; set; }
        public decimal VatRate { get; set; }
        public string Risk { get; set; } = "green";
        public string Note { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public decimal Duty => Math.Round(Value * DutyRate / 100m);
        public decimal Vat => Math.Round((Value + Duty) * VatRate / 100m);
        public decimal CustomsFee => CalculateCustomsFee(Value);
        public decimal Total => Duty + Vat + CustomsFee;

        private static decimal CalculateCustomsFee(decimal value)
        {
            if (value <= 200_000m) return 775m;
            if (value <= 450_000m) return 1_550m;
            if (value <= 1_200_000m) return 3_100m;
            if (value <= 2_600_000m) return 8_530m;
            if (value <= 6_300_000m) return 12_000m;
            if (value <= 10_000_000m) return 15_500m;
            if (value <= 18_700_000m) return 20_000m;
            if (value <= 26_400_000m) return 26_000m;
            if (value <= 31_000_000m) return 31_000m;
            return 32_500m;
        }
    }

    public class UserInfo
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string ContactPerson { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public static class DealService
    {
        public static readonly Dictionary<string, UserInfo> Users = new()
        {
            ["ved"] = new UserInfo { Login = "ved", Password = "1234", CompanyName = "ООО «ВЭД-Сервис»", ContactPerson = "Петрова Анна Ивановна", Email = "petrova@ved-service.ru" },
            ["global"] = new UserInfo { Login = "global", Password = "1234", CompanyName = "ООО «Глобал Трейд»", ContactPerson = "Сидоров Михаил Петрович", Email = "sidorov@globaltrade.ru" },
            ["ivanov"] = new UserInfo { Login = "ivanov", Password = "1234", CompanyName = "ИП Иванов А.С.", ContactPerson = "Иванов Алексей Сергеевич", Email = "ivanov@mail.ru" },
            ["import"] = new UserInfo { Login = "import", Password = "1234", CompanyName = "АО «Импорт-Экспорт»", ContactPerson = "Кузнецова Елена Владимировна", Email = "kuznetsova@import-export.ru" },
            ["trans"] = new UserInfo { Login = "trans", Password = "1234", CompanyName = "ООО «ТрансЛогистик»", ContactPerson = "Волков Дмитрий Андреевич", Email = "volkov@translog.ru" },
            ["pacific"] = new UserInfo { Login = "pacific", Password = "1234", CompanyName = "ООО «Пасифик Карго»", ContactPerson = "Морозова Ольга Николаевна", Email = "morozova@pacific-cargo.ru" },
            ["euro"] = new UserInfo { Login = "euro", Password = "1234", CompanyName = "ООО «ЕвроИмпорт»", ContactPerson = "Белов Сергей Владимирович", Email = "belov@euroimport.ru" },
            ["asia"] = new UserInfo { Login = "asia", Password = "1234", CompanyName = "ООО «Азия Трейдинг»", ContactPerson = "Новикова Татьяна Павловна", Email = "novikova@asia-trading.ru" },
            ["cargo"] = new UserInfo { Login = "cargo", Password = "1234", CompanyName = "ООО «Карго Плюс»", ContactPerson = "Григорьев Павел Игоревич", Email = "grigoriev@cargoplus.ru" },
            ["trade"] = new UserInfo { Login = "trade", Password = "1234", CompanyName = "ООО «Трейд Мастер»", ContactPerson = "Соколова Мария Дмитриевна", Email = "sokolova@trademaster.ru" }
        };

        public static readonly List<Deal> CheckDeals = new()
        {
            new Deal { Id = "100001", Sender = "Китай, г. Шанхай", Destination = "Россия, г. Владивосток", Product = "Ноутбуки Lenovo ThinkPad", TnvedCode = "8471 30 000 0", Value = 13_800_000m, DutyRate = 5, VatRate = 22, Risk = "green", Note = "" },
            new Deal { Id = "100002", Sender = "Турция, г. Стамбул", Destination = "Россия, г. Москва", Product = "Куртки мужские", TnvedCode = "6101 30 000 0", Value = 4_140_000m, DutyRate = 10, VatRate = 22, Risk = "yellow", Note = "️ Расхождение в количестве мест на 3%. Требуется сертификат соответствия ТР ТС 017/2011." },
            new Deal { Id = "100003", Sender = "Южная Корея, г. Сеул", Destination = "Россия, г. Новосибирск", Product = "Автомобили Hyundai Tucson", TnvedCode = "8703 23 900 9", Value = 47_840_000m, DutyRate = 17, VatRate = 22, Risk = "red", Note = "🚫 Критическое расхождение инвойсной стоимости (>15%). Отсутствие СБКТС и ОТТС. Назначен таможенный досмотр." },
            new Deal { Id = "100004", Sender = "Китай, г. Циндао", Destination = "Россия, г. Санкт-Петербург", Product = "Рыба замороженная (минтай)", TnvedCode = "0303 67 000 0", Value = 7_176_000m, DutyRate = 12, VatRate = 22, Risk = "yellow", Note = "⚠️ Требуется ветеринарный сертификат. Проверить срок годности продукции." },
            new Deal { Id = "100005", Sender = "Китай, г. Шэньчжэнь", Destination = "Россия, г. Екатеринбург", Product = "Смартфоны Xiaomi", TnvedCode = "8517 13 000 0", Value = 8_464_000m, DutyRate = 5, VatRate = 22, Risk = "green", Note = "" },
            new Deal { Id = "100006", Sender = "Германия, г. Берлин", Destination = "Россия, г. Москва", Product = "Оборудование для лабораторий", TnvedCode = "9027 80 170 0", Value = 22_500_000m, DutyRate = 0, VatRate = 22, Risk = "green", Note = "" },
            new Deal { Id = "100007", Sender = "Италия, г. Милан", Destination = "Россия, г. Санкт-Петербург", Product = "Обувь кожаная женская", TnvedCode = "6403 51 000 0", Value = 5_600_000m, DutyRate = 15, VatRate = 22, Risk = "yellow", Note = "⚠️ Требуется маркировка «Честный знак». Проверить наличие DataMatrix кодов." },
            new Deal { Id = "100008", Sender = "Япония, г. Токио", Destination = "Россия, г. Владивосток", Product = "Запчасти для станков ЧПУ", TnvedCode = "8466 93 700 0", Value = 18_900_000m, DutyRate = 5, VatRate = 22, Risk = "green", Note = "" },
            new Deal { Id = "100009", Sender = "Индия, г. Дели", Destination = "Россия, г. Новороссийск", Product = "Кофе натуральный жареный", TnvedCode = "0901 21 000 0", Value = 3_200_000m, DutyRate = 10, VatRate = 22, Risk = "yellow", Note = "️ Требуется фитосанитарный сертификат. Проверить условия транспортировки." },
            new Deal { Id = "100010", Sender = "США, г. Лос-Анджелес", Destination = "Россия, г. Владивосток", Product = "Самолёт лёгкий (Cessna)", TnvedCode = "8802 20 000 0", Value = 85_000_000m, DutyRate = 0, VatRate = 22, Risk = "red", Note = " Требуется разрешение Минпромторга. Отсутствие документов о происхождении. Назначен полный досмотр." }
        };

        public static readonly List<Deal> DealsVed = new()
        {
            new Deal { Id = "200001", OwnerLogin = "ved", Sender = "Германия, г. Гамбург", Destination = "Россия, г. Хабаровск", Product = "Электронные компоненты", TnvedCode = "8542 31 000 0", Value = 5_980_000m, DutyRate = 0, VatRate = 22, Risk = "green" },
            new Deal { Id = "200002", OwnerLogin = "ved", Sender = "Китай, г. Тяньцзинь", Destination = "Россия, г. Красноярск", Product = "Мебель офисная деревянная", TnvedCode = "9403 60 100 0", Value = 16_560_000m, DutyRate = 15, VatRate = 22, Risk = "yellow" },
            new Deal { Id = "200003", OwnerLogin = "ved", Sender = "Италия, г. Милан", Destination = "Россия, г. Казань", Product = "Промышленное оборудование", TnvedCode = "8479 89 970 0", Value = 31_280_000m, DutyRate = 5, VatRate = 22, Risk = "red" }
        };

        public static readonly List<Deal> DealsGlobal = new()
        {
            new Deal { Id = "300001", OwnerLogin = "global", Sender = "Япония, г. Иокогама", Destination = "Россия, г. Владивосток", Product = "Автозапчасти Toyota", TnvedCode = "8708 99 990 0", Value = 11_500_000m, DutyRate = 5, VatRate = 22, Risk = "green" },
            new Deal { Id = "300002", OwnerLogin = "global", Sender = "Китай, г. Гуанчжоу", Destination = "Россия, г. Москва", Product = "Бытовая техника Midea", TnvedCode = "8509 40 000 0", Value = 8_188_000m, DutyRate = 10, VatRate = 22, Risk = "yellow" }
        };

        public static readonly List<Deal> DealsIvanov = new()
        {
            new Deal { Id = "400001", OwnerLogin = "ivanov", Sender = "Турция, г. Бурса", Destination = "Россия, г. Москва", Product = "Текстиль (постельное бельё)", TnvedCode = "6302 31 000 0", Value = 3_864_000m, DutyRate = 10, VatRate = 22, Risk = "yellow" },
            new Deal { Id = "400002", OwnerLogin = "ivanov", Sender = "Китай, г. Иу", Destination = "Россия, г. Новосибирск", Product = "Галантерея", TnvedCode = "4202 21 000 0", Value = 1_702_000m, DutyRate = 12, VatRate = 22, Risk = "green" }
        };

        public static readonly List<Deal> DealsImport = new()
        {
            new Deal { Id = "500001", OwnerLogin = "import", Sender = "Германия, г. Мюнхен", Destination = "Россия, г. Москва", Product = "Станки фрезерные", TnvedCode = "8459 61 000 0", Value = 44_160_000m, DutyRate = 5, VatRate = 22, Risk = "red" },
            new Deal { Id = "500002", OwnerLogin = "import", Sender = "Китай, г. Нинбо", Destination = "Россия, г. Владивосток", Product = "Металлопрокат", TnvedCode = "7208 51 000 0", Value = 19_780_000m, DutyRate = 10, VatRate = 22, Risk = "yellow" }
        };

        public static readonly List<Deal> DealsTrans = new()
        {
            new Deal { Id = "600001", OwnerLogin = "trans", Sender = "Китай, г. Шанхай", Destination = "Россия, г. Санкт-Петербург", Product = "Контейнеры грузовые", TnvedCode = "8609 00 900 0", Value = 12_400_000m, DutyRate = 5, VatRate = 22, Risk = "green" },
            new Deal { Id = "600002", OwnerLogin = "trans", Sender = "Южная Корея, г. Пусан", Destination = "Россия, г. Владивосток", Product = "Судовые двигатели", TnvedCode = "8408 10 000 0", Value = 28_900_000m, DutyRate = 0, VatRate = 22, Risk = "yellow" }
        };

        public static readonly List<Deal> DealsPacific = new()
        {
            new Deal { Id = "700001", OwnerLogin = "pacific", Sender = "Китай, г. Далянь", Destination = "Россия, г. Владивосток", Product = "Морепродукты (креветки)", TnvedCode = "0306 17 000 0", Value = 6_700_000m, DutyRate = 12, VatRate = 22, Risk = "yellow" },
            new Deal { Id = "700002", OwnerLogin = "pacific", Sender = "Вьетнам, г. Хошимин", Destination = "Россия, г. Новороссийск", Product = "Рис шлифованный", TnvedCode = "1006 30 000 0", Value = 2_100_000m, DutyRate = 5, VatRate = 10, Risk = "green" }
        };

        public static readonly List<Deal> DealsEuro = new()
        {
            new Deal { Id = "800001", OwnerLogin = "euro", Sender = "Франция, г. Париж", Destination = "Россия, г. Москва", Product = "Парфюмерия и косметика", TnvedCode = "3304 99 000 0", Value = 9_800_000m, DutyRate = 6.5m, VatRate = 22, Risk = "yellow" },
            new Deal { Id = "800002", OwnerLogin = "euro", Sender = "Испания, г. Барселона", Destination = "Россия, г. Санкт-Петербург", Product = "Оливковое масло", TnvedCode = "1509 10 100 0", Value = 4_500_000m, DutyRate = 10, VatRate = 22, Risk = "green" }
        };

        public static readonly List<Deal> DealsAsia = new()
        {
            new Deal { Id = "900001", OwnerLogin = "asia", Sender = "Китай, г. Пекин", Destination = "Россия, г. Иркутск", Product = "Строительные материалы", TnvedCode = "6907 21 000 0", Value = 15_600_000m, DutyRate = 10, VatRate = 22, Risk = "yellow" },
            new Deal { Id = "900002", OwnerLogin = "asia", Sender = "Таиланд, г. Бангкок", Destination = "Россия, г. Владивосток", Product = "Резина натуральная", TnvedCode = "4001 21 000 0", Value = 7_800_000m, DutyRate = 5, VatRate = 22, Risk = "green" }
        };

        public static readonly List<Deal> DealsCargo = new()
        {
            new Deal { Id = "110001", OwnerLogin = "cargo", Sender = "Китай, г. Ухань", Destination = "Россия, г. Екатеринбург", Product = "Медицинское оборудование", TnvedCode = "9018 90 840 0", Value = 19_200_000m, DutyRate = 0, VatRate = 22, Risk = "green" },
            new Deal { Id = "110002", OwnerLogin = "cargo", Sender = "Индия, г. Мумбаи", Destination = "Россия, г. Москва", Product = "Фармацевтические субстанции", TnvedCode = "2933 99 000 0", Value = 11_400_000m, DutyRate = 5, VatRate = 22, Risk = "yellow" }
        };

        public static readonly List<Deal> DealsTrade = new()
        {
            new Deal { Id = "120001", OwnerLogin = "trade", Sender = "Польша, г. Варшава", Destination = "Россия, г. Смоленск", Product = "Молочная продукция", TnvedCode = "0402 91 000 0", Value = 5_300_000m, DutyRate = 15, VatRate = 10, Risk = "yellow" },
            new Deal { Id = "120002", OwnerLogin = "trade", Sender = "Беларусь, г. Минск", Destination = "Россия, г. Москва", Product = "Тракторы сельскохозяйственные", TnvedCode = "8701 94 900 0", Value = 24_700_000m, DutyRate = 0, VatRate = 22, Risk = "green" }
        };

        public static UserInfo? GetUser(string login)
        {
            return Users.TryGetValue(login, out var user) ? user : null;
        }

        public static bool Authenticate(string login, string password)
        {
            return Users.TryGetValue(login, out var user) && user.Password == password;
        }

        public static List<Deal> GetUserDeals(string login)
        {
            return login switch
            {
                "ved" => DealsVed,
                "global" => DealsGlobal,
                "ivanov" => DealsIvanov,
                "import" => DealsImport,
                "trans" => DealsTrans,
                "pacific" => DealsPacific,
                "euro" => DealsEuro,
                "asia" => DealsAsia,
                "cargo" => DealsCargo,
                "trade" => DealsTrade,
                _ => new List<Deal>()
            };
        }

        public static Deal? FindById(string id)
        {
            return CheckDeals.FirstOrDefault(d => d.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public static string AddDeal(Deal deal)
        {
            string newId = (CheckDeals.Count + 100011).ToString();
            deal.Id = newId;
            deal.VatRate = 22;
            if (deal.Value > 46_000_000m) { deal.Risk = "red"; deal.Note = "Автоматически: высокая стоимость требует проверки."; }
            else if (deal.Value > 9_200_000m) { deal.Risk = "yellow"; deal.Note = "Автоматически: требуется верификация инвойса."; }
            CheckDeals.Add(deal);
            return newId;
        }
    }
}