using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicLightingRemoteCore
{
    public class MagicColors
    {
        #region Fields

        private static MagicColor _greenBlue3;
        private static MagicColor _green;
        private static MagicColor _greenBlue2;
        private static MagicColor _brightnessDown;
        private static MagicColor _greenBlue1;
        private static MagicColor _greenLight;
        private static MagicColor _pink;
        private static MagicColor _blue;
        private static MagicColor _purpleLight;
        private static MagicColor _off;
        private static MagicColor _purpleDark;
        private static MagicColor _blueRed;
        private static MagicColor _yellowLight;
        private static MagicColor _red;
        private static MagicColor _yellowMedium;
        private static MagicColor _brightnessUp;
        private static MagicColor _yellowDark;
        private static MagicColor _orange;
        private static MagicColor _smooth;
        private static MagicColor _white;
        private static MagicColor _fade;
        private static MagicColor _on;
        private static MagicColor _strobe;
        private static MagicColor _flash;

        #endregion Fields

        #region Properties

        public static MagicColor GreenBlue3 { get { return _greenBlue3 = _greenBlue3 ?? new MagicColor(AvailableColorCode.GreenBlue3); } }
        public static MagicColor Green { get { return _green = _green ?? new MagicColor(AvailableColorCode.Green); } }
        public static MagicColor GreenBlue2 { get { return _greenBlue2 = _greenBlue2 ?? new MagicColor(AvailableColorCode.GreenBlue2); } }
        public static MagicColor BrightnessDown { get { return _brightnessDown = _brightnessDown ?? new MagicColor(AvailableColorCode.BrightnessDown); } }
        public static MagicColor GreenBlue1 { get { return _greenBlue1 = _greenBlue1 ?? new MagicColor(AvailableColorCode.GreenBlue1); } }
        public static MagicColor GreenLight { get { return _greenLight = _greenLight ?? new MagicColor(AvailableColorCode.GreenLight); } }
        public static MagicColor Pink { get { return _pink = _pink ?? new MagicColor(AvailableColorCode.Pink); } }
        public static MagicColor Blue { get { return _blue = _blue ?? new MagicColor(AvailableColorCode.Blue); } }
        public static MagicColor PurpleLight { get { return _purpleLight = _purpleLight ?? new MagicColor(AvailableColorCode.PurpleLight); } }
        public static MagicColor Off { get { return _off = _off ?? new MagicColor(AvailableColorCode.Off); } }
        public static MagicColor PurpleDark { get { return _purpleDark = _purpleDark ?? new MagicColor(AvailableColorCode.PurpleDark); } }
        public static MagicColor BlueRed { get { return _blueRed = _blueRed ?? new MagicColor(AvailableColorCode.BlueRed); } }
        public static MagicColor YellowLight { get { return _yellowLight = _yellowLight ?? new MagicColor(AvailableColorCode.YellowLight); } }
        public static MagicColor Red { get { return _red = _red ?? new MagicColor(AvailableColorCode.Red); } }
        public static MagicColor YellowMedium { get { return _yellowMedium = _yellowMedium ?? new MagicColor(AvailableColorCode.YellowMedium); } }
        public static MagicColor BrightnessUp { get { return _brightnessUp = _brightnessUp ?? new MagicColor(AvailableColorCode.BrightnessUp); } }
        public static MagicColor YellowDark { get { return _yellowDark = _yellowDark ?? new MagicColor(AvailableColorCode.YellowDark); } }
        public static MagicColor Orange { get { return _orange = _orange ?? new MagicColor(AvailableColorCode.Orange); } }
        public static MagicColor Smooth { get { return _smooth = _smooth ?? new MagicColor(AvailableColorCode.Smooth); } }
        public static MagicColor White { get { return _white = _white ?? new MagicColor(AvailableColorCode.White); } }
        public static MagicColor Fade { get { return _fade = _fade ?? new MagicColor(AvailableColorCode.Fade); } }
        public static MagicColor On { get { return _on = _on ?? new MagicColor(AvailableColorCode.On); } }
        public static MagicColor Strobe { get { return _strobe = _strobe ?? new MagicColor(AvailableColorCode.Strobe); } }
        public static MagicColor Flash { get { return _flash = _flash ?? new MagicColor(AvailableColorCode.Flash); } }

        /// <summary>
        /// returns a collection containing all available magic colors
        /// </summary>
        /// <returns>Collection of all available magic colors</returns>
        public static IEnumerable<MagicColor> All
        {
            get
            {
                return Enum.GetValues(typeof(AvailableColorCode)).Cast<AvailableColorCode>().Select(cc => new MagicColor(cc));
            }
        }

        #endregion Properties
    }
}
