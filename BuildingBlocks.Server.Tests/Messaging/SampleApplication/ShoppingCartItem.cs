﻿
namespace Kingo.BuildingBlocks.Messaging.SampleApplication
{
    public sealed class ShoppingCartItem
    {
        private readonly int _productId;
        private int _quantity;

        public ShoppingCartItem(int productId)
        {
            _productId = productId;            
        }

        public int ProductId
        {
            get { return _productId; }
        }

        public int Quantity
        {
            get { return _quantity; }
        }

        public void AddQuantity(int quantity)
        {
            _quantity += quantity;
        }
    }
}