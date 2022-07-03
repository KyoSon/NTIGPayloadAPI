using NUnit.Framework;
using NTIGPayloadAPI.Service;
using NTIGPayloadAPI.Models;
using NTIGPayloadAPINUnitTest.Fixtures;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NTIGPayloadAPINUnitTest
{
    public class PayLoadServiceUnitTest
    {
        private OrderService _orderService;
        private string _fileName = "PayLoads.json";
        [SetUp]
        public void Setup()
        {
            _orderService = new OrderService();
        }

        [Test]
        public async Task Test_SaveFile_CreateNewFile()
        {
            OrderFixture orderFixture = new OrderFixture();
            Order order = orderFixture.GetOrderInfo();

            await _orderService.SaveFile(order, "PayLoadsNotExist.json");
            Assert.That(new FileInfo("PayLoadsNotExist.json"), Does.Exist);
        }

        [Test]
        public async Task Test_SaveFile_AddNewOrder()
        {
            OrderFixture orderFixture = new OrderFixture();
            Order order = orderFixture.GetOrderInfo();

            List<Order> orders = await _orderService.ReadUserInfoFromFile(_fileName);
            int orderCount = orders.Count >0 ? orders.Count : 0;

            await _orderService.SaveFile(order, _fileName);

            int expectedCount = orderCount + 1;

            Assert.That(expectedCount, Is.EqualTo(orderCount + 1));
        }

        [Test]
        public async Task Test_SaveFile_AddOneToOrderId()
        {
            OrderFixture orderFixture = new OrderFixture();
            Order order = orderFixture.GetOrderInfo();

            int maxOrderId = 0;
            List<Order> orders = await _orderService.ReadUserInfoFromFile(_fileName);
            if (orders.Count > 0)
            {
                foreach (Order o in orders)
                {
                    maxOrderId = maxOrderId > o.Id ? maxOrderId : o.Id + 1;
                }
            }

            await _orderService.SaveFile(order, _fileName);

            int expectedCount =order.Id;

            Assert.That(expectedCount, Is.EqualTo(maxOrderId));
        }

        [Test]
        public async Task Test_SaveFile_AddOneToAddressId()
        {
            OrderFixture orderFixture = new OrderFixture();
            Order order = orderFixture.GetOrderInfo();

            int maxAddressId = 0;
            List<Order> orders = await _orderService.ReadUserInfoFromFile(_fileName);
            if (orders.Count > 0)
            {
                foreach (Order o in orders)
                {
                    maxAddressId = maxAddressId > o.PickupAddress.Id ? maxAddressId : o.PickupAddress.Id + 1;
                    maxAddressId = maxAddressId > o.DeliveryAddress.Id ? maxAddressId : o.DeliveryAddress.Id + 1;
                }
            }

            await _orderService.SaveFile(order, _fileName);

            int expectedPickupAddressId = order.PickupAddress.Id;
            int expectedDeliveryAddressId = order.DeliveryAddress.Id;

            Assert.That(expectedPickupAddressId, Is.EqualTo(maxAddressId));
            Assert.That(expectedDeliveryAddressId, Is.EqualTo(maxAddressId + 1));
        }

        [Test]
        public async Task Test_SaveFile_AddOneToItemId()
        {
            OrderFixture orderFixture = new OrderFixture();
            Order order = orderFixture.GetOrderInfo();

            int maxItemId = 0;
            List<Order> orders = await _orderService.ReadUserInfoFromFile(_fileName);
            if (orders.Count > 0)
            {
                foreach (Order o in orders)
                {
                    foreach(Item i in o.Items)
                    {
                        maxItemId = maxItemId > i.Id ? maxItemId : i.Id + 1;
                    }                    
                }
            }

            await _orderService.SaveFile(order, _fileName);

            int expectedItemId1 = order.Items[0].Id;
            int expectedItemId2 = order.Items[1].Id;

            Assert.That(expectedItemId1, Is.EqualTo(maxItemId));
            Assert.That(expectedItemId2, Is.EqualTo(maxItemId + 1));
        }

        [Test]
        public async Task Test_ReadUserInfoFromFile_NoFile()
        {
            await _orderService.ReadUserInfoFromFile("PayLoadsNotExist.json");
            int expectedCount = 0;

            Assert.That(expectedCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_ReadUserInfoFromFile_FileExist()
        {
            List<Order> orders = await _orderService.ReadUserInfoFromFile(_fileName);
            int expectedCount = orders.Count;

            Assert.That(expectedCount, Is.GreaterThan(0));
        }
    }
}