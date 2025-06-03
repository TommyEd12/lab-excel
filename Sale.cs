public class Sale
{
    public int AnimalId { get; set; }
    public int BuyerId { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public Sale(int animalId, int buyerId, decimal price, DateTime date)
    {
        AnimalId = animalId;
        BuyerId = buyerId;
        Price = price;
        Date = date;
    }

    public override string ToString()
    {
        return $"Животное Id: {AnimalId}, Покупатель Id: {BuyerId}, Цена: {Price}, Дата: {Date.ToShortDateString()}";
    }
}