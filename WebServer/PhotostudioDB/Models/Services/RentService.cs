using PhotostudioDB.Exceptions;

namespace PhotostudioDB.Models.Services;

public class RentService : TimedService
{
    #region Props

    public int RentedItemId { get; internal set; }
    public int Number { get; internal set; }
    public RentedItem RentedItem { get; internal set; }

    #endregion

    #region Ctors

    internal RentService()
    {
        RentedItem = new RentedItem();
        Number = 0;
    }

    public RentService(Order order, Service service, Employee employee, Status status, DateTime startDateTime,
        int duration, RentedItem rentedItem, int number) : base(order, service, employee, status, startDateTime,
        duration)
    {
        RentedItem = rentedItem;
        Number = number;
    }

    #endregion
}