using System;
namespace AnyCompany
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            try
            {
                Customer customer = CustomerRepository.Load(customerId);
                if (customer != null)
                {
                    order.CustomerId = customerId;
                    if (order.Amount == 0)
                        return false;

                    if (customer.Country == "UK")
                        order.VAT = 0.2d;
                    else
                        order.VAT = 0;

                    orderRepository.Save(order);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utils.LogErrorToDB(ex, "OrderService.PlaceOrder");
                return false;
            }

}
    }
}
