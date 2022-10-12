#pragma once

class Button {

public:
  Button(int pin);
  void tick(int milliseconds);
  void send();
  void readPin();
  void handleValue();

private:

  int inputPin = 0;
  int fsrReading; 
  int tickRate = 200;
};