using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata.ShoppingCart
{
    public class Checkout : ICheckout
    {
        private readonly IReadOnlyDictionary<char, int> _priceList;
        private readonly IReadOnlyDictionary<char, IOffers> _offers;

        public Checkout(IReadOnlyDictionary<char, int> priceList, IReadOnlyDictionary<char, IOffers> offers)
        {
            _priceList = priceList;
            _offers = offers;
        }

        public class ItemCodeCounts
        {
            public char ItemCode { get; }
            public int Count { get; set; }

            public ItemCodeCounts(char itemCode, int count)
            {
                ItemCode = itemCode;
                Count = count;
            }
        }

        public double GetTotal(string items)
        {
            double total = 0;

            var array = items.GroupBy(x => x)
                .Select(x => new ItemCodeCounts(x.Key, x.Count()))
                .ToArray();


            foreach (var item in array)
            {
                total += GetMultiPriceTotal(item);
                total += GetSinglePriceTotal(item);
            }

            return total;
        }

        private double GetMultiPriceTotal(ItemCodeCounts item)
        {
            if (_offers.TryGetValue(item.ItemCode, out var offer))
            {
                return offer.Apply(item, offer);
            }

            return 0;
        }

        private double GetSinglePriceTotal(ItemCodeCounts item)
        {
            if (_priceList.TryGetValue(item.ItemCode, out var v))
            {
                return v * item.Count;
            }

            return 0;
        }
    }

    public interface IOffers
    {
        double Apply(Checkout.ItemCodeCounts item, IOffers offer);
    }

    public class Offers : IOffers
    {
        public Offers(int discountPrice, int itemCount)
        {
            ItemCount = itemCount;
            DiscountPrice = discountPrice;
        }

        public int DiscountPrice { get; }
        public int ItemCount { get; }

        public double Apply(Checkout.ItemCodeCounts item, IOffers offer)
        {
            return Apply(item, (Offers) offer);
        }
        public double Apply(Checkout.ItemCodeCounts item, Offers offer)
        {
            var offerCount = item.Count / offer.ItemCount;
            var itemsLeft = item.Count % offer.ItemCount;

            item.Count = itemsLeft;
            return offer.DiscountPrice * offerCount;
        }
    };
}