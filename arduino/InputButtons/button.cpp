#include "button.h"
#include "Arduino.h"


Button::Button(int pin){
  inputPin = pin;
}


void Button::tick(int m){
  if(m % tickRate == 0){
    readPin();
    sendMessage("a");
  }
}

void Button::writeValue(int v){
  Serial.write(v);
}

void Button::sendMessage(char const* msg){
  Serial.print(msg);
}

void Button::readPin(){
 fsrReading = analogRead(inputPin);
 handleValue();
}

void Button::handleValue(){
  
}