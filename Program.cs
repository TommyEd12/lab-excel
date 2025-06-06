class Program
{
    static void Main()
    {
        Console.WriteLine("Начинаем работу с базой данных животных и покупателей");

        Console.Write("Создать новый лог-файл? (y/n): ");
        var input = Console.ReadLine();
        DatabaseHelper.InitLogger(input?.ToLower() == "y");

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Загрузить базу данных из Excel");
            Console.WriteLine("2 - Просмотреть таблицы");
            Console.WriteLine("3 - Удалить элемент");
            Console.WriteLine("4 - Изменить элемент");
            Console.WriteLine("5 - Добавить элемент");
            Console.WriteLine("6 - Выполнить запрос");
            Console.WriteLine("0 - Выход");

            Console.Write("Ваш выбор: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите путь к Excel файлу: ");
                    var path = Console.ReadLine();
                    DatabaseHelper.LoadDatabaseFromExcel(path ?? "");
                    break;

                case "2":
                    Console.WriteLine("Выберите таблицу: 1-Животные, 2-Покупатели, 3-Продажи");
                    var tableChoice = Console.ReadLine();
                    switch (tableChoice)
                    {
                        case "1": DatabaseHelper.ShowAnimals(); break;
                        case "2": DatabaseHelper.ShowBuyers(); break;
                        case "3": DatabaseHelper.ShowSales(); break;
                        default: Console.WriteLine("Неверный выбор."); break;
                    }
                    break;

                case "3":
                    Console.WriteLine("Выберите таблицу для удаления: 1-Животные, 2-Покупатели, 3-Продажи");
                    var deletionChoise = Console.ReadLine();
                    switch (deletionChoise)
                    {
                        case "1":
                            Console.Write("Введите Id животного для удаления: ");
                            if (int.TryParse(Console.ReadLine(), out int animalToDeleteId))
                                DatabaseHelper.DeleteAnimal(animalToDeleteId);
                            else
                                Console.WriteLine("Неверный ввод.");
                            break;
                        case "2":
                            Console.Write("Введите Id покупателя для удаления: ");
                            if (int.TryParse(Console.ReadLine(), out int buyerToDeleteId))
                                DatabaseHelper.DeleteBuyer(buyerToDeleteId);
                            else
                                Console.WriteLine("Неверный ввод.");
                            break;
                        case "3":
                            Console.Write("Введите Id животного продажи: ");
                            bool animalParsed = int.TryParse(Console.ReadLine(), out int animalIdSaleDel);
                            Console.Write("Введите Id покупателя продажи: ");
                            bool buyerParsed = int.TryParse(Console.ReadLine(), out int buyerIdSaleDel);
                            if (animalParsed && buyerParsed)
                                DatabaseHelper.DeleteSale(animalIdSaleDel, buyerIdSaleDel);
                            else
                                Console.WriteLine("Неверный ввод.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "4":
                    Console.WriteLine("Выберите таблицу для изменения: 1-Животные, 2-Покупатели, 3-Продажи");
                    var editChoice = Console.ReadLine();
                    switch (editChoice)
                    {
                        case "1":
                            Console.Write("Введите Id животного для изменения: ");
                            if (int.TryParse(Console.ReadLine(), out int animalIdEdit))
                            {
                                Console.Write("Введите новый вид: ");
                                var species = Console.ReadLine() ?? "";
                                Console.Write("Введите новую породу: ");
                                var breed = Console.ReadLine() ?? "";
                                DatabaseHelper.EditAnimal(animalIdEdit, species, breed);
                            }
                            else Console.WriteLine("Неверный ввод.");
                            break;

                        case "2":
                            Console.Write("Введите Id покупателя для изменения: ");
                            if (int.TryParse(Console.ReadLine(), out int buyerIdEdit))
                            {
                                Console.Write("Введите новое имя: ");
                                var name = Console.ReadLine() ?? "";
                                Console.Write("Введите новый возраст: ");
                                bool ageParsed = int.TryParse(Console.ReadLine(), out int age);
                                Console.Write("Введите новый адрес: ");
                                var address = Console.ReadLine() ?? "";
                                if (ageParsed)
                                    DatabaseHelper.EditBuyer(buyerIdEdit, name, age, address);
                                else
                                    Console.WriteLine("Неверный возраст.");
                            }
                            else Console.WriteLine("Неверный ввод.");
                            break;

                        case "3":
                            Console.Write("Введите Id животного продажи для изменения: ");
                            bool animalParsed = int.TryParse(Console.ReadLine(), out int animalIdSaleEdit);
                            Console.Write("Введите Id покупателя продажи для изменения: ");
                            bool buyerParsed = int.TryParse(Console.ReadLine(), out int buyerIdSaleEdit);
                            if (animalParsed && buyerParsed)
                            {
                                Console.Write("Введите новую цену: ");
                                bool priceParsed = decimal.TryParse(Console.ReadLine(), out decimal price);
                                Console.Write("Введите новую дату (гггг-мм-дд): ");
                                bool dateParsed = DateTime.TryParse(Console.ReadLine(), out DateTime date);

                                if (priceParsed && dateParsed)
                                    DatabaseHelper.EditSale(animalIdSaleEdit, buyerIdSaleEdit, price, date);
                                else
                                    Console.WriteLine("Неверный ввод данных.");
                            }
                            else Console.WriteLine("Неверный ввод.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "5":
                    Console.WriteLine("Выберите таблицу для добавления: 1-Животные, 2-Покупатели, 3-Продажи");
                    var addChoice = Console.ReadLine();
                    switch (addChoice)
                    {
                        case "1":
                            Console.Write("Введите Id животного: ");
                            if (int.TryParse(Console.ReadLine(), out int animalIdAdd))
                            {
                                Console.Write("Введите вид: ");
                                var species = Console.ReadLine() ?? "";
                                Console.Write("Введите породу: ");
                                var breed = Console.ReadLine() ?? "";
                                DatabaseHelper.AddAnimal(animalIdAdd, species, breed);
                            }
                            else Console.WriteLine("Неверный ввод.");
                            break;
                        case "2":
                            Console.Write("Введите Id покупателя: ");
                            if (int.TryParse(Console.ReadLine(), out int buyerIdAdd))
                            {
                                Console.Write("Введите имя: ");
                                var name = Console.ReadLine() ?? "";
                                Console.Write("Введите возраст: ");
                                bool ageParsed = int.TryParse(Console.ReadLine(), out int age);
                                Console.Write("Введите адрес: ");
                                var address = Console.ReadLine() ?? "";
                                if (ageParsed)
                                    DatabaseHelper.AddBuyer(buyerIdAdd, name, age, address);
                                else
                                    Console.WriteLine("Неверный возраст.");
                            }
                            else Console.WriteLine("Неверный ввод.");
                            break;
                            
                        case "3":
                            Console.Write("Введите Id животного продажи: ");
                            bool animalParsedAdd = int.TryParse(Console.ReadLine(), out int animalIdSaleAdd);
                            Console.Write("Введите Id покупателя продажи: ");
                            bool buyerParsedAdd = int.TryParse(Console.ReadLine(), out int buyerIdSaleAdd);
                            if (animalParsedAdd && buyerParsedAdd)
                            {
                                Console.Write("Введите цену: ");
                                bool priceParsed = decimal.TryParse(Console.ReadLine(), out decimal price);
                                Console.Write("Введите дату (гггг-мм-дд): ");
                                bool dateParsed = DateTime.TryParse(Console.ReadLine(), out DateTime date);
                                Console.Write("Введите итоговую цену: ");
                                bool finalPriceParsed = decimal.TryParse(Console.ReadLine(), out decimal finalPrice);

                                if (priceParsed && dateParsed && finalPriceParsed)
                                    DatabaseHelper.AddSale(animalIdSaleAdd, buyerIdSaleAdd, price, date, finalPrice);
                                else
                                    Console.WriteLine("Неверный ввод данных.");
                            }
                            else Console.WriteLine("Неверный ввод.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "6":
                    Console.WriteLine("Доступные запросы:");
                    Console.WriteLine("1 - Найти всех животных определённого вида");
                    Console.WriteLine("2 - Найти имена покупателей, купивших животных определённого вида");
                    Console.WriteLine("3 - Общая сумма продаж для каждого покупателя");
                    Console.WriteLine("4 - Средняя цена продажи для каждого вида животных");
                    Console.Write("Выберите запрос: ");
                    var queryChoice = Console.ReadLine();
                    switch (queryChoice)
                    {
                        case "1":
                            Console.Write("Введите вид животного: ");
                            var species1 = Console.ReadLine() ?? "";
                            DatabaseHelper.Query1(species1);
                            break;
                        case "2":
                            Console.Write("Введите вид животного: ");
                            var species2 = Console.ReadLine() ?? "";
                            DatabaseHelper.Query2(species2);
                            break;
                        case "3":
                            DatabaseHelper.Query3();
                            break;
                        case "4":
                            DatabaseHelper.Query4();
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "0":
                    Console.WriteLine("Выход из программы.");
                    return;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}
