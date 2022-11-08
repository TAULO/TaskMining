using TaskMining;

namespace TaskMiningTest
{
    [TestClass]
    public class IndividualTaskTest
    {
        [TestMethod]
        public void TestGetDateTime()
        {
            var task = new IndividualTask("1667304245123", "", "", "", new IndividualTaskData("", UserInteractions.WINDOW_OPEN));
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime result = dateTime.AddSeconds(double.Parse(task.TimeStamp) / 1000);
            
            Assert.AreEqual(task.GetDateTime(), result);
        }
    }
}