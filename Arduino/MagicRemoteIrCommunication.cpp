#include <Arduino.h>
#include <IRremote.h>
#include <IRremoteInt.h>

#include "MagicRemoteIrCommunication.h"

MagicRemoteIrCommunication::MagicRemoteIrCommunication()
{
  timeLastSend = 0;
}

void MagicRemoteIrCommunication::sendColorCode(byte colorCode)
{
  timeLastSend = millis();
  irSendColorCode(colorCode);
}

void MagicRemoteIrCommunication::resendColorCode(int delay)
{
  if(millisPassed(delay))
  {
    irSendColorCode(colorCode);
  }
}

void MagicRemoteIrCommunication::irSendColorCode(byte colorCode)
{
  if(colorCode != 0)
  {
    irsend.sendNEC(createNecCommand(rgbLedBulbAddress, colorCode), NEC_BITS);
  }
}

long MagicRemoteIrCommunication::createNecCommand(byte address, byte command)
{
  long result = address;
  result <<= 8;
  result |= invertByte(address);
  result <<= 8;
  result |= command;
  result <<= 8;
  result |= invertByte(command);

  return result;
}

byte MagicRemoteIrCommunication::invertByte(byte b)
{
  return b ^ 0xFF;
}

boolean MagicRemoteIrCommunication::millisPassed(int delay)
{
  unsigned long now = millis();
  if(now < timeLastSend)  // overflow occured
  {
    timeLastSend = 0;
  }
  if(now > timeLastSend + delay)
  {
    timeLastSend = now;
    return true;
  }
  return false;
}

