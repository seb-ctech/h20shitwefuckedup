using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : UnityEvent<int, float>
{

}

public class ButtonEventEdge : UnityEvent<int>
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

    public bool HandleNewValue(float pvalue){
        bool wasPressed = buttonPressed;
        if (pvalue > 0.2)
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
            return true;
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
            } else {
                ResetRecording();
            }
        }
        value = pvalue;
        return false;
    }

    public bool IsRecording(){
        return record;
    }

    public float GetValue(){
        Debug.Log("Button " + index + ": " + value);
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
    private ButtonEvent eBtnPressed;
    private ButtonEventEdge eBtnEdge;
    private PressureButton[] pressureButtons;
    private WaterLevel wl;

    void Start()
    {
        eBtnPressed = new ButtonEvent();
        eBtnEdge = new ButtonEventEdge();
        pressureButtons = new PressureButton[8];
        for(int i = 0; i < pressureButtons.Length; i++){
            pressureButtons[i] = new PressureButton(i);
        }
        wl = GameObject.Find("WaterTank").GetComponent<WaterLevel>();
    }


    void Update()
    {
        ArrowKeyControls();
    }

    void ArrowKeyControls()
    {
        float defaultValue = 0.1f;
        if (Input.GetKey("up"))
        {
            eBtnPressed.Invoke(0, defaultValue);
            AfterButtonPress();
        }

        if (Input.GetKeyDown("up")){
            eBtnEdge.Invoke(0);
        }

        if (Input.GetKey("left"))
        {
            eBtnPressed.Invoke(1, defaultValue);
            AfterButtonPress();
        }

        if (Input.GetKeyDown("left")){
            eBtnEdge.Invoke(1);
        }

        if (Input.GetKey("right"))
        {
            eBtnPressed.Invoke(2, defaultValue);
            AfterButtonPress();
        }

        if (Input.GetKeyDown("right")){
            eBtnEdge.Invoke(2);
        }

        if (Input.GetKey("down"))
        {
            eBtnPressed.Invoke(3, defaultValue);
            AfterButtonPress();
        }

        if (Input.GetKeyDown("down")){
            eBtnEdge.Invoke(3);
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
        bool trigger = targetButton.HandleNewValue(value);
        if (targetButton.IsRecording()){
            eBtnPressed.Invoke(index, targetButton.GetValue());
            AfterButtonPress();
        }
        if (trigger){
            eBtnEdge.Invoke(index);
            Debug.Log("Button " + index + " Pressed!");
        }
    }

    public ButtonEvent GetEvent()
    {
        return eBtnPressed;
    }

    public ButtonEventEdge GetEventEdge(){
        return eBtnEdge;
    }

    private void AfterButtonPress(){
        wl.LeakWater();
    }

}
