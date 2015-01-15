using System;

namespace MasterData.Core.Domain.Products
{
    public class ProductStock
    {
        public int Number { get; protected set; }
        public DateTime Date { get; protected set; }

        protected ProductStock()
        {
        }

        public ProductStock(int number, DateTime date)
        {
            Number = number;
            Date = date;
        }
    }
}