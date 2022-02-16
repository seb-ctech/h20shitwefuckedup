/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System;
using System.Collections;

/**
 * Sample for reading using polling by yourself. In case you are fond of that.
 */
public class ButtonMessageReader : MonoBehaviour
{
    public SerialController serialController;
    private ButtonEventDispatcher buttonDispatcher;
    private int serialTicks = 0;
    // Initialization
    void Start()
    {
        buttonDispatcher = gameObject.GetComponent<ButtonEventDispatcher>();
    }

    // Executed each frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();
        if (message == null)
            return;
        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else ParseMessage(message);
    }

    private void ParseMessage(string message)
    {
        if (true){
            string[] protocol = message.Split(':');
            if (protocol.Length > 1)
            {
                int buttonIndex = Int32.Parse(protocol[0]);
                int buttonValue = Int32.Parse(protocol[1]);
                buttonDispatcher.ProcessButtonInput(buttonIndex, buttonValue);
                // Debug.Log("Button " + buttonIndex + " pressed: " + buttonValue);
            }
        }
    }

    private bool EvaluateSerialTicks(){
        serialTicks = (serialTicks + 1) % 1000;
        // Debug.Log(serialTicks);
        if (serialTicks % 1 == 0){
            return true;
        }
        return false;
    }


}
