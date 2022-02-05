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

void Button::sendValue(int v){
  Serial.println(v);
}

void Button::sendMessage(char const* msg){
  Serial.println(msg);
}

void Button::readPin(){
 fsrReading = analogRead(inputPin);
 handleValue();
}

void Button::handleValue(){

  Serial.print("b");
  Serial.print(inputPin);
  Serial.print(":");
  sendValue(fsrReading);

}