using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTIGPayloadAPI.Models;

namespace NTIGPayloadAPINUnitTest.Fixtures
{
    internal class OrderFixture
    {
        internal Order GetOrderInfo()
        {
            Order order = new Order();

            order.Id = 1;
            order.RequestedPickupTime = DateTime.Now;
            order.PickupAddress = SetPickupAddressInfo();
            order.DeliveryAddress = SetDeliveryAddressInfo();
            order.Items = setItemsInfo();
            order.PickupInstructions = "Ensure driver signs in before heading to the loading bay";
            order.DeliveryInstructions = "Items are fragile, take extra care when unloading";

            return order;
        }

        private Address SetPickupAddressInfo()
        {
            Address address = new Address();
            address.Id = 1;
            address.Street = "Happy Valley Road";
            address.City = "Springfield";
            address.Unit = "14";
            address.Suburb = "Sunshine Place";
            address.Postcode = "1023";
            
            return address;
        }

        private Address SetDeliveryAddressInfo()
        {
            Address address = new Address();
            address.Id = 2;
            address.Street = "Over the hill street";
            address.City = "Shelbyville";
            address.Unit = "66";
            address.Suburb = "Mountaintop Place";
            address.Postcode = "2013";

            return address;
        }

        private List<Item> setItemsInfo()
        {
            List<Item> items = new List<Item> {
                new Item() {
                    Id = 1,
                    ItemCode = "AMZ-01",
                    Quantity = 20
                },
                new Item() {
                    Id = 2,
                    ItemCode = "XYZ-02",
                    Quantity = 5
                }
            };

            return items;
        }


    }
}
