#include "button.h"
#include "Arduino.h"


Button::Button(int pin){
  inputPin = pin;
}


void Button::tick(int m){
  if(m % tickRate == 0){
    readPin();
  }
}

void Button::sendValue(int v){
  Serial.print(v);
}

void Button::sendMessage(char const* msg){
  Serial.print(msg);
}

void Button::readPin(){
 fsrReading = analogRead(inputPin);
 handleValue();
}

void Button::handleValue(){

  sendMessage("Analog reading = ");
  sendValue(fsrReading);
  if (fsrReading < 10) {
    sendMessage(" - No pressure\r\n");
  } else if (fsrReading < 200) {
    sendMessage(" - Light touch\r\n");
  } else if (fsrReading < 500) {
    sendMessage(" - Light squeeze\r\n");
  } else if (fsrReading < 800) {
    sendMessage(" - Medium squeeze\r\n");
  } else {
    sendMessage(" - Big squeeze\r\n");
  }
}