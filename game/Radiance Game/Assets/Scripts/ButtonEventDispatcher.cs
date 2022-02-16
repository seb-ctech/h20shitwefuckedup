using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    private int maxTicks = 20;
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
            Debug.Log("Recording...");
            if (tickCount <= maxTicks)
            {
                tickCount++;
                Debug.Log("Ticks: " + tickCount);
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
        if(Input.GetKeyDown("space")){
            RestartScene();
        }
    }

    //TODO: Refactor to own component
    private void RestartScene(){
        // string currentScene = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(currentScene);
        wl.Reset();
    }

    void ArrowKeyControls()
    {
        float defaultValue = 0.6f;
        
        AssignButtonEventByKey("up", 0, defaultValue);
        AssignButtonEventByKey("left", 1, defaultValue);
        AssignButtonEventByKey("down", 2, defaultValue);
        AssignButtonEventByKey("right", 3, defaultValue);
    }

    private void AssignButtonEventByKey(string key, int index, float defaultValue){
        if (Input.GetKey(key))
        {   
            if (Time.frameCount % 4 == 0){
                pressureButtons[index].HandleNewValue(defaultValue);
                if(pressureButtons[index].IsRecording()){
                    eBtnPressed.Invoke(index, defaultValue);
                    Debug.Log("Key pressed: " + key);
                }
            }
        }

        if (Input.GetKeyDown(key)){
            eBtnEdge.Invoke(index);
        }

        if (Input.GetKeyUp(key)){
            pressureButtons[index].HandleNewValue(0.0f);
            AfterButtonPress();
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
