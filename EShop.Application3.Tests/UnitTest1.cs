namespace EShop.Application3.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var card = new Card();
            string card1 = "4539704354706391";
            var result= card.ValidateCard(card1);
            Assert.True(result);
        }


        [Fact]
        public void Test2()
        {
            var card = new Card();
            string card1 = "123456789";
            var result = card.ValidateCard(card1);
            Assert.False(result);
        }
        [Fact]
        public void Test3()
        {
            var card= new Card();
            string card1 = "1234567890123456789012345";
            var result = card.ValidateCard(card1);
            Assert.False(result);
        }
        [Theory]
        [InlineData("American Express","3497 7965 8312 797")]
        [InlineData("American Express", "345-470-784-783-010")]
        [InlineData("American Express", "378523393817437")]
        [InlineData("Visa","4024-0071-6540-1778")]
        [InlineData("Visa", "4532 2080 2150 4434")]
        [InlineData("Visa", "4532289052809181")]
        [InlineData("MasterCard","5530016454538418")]
        [InlineData("MasterCard", "5551561443896215")]
        [InlineData("MasterCard", "5131208517986691")]
        public void Test5(string card1, string card2)
        {
            var card = new Card();
            var result=card.GetCardType(card2);
            Assert.Equal(card1, result);
        }
    }
}