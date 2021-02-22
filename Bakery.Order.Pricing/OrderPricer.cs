using Bakery.Order.Domain;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Bakery.Order.Pricing
{
    public class OrderPricer : IOrderPricer
    {
        
        public OrderOutput CalculatePack(OrderInput oi , IProduct product)
        {
            if (oi == null || product == null) return null;
            var orderOutput = new OrderOutput();
            int number = oi.OrderQuantity;

            int packSizeS = product.packs[0].Number;
            int packSizeM = product.packs[1].Number;
            int packSizeL = product.packs.Count > 2 ? product.packs[2].Number : 0;
            int totalPackNo = 0;
            decimal totalPrice = 0.00m;
            List<PossibleAnswer> possibleAnswers = new List<PossibleAnswer>();
            string combine = string.Empty;

            int xlim = packSizeS == 0 ? 0 : number / packSizeS;
            int ylim = packSizeM == 0 ? 0 : number / packSizeM;
            int zlim = packSizeL == 0 ? 0 : number / packSizeL;
            
            //-- loop thru all possibility 
            for (int x = 0; x <= xlim; x++)
            {
                for (int y = 0; y <= ylim; y++)
                {
                    for (int z = 0; z <= zlim; z++)
                    {
                        int totalProduct = packSizeS * x + packSizeM * y + packSizeL * z;
                        //-- if total product number equals what we requested, save this combination. 
                        if (totalProduct == number && totalProduct != 0)
                        {
                            totalPackNo = x + y + z;
                            if (x > 0) combine += $"{x.ToString()} X {product.packs[0].Number} {product.packs[0].Price.ToString("C", CultureInfo.CurrentCulture)} ";
                            totalPrice += x * product.packs[0].Price;
                            if (y > 0) combine += $"{y.ToString()} X {product.packs[1].Number} {product.packs[1].Price.ToString("C", CultureInfo.CurrentCulture)} ";
                            totalPrice += y * product.packs[1].Price;
                            if (z > 0) combine += $"{z.ToString()} X {product.packs[2].Number} {product.packs[2].Price.ToString("C", CultureInfo.CurrentCulture)} ";
                            totalPrice += product.packs.Count > 2 ? z * product.packs[2].Price : 0;
                            possibleAnswers.Add(new PossibleAnswer(
                                x, y, z, totalPackNo,
                                combine,
                                totalPrice));
                            combine = string.Empty;
                            
                        }
                    }
                }
            }

            PossibleAnswer result = possibleAnswers.OrderBy(i => i.TotalPackNo).FirstOrDefault();
            orderOutput.OrderInputShow = oi;
            orderOutput.TotalPrice = result.TotalPrice.ToString("C", CultureInfo.CurrentCulture);
            orderOutput.PackCombination = result.Combination;
            return orderOutput;
        }
    }

    public class PossibleAnswer
    {
        public int PackSizeSNo { get; set; }
        public int PackSizeMNo { get; set; }
        public int PackSizeLNo { get; set; }
        public int TotalPackNo { get; set; }        
        public string Combination { get; set; }
        public decimal TotalPrice { get; set; }

        public PossibleAnswer(int sp, int mp, int lp, int totalPack, string c, decimal totalPrice)
        {
            PackSizeSNo = sp;
            PackSizeMNo = mp;
            PackSizeLNo = lp;
            TotalPackNo = totalPack;
            Combination = c;
            TotalPrice = totalPrice;
        }
    }

}
