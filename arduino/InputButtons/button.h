#pragma once

class Button {

public:
  Button(int pin);
  void sendValue(int value);
  void sendMessage(char const* messsage);
  void readPin();
  void tick(int milliseconds);
  void handleValue();

private:

  int inputPin = 0;     // the FSR and 10K pulldown are connected to a0
  int fsrReading;     // the analog reading from the FSR resistor divider
  int tickRate = 100;
};