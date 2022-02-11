#include "button.h"

Button* b;

void setup(void) {
  Serial.begin(9600);
  b = new Button(0);   
}
 
void loop(void) {
  b->tick(millis());
}