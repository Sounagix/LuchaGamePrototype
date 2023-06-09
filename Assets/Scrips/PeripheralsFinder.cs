using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public enum DISPOSITIVE : int
{
    KEYBOARD, XBOX, PS, JOYSTICK, GAMEPAD,SHARED_KEYBOARD_PLAYER1, SHARED_KEYBOARD_PLAYER2, NULL, 
}


public class PeripheralsFinder : MonoBehaviour
{
    private bool[] dispositiveUsed;

    public static PeripheralsFinder instance;

    private bool sharedKeyboardActive = false;

    private bool playerOneShared = true;

    private void OnEnable()
    {
        // Obtener la lista de dispositivos de entrada disponibles
        InputDevice[] dispositivos = InputSystem.devices.ToArray();
        dispositiveUsed = new bool[dispositivos.Length];

        foreach (var dispositive in dispositivos)
        {
            print(dispositive.description.deviceClass);
        }

        if (dispositivos.Length == 2)
        {
            sharedKeyboardActive = true;
        }

        //player1.SetUpDispositive(dispositivos[0]);
        //player2.SetUpDispositive(dispositivos[2]);
    }

    public void ResetDivice()
    {

        for (int i = 0; i < dispositiveUsed.Length; i++)
        {
            dispositiveUsed[i] = false;
        }
    }

    public KeyValuePair<InputDevice, DISPOSITIVE> GetDevice(DISPOSITIVE dISPOSITIVE)
    {

        InputDevice[] dispositivos = InputSystem.devices.ToArray();
        if (sharedKeyboardActive)
        {
            DISPOSITIVE dISPOSITIVE_SHARED = playerOneShared ? DISPOSITIVE.SHARED_KEYBOARD_PLAYER1 : DISPOSITIVE.SHARED_KEYBOARD_PLAYER2;

            if (dISPOSITIVE_SHARED.Equals(DISPOSITIVE.SHARED_KEYBOARD_PLAYER2))
                playerOneShared = true;
            else playerOneShared = false;
            return new KeyValuePair<InputDevice, DISPOSITIVE>(dispositivos[0], dISPOSITIVE_SHARED);
        }

        bool diviceFinded = false;
        int numDispositive = dispositivos.Length;
        int index = 0;
        switch (dISPOSITIVE)
        {
            case DISPOSITIVE.KEYBOARD:
                do
                {
                    if (!dispositiveUsed[index] && dispositivos[index].description.deviceClass.Contains(GetDispositiveName(DISPOSITIVE.KEYBOARD)) ||
                        dispositivos[index].description.product.Contains(GetDispositiveName(DISPOSITIVE.KEYBOARD)))
                    {
                        diviceFinded = true;
                    }
                    else index++;
                }
                while (!diviceFinded && index < numDispositive);
                break;
            case DISPOSITIVE.GAMEPAD:
                do
                {
                    if (!dispositiveUsed[index] && DiviceFinded(DISPOSITIVE.GAMEPAD, dispositivos[index]))
                    {
                        diviceFinded = true;
                    }
                    else index++;
                }
                while (!diviceFinded && index < numDispositive);
                break;
            case DISPOSITIVE.JOYSTICK:
                do
                {
                    if (!dispositiveUsed[index] && (dispositivos[index].description.deviceClass.Contains(GetDispositiveName(DISPOSITIVE.JOYSTICK)) ||
                        dispositivos[index].description.product.Contains(GetDispositiveName(DISPOSITIVE.JOYSTICK))))
                    {
                        diviceFinded = true;
                    }
                    else index++;
                }
                while (!diviceFinded && index < numDispositive);
                break;
            case DISPOSITIVE.NULL:
                break;
            case DISPOSITIVE.XBOX:
                do
                {
                    if (!dispositiveUsed[index] && DiviceFinded(DISPOSITIVE.XBOX, dispositivos[index]))
                    {
                        diviceFinded = true;
                        dISPOSITIVE = DISPOSITIVE.XBOX;
                    }
                    else index++;
                }
                while (!diviceFinded && index < numDispositive);
                break;
            case DISPOSITIVE.PS:
                do
                {
                    if (!dispositiveUsed[index] && DiviceFinded(DISPOSITIVE.PS, dispositivos[index]))
                    {
                        diviceFinded = true;
                        dISPOSITIVE = DISPOSITIVE.PS;
                    }
                    else index++;
                }
                while (!diviceFinded && index < numDispositive);
                break;
        }

        if (diviceFinded)
        {
            dispositiveUsed[index] = true;
            return new KeyValuePair<InputDevice, DISPOSITIVE>(dispositivos[index], dISPOSITIVE);
        }
        else
        {
            return new KeyValuePair<InputDevice, DISPOSITIVE>(null, DISPOSITIVE.NULL);
        }

    }

    private bool DiviceFinded(DISPOSITIVE _dISPOSITIVE, InputDevice inputDevice)
    {
        string descriptionClass = inputDevice.description.deviceClass;
        string descriptionProduct = inputDevice.description.product;
        string displayName = inputDevice.displayName;
        return descriptionClass.Contains(GetDispositiveName(_dISPOSITIVE)) ||
            descriptionProduct.Contains(GetDispositiveName(_dISPOSITIVE)) ||
            displayName.Contains(GetDispositiveName(_dISPOSITIVE));
    }


    private void Awake()
    {
        playerOneShared = true;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string GetDispositiveName(DISPOSITIVE dISPOSITIVE)
    {
        string chain = "";
        switch (dISPOSITIVE)
        {
            case DISPOSITIVE.KEYBOARD:
                chain = "Keyboard";
                break;
            case DISPOSITIVE.GAMEPAD:
                chain = "Xbox Controller";
                break;
            case DISPOSITIVE.JOYSTICK:
                chain = "PC";
                break;
            case DISPOSITIVE.NULL:
                chain = "NULL";
                break;
            case DISPOSITIVE.XBOX:
                chain = "Xbox";
                break;
            case DISPOSITIVE.PS:
                chain = "DualShock";
                break;
        }
        return chain;
    }
}
