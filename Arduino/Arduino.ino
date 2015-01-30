/*
 * Emulate a Magic Remote Control to programmatically send a color code
 * to a Magic Lightning RGB LED light
 * Date October 27, 2014
 * Copyright 2014 Robert Moro
 * 
 * Instructions:
 * - Go to http://github.com/shirriff/Arduino-IRremote
 * - Follow the download and installation instructions from the readme file.
 * - If exists, remove the folder 'RobotIRremote' from C:\Program Files\Arduino\libraries\ (on 64-bit system: C:\Program Files (x86)\Arduino\libraries\)
 * - Connect an IR LED to Arduino pin D3 and GND using a 100 ohm resistor.
 */
 
#include <IRremote.h>

#include "SerialCommunication.h"
#include "MagicRemoteIrCommunication.h"

SerialCommunication serialCommunication;
MagicRemoteIrCommunication magicRemoteIrCommunication;

void setup()
{
}

void loop()
{
  while(serialCommunication.byteAvailable())
  {
    serialCommunication.shiftByteReadIntoReceiveBuffer();

    if(serialCommunication.completeCommandReceived())
    {
      serialCommunication.sendAcknowledge();
      byte colorCode = serialCommunication.getColorCodeFromCommand();
      magicRemoteIrCommunication.sendColorCode(colorCode);
    }
  }

  magicRemoteIrCommunication.resendColorCode(3000);

  delay(100);  // Sleep 100 millis
}


