using System.IO.Ports;
using System.Linq;
using System;

namespace MagicLightingRemoteCore
{
    public class ArduinoCommunication
    {
        #region Fields

        private static string _portName;
        private const byte Enquiry = 5;
        private const byte Acknowledge = 6;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Port used for communication with Arduino.
        /// Is only set when the communication was successful.
        /// </summary>
        public static string PortName
        {
            get { return _portName; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Send the color code to the Arduino
        /// </summary>
        /// <param name="colorCode">The color code to send to the Arduino</param>
        /// <param name="portName">The port to try first. Method will try all possible ports when
        /// this name is null or empty or if sending the code on the given port was not successful.</param>
        /// <returns>true on successs, else false.</returns>
        public static bool SendColorCode(MagicColor colorCode, string portName = null)
        {
            if(colorCode == null) throw new ArgumentNullException("colorCode");

            return SendColorCode(colorCode.Code, portName);
        }

        /// <summary>
        /// Send the color code to the Arduino
        /// </summary>
        /// <param name="colorCode">The color code to send to the Arduino</param>
        /// <param name="portName">The port to try first. Method will try all possible ports when
        /// this name is null or empty or if sending the code on the given port was not successful.</param>
        /// <returns>true on success, else false.</returns>
        public static bool SendColorCode(byte colorCode, string portName = null)
        {
            // Optimization: use given port to start with
            // This can be the port last used for successful communication
            bool success = SendCommandOnPort(portName, colorCode);

            if(success == false)
            {
                // Or find the port the Arduino is connected to
                success = SendCommandOnUnkownPort(colorCode);
            }
            return success;
        }

        private static bool SendCommandOnUnkownPort(byte colorCode)
        {
            // Try all available COM-ports. Start with the highest port number.
            // On my computer, ports with the highest number are usually assigned to the Arduino.
            foreach (var portName in SerialPort.GetPortNames().OrderByDescending(p => int.Parse(p.Substring(3))))
            {
                bool result = SendCommandOnPort(portName, colorCode);
                if (result) return true;
            }
            return false;
        }

        private static bool SendCommandOnPort(string portName, byte colorCode)
        {
            if (string.IsNullOrWhiteSpace(portName)) return false;

            int readRetryCount = 3;
            while (readRetryCount > 0)
            {
                SerialPort serial = null;
                try
                {
                    serial = CreateSerialPort(portName);

                    byte[] command = FormatCommand(colorCode);
                    serial.Write(command, 0, command.Length);

                    try
                    {
                        byte receivedByte = (byte)serial.ReadByte();

                        if (receivedByte == Acknowledge)
                        {
                            // Success! Set the port name property and return
                            _portName = portName;
                            return true;
                        }
                        else
                        {
                            readRetryCount--;
                        }
                    }
                    catch
                    {
                        readRetryCount--;
                    }
                }
                catch
                {
                    // Exception during write; do not attempt a retry send using this port
                    break;  // stop while loop
                }
                finally
                {
                    if (serial != null)
                    {
                        serial.Close();
                    }
                }
            }
            return false;
        }

        private static SerialPort CreateSerialPort(string portName)
        {
            var serial = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            serial.Open();
            serial.WriteTimeout = 100;  // ms
            serial.ReadTimeout = 100;   // ms
            serial.DiscardInBuffer();
            serial.DiscardOutBuffer();
            return serial;
        }

        private static byte[] FormatCommand(byte colorCode)
        {
            return new [] { (byte)'$', (byte)colorCode.ToString("X2")[0], (byte)colorCode.ToString("X2")[1], Enquiry };
        }

        #endregion Methods
    }
}
