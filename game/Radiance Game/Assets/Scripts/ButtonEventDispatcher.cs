using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : UnityEvent<float>
{

}

public class ButtonEventDispatcher : MonoBehaviour
{
    // Start is called before the first frame update
    private float[] buttonValues;
    private float mainButton;
    private int tickCount;
    private bool record;
    private bool buttonPressed;
    private int maxTicks = 10;
    private float accForce;
    private ButtonEvent EventButtonPressed;
    void Start()
    {
        buttonValues = new float[8];
        record = false;
        EventButtonPressed = new ButtonEvent();
    }

    // Update is called once per frame


    public void ProcessButtonInput(int index, int value){
        float normalize = Mathf.Min(value / 1000f, 1.0f);
        // Debug.Log("Button " + index + " value is: " + normalize);
        buttonValues[index] = normalize;
        mainButton = normalize;
        EvaluateButtonPress(mainButton);
    }

    void EvaluateButtonPress(float value){
        bool wasPressed = buttonPressed;
        if (value > 0.2){
            buttonPressed = true;
        } else {
            buttonPressed = false;
        }

        if (buttonPressed && !wasPressed){
            Debug.Log("Button Pressed!");
            record = true;
        } else if (!buttonPressed && wasPressed) {
            EmitButtonPress();
        }

        if(record){
            if(tickCount <= maxTicks){
                accForce += value;
                tickCount++;
            } else {
                EmitButtonPress();
            }
        }
    }

    void EmitButtonPress(){
        float force = tickCount > 0 ? accForce / tickCount : 0.0f;
        EventButtonPressed.Invoke(force);
        ResetButtonEvaluation();
        Debug.Log("Button Released " + force);
    }

    void ResetButtonEvaluation(){
        record = false;
        accForce = 0.0f;
        tickCount = 0;
    }

    public ButtonEvent GetEvent(){
        return EventButtonPressed;
    }

}
