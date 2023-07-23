using eat_not_waste_api.Enums;

namespace eat_not_waste_api.Exceptions
{
    public class OutOfStockException : ServiceException
    {
        public OutOfStockException()
            : base(ErrorCode.OutOfStock, "The item is out of stock.")
        {
        }
    }
}