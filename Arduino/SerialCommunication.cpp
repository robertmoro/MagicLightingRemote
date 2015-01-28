#include "SerialCommunication.h"

SerialCommunication::SerialCommunication()
{
  Serial.begin(9600);  // Serial connection to host computer using USB-connection
}

boolean SerialCommunication::byteAvailable()
{
  return Serial.available();
}

void SerialCommunication::shiftByteReadIntoReceiveBuffer()
{
  // Create room in the read buffer for one new byte to read
  for(int i = 3; i > 0; i--)
  {
    rxBuffer[i] = rxBuffer[i - 1];
  }

  // Read one byte in the read buffer
  rxBuffer[0] = Serial.read();   
}

// Return true if buffer contains a complete command
boolean SerialCommunication::completeCommandReceived()
{
  return rxBuffer[3] == '$' && rxBuffer[0] == enquiry;
}

void SerialCommunication::sendAcknowledge()
{
  Serial.write(acknowledge);
}

byte SerialCommunication::getColorCodeFromCommand()
{
  rxBuffer[3] = 0;
  swapColorCodeChars(rxBuffer);
  return asciiHex2Byte(&rxBuffer[1]);
}

void SerialCommunication::swapColorCodeChars(char* buffer)
{
  char c = buffer[1];
  buffer[1] = buffer[2];
  buffer[2] = c;
}

// assumes uppercase A-F
byte SerialCommunication::asciiHex2Byte(char* a)
{
  byte val = 0;

  for(int i=0; i < 2; i++)
  {
    if(a[i] <= 57)
    {
      val += (a[i]-48)*(1<<(4*(1-i)));
    }
    else
    {
      val += (a[i]-55)*(1<<(4*(1-i)));
    }
  }
  return val;
}

