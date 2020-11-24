# Shopping Cart code kata

We’re going to see how far we can get in implementing a supermarket checkout that calculates the total price of a number of items.

In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our store, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced individually. In addition, some items are multipriced: buy 'n' of them, and they’ll cost you 'y' pounds. For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130.

The price and offer table:

|Item|Price|Offer     |
|----|-----|----------|
|A   |50   |3 for 130 |
|B   |30   |2 for 45  |
|C   |20   |          |
|D   |15   |          |

Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total price so far of 95).

## Getting Started

The interface to your checkout has been provided, you will find it when you open the solution. Here are a few tests, feel free to add any tests you feel are neeed, in any order you prefer:

``` csharp
	[DataTestMethod]
	[DataRow("A", 50)]
	[DataRow("B", 30)]
	[DataRow("C", 20)]
	[DataRow("D", 15)]
	public void Checkout_GetTotal_WhenCalledWithOneItem_ShouldReturnSingleItemPrice(string item, double price)
	{
		_sut.GetTotal(item).Should().Be(price);
	}
```

credits: adapted from Dave Thomas http://codekata.com/kata/kata09-back-to-the-checkout/