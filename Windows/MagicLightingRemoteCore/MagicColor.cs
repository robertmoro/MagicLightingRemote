using System;
using System.Globalization;

namespace MagicLightingRemoteCore
{
    public class MagicColor
    {
        #region Fields

        private readonly string _name;
        private readonly AvailableColorCode _code;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">Color code</param>
        internal MagicColor(AvailableColorCode code)
        {
            _code = code;
            _name = Enum.GetName(typeof(AvailableColorCode), code);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get the color name
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Get the color code
        /// </summary>
        public byte Code
        {
            get { return (byte)_code; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a string that represents the current MagicColor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Code);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current MagicColor object
        /// </summary>
        /// <param name="obj">the specified object</param>
        /// <returns>true when the objects are equal</returns>
        public override bool Equals(Object obj)
        {
            var magicColor = obj as MagicColor;

            return magicColor != null && Code.Equals(magicColor.Code);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        /// <summary>
        /// Try to parse the given <paramref name="value"/> as <paramref name="magicColor"/>
        /// </summary>
        /// <param name="value">The string to parse</param>
        /// <param name="magicColor">The parsed value</param>
        /// <returns>true when successfully parsed, else false</returns>
        public static bool TryParse(string value, out MagicColor magicColor)
        {
            byte byteValue;
            // Try to parse value as a hexadecimal number
            if (value.Trim().ToLower().StartsWith("0x") &&
                byte.TryParse(value.Trim().Substring(2), NumberStyles.HexNumber, null, out byteValue))
            {
                return TryParseAvailableColorCode(byteValue, out magicColor);
            }
            // Try parse value as a number
            if (byte.TryParse(value, out byteValue))
            {
                return TryParseAvailableColorCode(byteValue, out magicColor);
            }
            // Try to parse value as a name
            return TryParseAvailableColorName(value, out magicColor);
        }

        private static bool TryParseAvailableColorCode(byte value, out MagicColor magicColor)
        {
            AvailableColorCode availableColorCode;

            if ((!Enum.TryParse(value.ToString(CultureInfo.InvariantCulture), out availableColorCode)) ||
                (!Enum.IsDefined(typeof(AvailableColorCode), availableColorCode)))
            {
                magicColor = null;
                return false;
            }
            magicColor = new MagicColor(availableColorCode);
            return true;
        }

        private static bool TryParseAvailableColorName(string value, out MagicColor magicColor)
        {
            AvailableColorCode availableColorCode;
            if (Enum.TryParse(value, true/*ingnoreCase*/, out availableColorCode))
            {
                magicColor = new MagicColor(availableColorCode);
                return true;
            }
            magicColor = null;
            return false;
        }

        #endregion Methods
    }
}
