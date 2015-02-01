#ifndef SerialCommunication_h
#define SerialCommunication_h

#include <Arduino.h>

class SerialCommunication
{
private:
  const static byte enquiry = 5;
  const static byte acknowledge = 6;
  byte rxBuffer[4];

public:
  SerialCommunication();
  boolean byteAvailable();
  void shiftByteReadIntoReceiveBuffer();
  boolean completeCommandReceived();
  void sendAcknowledge();
  byte getColorCodeFromCommand();

private:
  void swapColorCodeBytes(byte* buffer);
  byte asciiHex2Byte(byte* a);
};

#endif // SerialCommunication_h
