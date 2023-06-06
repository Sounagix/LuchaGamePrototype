using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum DISPOSITIVE : int
{
    KEYBOARD, GAMEPAD, JOYSTICK, NULL,
}


public class PeripheralsFinder : MonoBehaviour
{
    private bool[] dispositiveUsed;

    public static PeripheralsFinder instance;

    private void OnEnable()
    {
        // Obtener la lista de dispositivos de entrada disponibles
        InputDevice[] dispositivos = InputSystem.devices.ToArray();
        dispositiveUsed = new bool[dispositivos.Length];

        foreach (var dispositive in dispositivos)
        {
            print(dispositive.description.deviceClass);
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

    public InputDevice GetDevice(DISPOSITIVE dISPOSITIVE)
    {
        InputDevice[] dispositivos = InputSystem.devices.ToArray();
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
                    if (!dispositiveUsed[index] && dispositivos[index].description.deviceClass.Contains(GetDispositiveName(DISPOSITIVE.GAMEPAD)) ||
                        dispositivos[index].description.product.Contains(GetDispositiveName(DISPOSITIVE.GAMEPAD)))
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
        }

        if (diviceFinded)
        {
            dispositiveUsed[index] = true;
            return dispositivos[index];
        }
        else
        {
            return null;
        }

    }


    private void Awake()
    {
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
                chain = "Gamepad";
                break;
            case DISPOSITIVE.JOYSTICK:
                chain = "PC";
                break;
            case DISPOSITIVE.NULL:
                chain = "NULL";
                break;
        }
        return chain;
    }
}
