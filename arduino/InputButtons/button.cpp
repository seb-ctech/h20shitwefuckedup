#include "button.h"
#include "Arduino.h"

Button::Button(int pin){
  inputPin = pin;
}

void Button::send(){
  Serial.print(inputPin);
  Serial.print(":");
  Serial.println(fsrReading);
}

void Button::tick(int m){
  if(m % tickRate == 0){
    readPin();
  }
}

void Button::readPin(){
 fsrReading = analogRead(inputPin);
 handleValue();
}

void Button::handleValue(){
  send();
}