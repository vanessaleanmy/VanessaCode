using Xunit;
using Bakery.Order.Domain;
using Bakery.Order.Pricing;

namespace Bakery.Order.Test.XUnit
{
    public class OrderPricerTest
    {
        IOrderPricer _orderPricer;        

        [Theory]
        [InlineData(10, "VS5", "$17.98", "2 X 5 $8.99 ")]
        [InlineData(14, "MB11", "$53.80", "2 X 2 $9.95 2 X 5 $16.95 ")]
        [InlineData(13, "CF", "$25.85", "1 X 3 $5.95 2 X 5 $9.95 ")]
        public void CalculatePack_Test(int orderQuantity, string productCode, string ttlPrice, string combinationPack)
        {
            //Arrange            
            OrderInput orderInput = new OrderInput(orderQuantity, productCode);
            IProduct product = new Vegemite(); 
            switch (productCode) {
                case "VS5":
                    product = new Vegemite();
                    break;
                case "MB11":
                    product = new BlueBerryMuffin();
                    break;
                case "CF":
                    product = new Croissant();
                    break;
            }
            OrderOutput orderOutputExpected = new OrderOutput();
            orderOutputExpected.OrderInputShow = orderInput;
            orderOutputExpected.TotalPrice = ttlPrice;
            orderOutputExpected.PackCombination = combinationPack;
            _orderPricer = new OrderPricer();
            
            //Act
            var _result = _orderPricer.CalculatePack(orderInput, product);
            string resultTotalPrice = _result.TotalPrice;

            //Assert
            Assert.NotNull(_result);
            Assert.Equal(orderOutputExpected.OrderInputShow, _result.OrderInputShow);            
            Assert.Equal(ttlPrice, resultTotalPrice);
            Assert.Equal(orderOutputExpected.PackCombination, _result.PackCombination);
        }

        [Fact]
        public void CalculatePack_NoOrderOrProduct_NegativeTest()
        {
            //Arrange            
            OrderInput orderInput = null;  
            IProduct product = null;
            _orderPricer = new OrderPricer();

            //Act
            var _result = _orderPricer.CalculatePack(orderInput, product);            

            //Assert
            Assert.Null(_result);            
        }


    }
}

