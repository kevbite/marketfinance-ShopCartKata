using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Xunit;

namespace Kata.ShoppingCart.Tests.XUnit
{

    /*
     * Item	Price	Offer
A	50	3 for 130
B	30	2 for 45
C	20
D	15
     */
    public class CheckoutTests
    {
        private readonly Checkout _checkout;

        public CheckoutTests()
        {
            _checkout = new Checkout(new Dictionary<char, int>
            {
                ['A'] = 50,
                ['B'] = 30,
                ['C'] = 20,
                ['D'] = 15
            }, new Dictionary<char, IOffers>
            {
                ['A'] = new Offers(130, 3),
                ['B'] = new Offers(45, 2)
            });
        }
        [Fact]
        public void ShouldReturnZeroForScannedItems()
        {
            var total = _checkout.GetTotal("");
            total.Should().Be(0);
        }

        [Theory]
        [InlineData("A", 50)]
        [InlineData("B", 30)]
        [InlineData("C", 20)]
        [InlineData("D", 15)]
        public void ShouldReturnItemPriceWhenScannedOnce(string itemCode, int price)
        {
            var total = _checkout.GetTotal(itemCode);
            total.Should().Be(price);
        }

        [Theory]
        [InlineData("AA", 100)]
        [InlineData("CC", 40)]
        [InlineData("DD", 30)]
        [InlineData("ABCD", 115)]
        public void ShouldReturnCorrectPricesForMultipleItems(string itemCode, int price)
        {
            var total = _checkout.GetTotal(itemCode);
            total.Should().Be(price);
        }

        [Theory]
        [InlineData("AAA", 130)]
        [InlineData("BB", 45)]
        public void ShouldReturnCorrectPriceForSpecialOffer(string itemCodes, int totalPrice)
        {
            var total = _checkout.GetTotal(itemCodes);
            total.Should().Be(totalPrice);
        }

        [Theory]
        [InlineData("AAABB", 175)]
        [InlineData("BBBB", 90)]
        [InlineData("DAAAD", 160)]
        public void ShouldReturnCorrectPriceForSpecialOfferAndSingleItems(string itemCodes, int totalPrice)
        {
            var total = _checkout.GetTotal(itemCodes);
            total.Should().Be(totalPrice);
        }
    }
}