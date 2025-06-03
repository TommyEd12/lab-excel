using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

public static class DatabaseHelper
{
    public static List<Animal> Animals = new List<Animal>();
    public static List<Buyer> Buyers = new List<Buyer>();
    public static List<Sale> Sales = new List<Sale>();

    private static string logFilePath = "";

    public static void InitLogger(bool newFile)
    {
        logFilePath = "log.txt";
        if (newFile)
        {
            File.WriteAllText(logFilePath, $"Лог сеанса: {DateTime.Now}\n");
        }
        else
        {
            File.AppendAllText(logFilePath, $"\nНовый сеанс: {DateTime.Now}\n");
        }
    }

    public static void Log(string message)
    {
        File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
    }

    public static void LoadDatabaseFromExcel(string filePath)
    {
        Log($"Начата загрузка базы данных из файла {filePath}");
        try
        {   
           ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var animalsSheet = package.Workbook.Worksheets["Животные"];
                Animals.Clear();
                for (int row = 2; row <= animalsSheet.Dimension.End.Row; row++)
                {
                    int id = int.Parse(animalsSheet.Cells[row, 1].Text);
                    string species = animalsSheet.Cells[row, 2].Text;
                    string breed = animalsSheet.Cells[row, 3].Text;
                    Animals.Add(new Animal(id, species, breed));
                }

                var buyersSheet = package.Workbook.Worksheets["Покупатели"];
                Buyers.Clear();
                for (int row = 2; row <= buyersSheet.Dimension.End.Row; row++)
                {
                    int id = int.Parse(buyersSheet.Cells[row, 1].Text);
                    string name = buyersSheet.Cells[row, 2].Text;
                    int age = int.Parse(buyersSheet.Cells[row, 3].Text);
                    string address = buyersSheet.Cells[row, 4].Text;
                    Buyers.Add(new Buyer(id, name, age, address));
                }

                var salesSheet = package.Workbook.Worksheets["Продажи"];
                Sales.Clear();
                for (int row = 2; row <= salesSheet.Dimension.End.Row; row++)
                {
                    int animalId = int.Parse(salesSheet.Cells[row, 1].Text);
                    int buyerId = int.Parse(salesSheet.Cells[row, 2].Text);
                    decimal price = decimal.Parse(salesSheet.Cells[row, 3].Text);
                    Sales.Add(new Sale(animalId, buyerId, price, DateTime.Now));
                }
            }
            Log("База данных успешно загружена.");
        }
        catch (Exception ex)
        {
            Log($"Ошибка при загрузке базы данных: {ex.Message}");
            Console.WriteLine($"Ошибка при загрузке базы данных: {ex.Message}");
        }
    }

    public static void ShowAnimals()
    {
        Log("Просмотр таблицы Животные");
        foreach (var animal in Animals)
        {
            Console.WriteLine(animal);
        }
    }

    public static void ShowBuyers()
    {
        Log("Просмотр таблицы Покупатели");
        foreach (var buyer in Buyers)
        {
            Console.WriteLine(buyer);
        }
    }

    public static void ShowSales()
    {
        Log("Просмотр таблицы Продажи");
        foreach (var sale in Sales)
        {
            Console.WriteLine(sale);
        }
    }

    public static void DeleteAnimal(int id)
    {
        var animal = Animals.FirstOrDefault(a => a.Id == id);
        if (animal != null)
        {
            Animals.Remove(animal);
            Log($"Удалено животное с Id={id}");
            Console.WriteLine("Элемент удалён.");
        }
        else
        {
            Console.WriteLine("Животное с таким Id не найдено.");
        }
    }

    public static void DeleteBuyer(int id)
    {
        var buyer = Buyers.FirstOrDefault(b => b.Id == id);
        if (buyer != null)
        {
            Buyers.Remove(buyer);
            Log($"Удалён покупатель с Id={id}");
            Console.WriteLine("Элемент удалён.");
        }
        else
        {
            Console.WriteLine("Покупатель с таким Id не найден.");
        }
    }

    public static void DeleteSale(int animalId, int buyerId)
    {
        var sale = Sales.FirstOrDefault(s => s.AnimalId == animalId && s.BuyerId == buyerId);
        if (sale != null)
        {
            Sales.Remove(sale);
            Log($"Удалена продажа животного {animalId} покупателю {buyerId}");
            Console.WriteLine("Элемент удалён.");
        }
        else
        {
            Console.WriteLine("Такая продажа не найдена.");
        }
    }

    public static void EditAnimal(int id, string newSpecies, string newBreed)
    {
        var animal = Animals.FirstOrDefault(a => a.Id == id);
        if (animal != null)
        {
            animal.Species = newSpecies;
            animal.Breed = newBreed;
            Log($"Изменено животное с Id={id}");
            Console.WriteLine("Элемент изменён.");
        }
        else
        {
            Console.WriteLine("Животное с таким Id не найдено.");
        }
    }

    public static void EditBuyer(int id, string newName, int newAge, string newAddress)
    {
        var buyer = Buyers.FirstOrDefault(b => b.Id == id);
        if (buyer != null)
        {
            buyer.Name = newName;
            buyer.Age = newAge;
            buyer.Address = newAddress;
            Log($"Изменён покупатель с Id={id}");
            Console.WriteLine("Элемент изменён.");
        }
        else
        {
            Console.WriteLine("Покупатель с таким Id не найден.");
        }
    }

    public static void EditSale(int animalId, int buyerId, decimal newPrice, DateTime newDate, decimal newFinalPrice)
    {
        var sale = Sales.FirstOrDefault(s => s.AnimalId == animalId && s.BuyerId == buyerId);
        if (sale != null)
        {
            sale.Price = newPrice;
            sale.Date = newDate;
  
            Log($"Изменена продажа животного {animalId} покупателю {buyerId}");
            Console.WriteLine("Элемент изменён.");
        }
        else
        {
            Console.WriteLine("Такая продажа не найдена.");
        }
    }

    public static void AddAnimal(int id, string species, string breed)
    {
        if (Animals.Any(a => a.Id == id))
        {
            Console.WriteLine("Животное с таким Id уже существует.");
            return;
        }
        Animals.Add(new Animal(id, species, breed));
        Log($"Добавлено животное с Id={id}");
        Console.WriteLine("Элемент добавлен.");
    }

    public static void AddBuyer(int id, string name, int age, string address)
    {
        if (Buyers.Any(b => b.Id == id))
        {
            Console.WriteLine("Покупатель с таким Id уже существует.");
            return;
        }
        Buyers.Add(new Buyer(id, name, age, address));
        Log($"Добавлен покупатель с Id={id}");
        Console.WriteLine("Элемент добавлен.");
    }

    public static void AddSale(int animalId, int buyerId, decimal price, DateTime date, decimal finalPrice)
    {
        if (!Animals.Any(a => a.Id == animalId))
        {
            Console.WriteLine("Животное с таким Id не найдено.");
            return;
        }
        if (!Buyers.Any(b => b.Id == buyerId))
        {
            Console.WriteLine("Покупатель с таким Id не найден.");
            return;
        }
        if (Sales.Any(s => s.AnimalId == animalId && s.BuyerId == buyerId && s.Date == date))
        {
            Console.WriteLine("Такая продажа уже существует.");
            return;
        }
        Sales.Add(new Sale(animalId, buyerId, price, date));
        Log($"Добавлена продажа животного {animalId} покупателю {buyerId}");
        Console.WriteLine("Элемент добавлен.");
    }

    public static void Query1(string species)
    {
        Log($"Выполнен запрос 1: Найти всех животных вида '{species}'");
        var result = Animals.Where(a => a.Species.Equals(species, StringComparison.OrdinalIgnoreCase)).ToList();
        Console.WriteLine($"Животные вида '{species}':");
        foreach (var a in result)
            Console.WriteLine(a);
    }

    public static void Query2(string species)
    {
        Log($"Выполнен запрос 2: Найти имена покупателей, купивших животных вида '{species}'");
        var query = from s in Sales
                    join a in Animals on s.AnimalId equals a.Id
                    join b in Buyers on s.BuyerId equals b.Id
                    where a.Species.Equals(species, StringComparison.OrdinalIgnoreCase)
                    select b.Name;

        var distinctNames = query.Distinct().ToList();
        Console.WriteLine($"Покупатели, купившие животных вида '{species}':");
        foreach (var name in distinctNames)
            Console.WriteLine(name);
    }

    public static void Query3()
    {
        Log("Выполнен запрос 3: Общая сумма продаж для каждого покупателя");
        var query = from s in Sales
                    join b in Buyers on s.BuyerId equals b.Id
                    group s by s.BuyerId into g
                    join b in Buyers on g.Key equals b.Id
                    select new { BuyerName = b.Name, TotalSales = g.Sum(s => s.Price) };

        foreach (var item in query)
        {
            Console.WriteLine($"Покупатель: {item.BuyerName}, Общая сумма продаж: {item.TotalSales}");
        }
    }

    public static void Query4()
    {
        Log("Выполнен запрос 4: Средняя цена продажи для каждого вида животных");
        var query = from s in Sales
                    join a in Animals on s.AnimalId equals a.Id
                    group s by a.Species into g
                    select new { Species = g.Key, AvgPrice = g.Average(s => s.Price) };

        foreach (var item in query)
        {
            Console.WriteLine($"Вид: {item.Species}, Средняя цена: {item.AvgPrice:F2}");
        }
    }
}
