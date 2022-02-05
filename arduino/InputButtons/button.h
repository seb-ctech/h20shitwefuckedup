#pragma once

class Button {

public:
  Button(int pin);
  void writeValue(int value);
  void sendMessage(char const* messsage);
  void readPin();
  void tick(int milliseconds);
  void handleValue();

private:

  int inputPin = 0;
  int fsrReading; 
  int tickRate = 200;
};