using System.Text.Json;
using System.Collections.Generic;
using NTIGPayloadAPI.Models;

namespace NTIGPayloadAPI.Service
{
    public interface IService 
    {
        Task SaveFile(Order orderInfo, string fileName);
        Task<List<Order>> ReadUserInfoFromFile(string fileName);
    }

    public class OrderService : IService
    {
        //private  string _fileName = "PayLoads.json";
        private List<Address>? _addresses = new List<Address>();
        private List<Item>? _items = new List<Item>();
        private int _maxOrderId, _maxAddressId, _maxItemId;

        public async Task SaveFile(Order orderInfo, string fileName)
        {
            List<Order> orders = await ReadUserInfoFromFile(fileName);
            constructModel(orders);

            orderInfo.Id = _maxOrderId + 1;
            orderInfo.PickupAddress.Id = _maxAddressId + 1;
            orderInfo.DeliveryAddress.Id = _maxAddressId + 2;

            for (int i = 0; i < orderInfo.Items.Count; i++)
            {
                orderInfo.Items[i].Id = _maxItemId + 1 + i;
            }

            // Add 1 to Id
            orders.Add(orderInfo);

            using (var writer = File.CreateText(fileName))
            {
                // save object to simple file in json format 
                string jsonString = JsonSerializer.Serialize(orders);
                writer.Write(jsonString);
            }
        }

        public async Task<List<Order>> ReadUserInfoFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                // Read object from simple file in json format
                string jsonString = await File.ReadAllTextAsync(fileName);
                return JsonSerializer.Deserialize<List<Order>>(jsonString) ?? new List<Order>();
            }

            return new List<Order>();
        }

        private void constructModel(List<Order> ordersParam)
        {
            foreach (Order order in ordersParam)
            {
                if (_maxOrderId < order.Id)
                {
                    _maxOrderId = order.Id;
                }
                constructAddressList(order.PickupAddress);
                constructAddressList(order.DeliveryAddress);
                constructItemList(order.Items);
            }
        }

        private void constructAddressList(Address addressParam)
        {
            if (_maxAddressId < addressParam.Id)
            {
                _maxAddressId = addressParam.Id;
            }
            _addresses.Add(addressParam);
        }

        private void constructItemList(List<Item> itemsParam)
        {
            int maxItemId = itemsParam.Max(item => item.Id);

            if (_maxItemId < maxItemId)
            {
                _maxItemId = maxItemId;
            }

            _items.AddRange(itemsParam);
        }
    }
}
