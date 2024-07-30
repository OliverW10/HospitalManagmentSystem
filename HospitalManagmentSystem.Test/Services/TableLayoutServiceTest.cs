using HospitalManagmentSystem.Services.Implementations;

namespace HospitalManagmentSystem.Test.Services
{
    internal class TableLayoutServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            _tableLayoutService = new TableLayoutService();
        }

        TableLayoutService _tableLayoutService;

        [Test]
        public void TestGetTableWidths()
        {
            var columnNames = new string[] { "asdf", "asdfadsf", "a" };
            var data = new string[][] { new[] { "asdf", "xyz!", "a" } };

            for (int i = 1; i < 100; i++)
            {
                var widths = _tableLayoutService.GetColumnWidths(data, columnNames, i);
                Assert.That(i, Is.EqualTo(widths.Sum()));
            }
        }

        [Test]
        public void TestPadToWidthTooShort()
        {
            var tableLayoutService = new TableLayoutService();
            var result = tableLayoutService.RightPadToWidth("12", 5);
            Assert.That(result, Is.EqualTo(" 12  "));
        }

        [Test]
        public void TestPadToWidthTooLong()
        {
            var result = _tableLayoutService.RightPadToWidth("12345678", 5);
            Assert.That(result, Is.EqualTo("12..."));
        }
    }
}
