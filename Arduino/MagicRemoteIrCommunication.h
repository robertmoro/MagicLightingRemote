#ifndef MagicRemoteIrCommunication_h
#define MagicRemoteIrCommunication_h

#include <Arduino.h>
#include <IRremote.h>
#include <IRremoteInt.h>

class MagicRemoteIrCommunication
{
private:
  const static byte rgbLedBulbAddress = 0x00;
  IRsend irsend;
  byte colorCode;
  unsigned long timeLastSend;

public:
  MagicRemoteIrCommunication();
  void sendColorCode(byte colorCode);
  void resendColorCode(int delay);

private:
  void irSendColorCode(byte colorCode);
  long createNecCommand(byte address, byte command);
  byte invertByte(byte b);
  boolean millisPassed(int delay);
};

#endif // MagicRemoteIrCommunication_h
