using System;
using System.Linq;
using System.Reflection;
using MagicLightingRemoteCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicLightingRemoteCoreUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AssertThatStaticMagicColorsAreSameObject()
        {
            Assert.AreSame(MagicColors.Blue, MagicColors.Blue);
        }

        [TestMethod]
        public void AssertThatValuesOfTwoMagicColorObjectsAreEqual()
        {
            MagicColor magicColor1;
            MagicColor.TryParse("blue", out magicColor1);
            MagicColor magicColor2;
            MagicColor.TryParse("blue", out magicColor2);

            Assert.AreEqual(magicColor1, magicColor2);
        }

        [TestMethod]
        public void AssertThatTryParseCanParseCorrectColorName()
        {
            MagicColor colorCode;
            bool canParse = MagicColor.TryParse("blue", out colorCode);

            Assert.IsTrue(canParse);
        }

        [TestMethod]
        public void AssertThatTryParseCanNotParseIncorrectColorName()
        {
            MagicColor colorCode;
            bool canParse = MagicColor.TryParse("trein", out colorCode);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void AssertThatTryParseCanParseCorrectColorAsHexadecimalString()
        {
            MagicColor colorCode;
            bool canParse = MagicColor.TryParse("0x50", out colorCode);

            Assert.IsTrue(canParse);
        }

        [TestMethod]
        public void AssertThatTryParseCanNotParseIncorrectColorAsHexadecimalString()
        {
            MagicColor colorCode;
            bool canParse = MagicColor.TryParse("0x51", out colorCode);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void AssertThatTryParseCanParseCorrectColorAsIntegerString()
        {
            MagicColor colorCode;
            bool canParse = MagicColor.TryParse("80", out colorCode);

            Assert.IsTrue(canParse);
        }

        [TestMethod]
        public void AssertThatTryParseCanNotParseIncorrectColorAsIntegerString()
        {
            MagicColor colorCode;
            bool canParse = MagicColor.TryParse("81", out colorCode);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void AssertThatTryParseParsesAvailableNameAllLowerCase()
        {
            MagicColor colorCode;
            MagicColor.TryParse("blue", out colorCode);

            Assert.AreEqual(MagicColors.Blue, colorCode);
        }

        [TestMethod]
        public void AssertThatTryParseParsesAvailableNameAllUpperCase()
        {
            MagicColor colorCode;
            MagicColor.TryParse("BLUE", out colorCode);

            Assert.AreEqual(MagicColors.Blue, colorCode);
        }

        [TestMethod]
        public void AssertThatTryParseParsesColorFromHexadecimalString()
        {
            MagicColor colorCode;
            MagicColor.TryParse("0x50", out colorCode);

            Assert.AreEqual(MagicColors.Blue, colorCode);
        }

        [TestMethod]
        public void AssertThatTryParseParsesColorFromIntegerString()
        {
            MagicColor colorCode;
            MagicColor.TryParse("80", out colorCode);

            Assert.AreEqual(MagicColors.Blue, colorCode);
        }

        [TestMethod]
        public void AssertThatStaticMagicColorPropertiesHaveCorrectValue()
        {
            Type magicColors = typeof(MagicColors);
            PropertyInfo[] propertyInfos = magicColors.GetProperties();
            foreach (byte magicColorValue in Enum.GetValues(typeof(AvailableColorCode)))
            {
                var magicColorName = Enum.GetName((typeof(AvailableColorCode)), magicColorValue);

                PropertyInfo pi = propertyInfos.FirstOrDefault(mc => mc.Name == magicColorName);

                var magicColor = (MagicColor)pi.GetValue(magicColors, null);

                Assert.AreEqual(magicColorValue, magicColor.Code, string.Format("Static property {0} on class MagicColors should have value {1}, but has value {2}", magicColorName, magicColorValue, magicColor.Code));
            }
        }

        [TestMethod]
        public void AssertThatForEveryAvailableColorCodeEnumNameAStaticPropertyIsCreated()
        {
            Type magicColors = typeof(MagicColors);
            PropertyInfo[] propertyInfos = magicColors.GetProperties();
            foreach (string magicColorName in Enum.GetNames(typeof(AvailableColorCode)))
            {
                PropertyInfo pi = propertyInfos.FirstOrDefault(mc => mc.Name == magicColorName);

                Assert.IsNotNull(pi, string.Format("No (static) MagicColor property defined on class MagicColors for enum AvailableColorCode value '{0}'", magicColorName));
                Assert.AreEqual(MemberTypes.Property, pi.MemberType);
            }
        }

        [TestMethod]
        public void AssertThatMagicColorToStringContainsName()
        {
            var magicColorToString = MagicColors.Blue.ToString();
            var expectedName = "Blue";

            Assert.IsTrue(magicColorToString.Contains(expectedName));
        }

        [TestMethod]
        public void AssertThatMagicColorToStringContainsCode()
        {
            var magicColorToString = MagicColors.Blue.ToString();
            var expectedCode = ((byte)AvailableColorCode.Blue).ToString();

            Assert.IsTrue(magicColorToString.Contains(expectedCode));
        }
    }
}
