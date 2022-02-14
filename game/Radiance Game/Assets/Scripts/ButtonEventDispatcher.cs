using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : UnityEvent<int, float>
{

}


public class PressureButton
{
    private int index;
    private bool record = false;
    private bool buttonPressed = false;
    private int tickCount = 0;
    private int maxTicks = 10;
    private float value;
    
    public PressureButton(int _index){
        index = _index;
    }

    public void HandleNewValue(float value){
        bool wasPressed = buttonPressed;
        if (value > 0.2)
        {
            buttonPressed = true;
        }
        else
        {
            buttonPressed = false;
        }

        if (buttonPressed && !wasPressed)
        {
            record = true;
        }
        else if (!buttonPressed && wasPressed)
        {
            ResetRecording();   
        }

        if (record)
        {
            if (tickCount <= maxTicks)
            {
                tickCount++;
            }
        }
    }

    public bool IsRecording(){
        return record;
    }

    public float GetValue(){
        return value;
    }

    void ResetRecording()
    {
        record = false;
        tickCount = 0;
    }

}



public class ButtonEventDispatcher : MonoBehaviour
{
    // Start is called before the first frame update
    private ButtonEvent EventButtonPressed;
    private PressureButton[] pressureButtons;

    void Start()
    {
        EventButtonPressed = new ButtonEvent();
        pressureButtons = new PressureButton[8];
        for(int i = 0; i < pressureButtons.Length; i++){
            pressureButtons[i] = new PressureButton(i);
        }
    }


    void Update()
    {
        ArrowKeyControls();
    }

    void ArrowKeyControls()
    {
        float defaultValue = 0.5f;
        if (Input.GetKey("up"))
        {
            EventButtonPressed.Invoke(0, defaultValue);
        }

        if (Input.GetKey("left"))
        {
            EventButtonPressed.Invoke(1, defaultValue);
        }

        if (Input.GetKey("right"))
        {
            EventButtonPressed.Invoke(2, defaultValue);
        }

        if (Input.GetKey("down"))
        {
            EventButtonPressed.Invoke(3, defaultValue);
        }
    }

    public void ProcessButtonInput(int index, int value)
    {
        float normalized = Mathf.Min(value / 1000f, 1.0f);
        EvaluateButtonPress(index, normalized);
    }

    void EvaluateButtonPress(int index, float value)
    {
        PressureButton targetButton = pressureButtons[index];
        targetButton.HandleNewValue(value);
        if (targetButton.IsRecording()){
            EventButtonPressed.Invoke(index, targetButton.GetValue());
            Debug.Log("Button " + index + " Pressed!");
        }
    }

    public ButtonEvent GetEvent()
    {
        return EventButtonPressed;
    }

}
