using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private DISPOSITIVE[] priorityInput;

    private DISPOSITIVE dISPOSITIVE;

    private PlayerController inputActions;


    private InputDevice inputDevice;



    private void OnDisable()
    {
        switch (dISPOSITIVE)
        {
            case DISPOSITIVE.KEYBOARD:
                inputActions.KeyBoard.Back.performed -= Back;
                inputActions.KeyBoard.Disable();
                break;
            case DISPOSITIVE.GAMEPAD:
                inputActions.GamePad.Back.performed -= Back;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.JOYSTICK:
                inputActions.JoyStick.Back.performed -= Back;
                inputActions.JoyStick.Disable();
                break;
            case DISPOSITIVE.NULL:
                break;
            case DISPOSITIVE.XBOX:
                inputActions.GamePad.Back.performed -= Back;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.PS:
                inputActions.GamePad.Back.performed -= Back;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.SHARED_KEYBOARD_PLAYER1:
                inputActions.KeyBoardShared.P1Back.performed -= Back;
                inputActions.KeyBoardShared.Disable();
                break;
            case DISPOSITIVE.SHARED_KEYBOARD_PLAYER2:
                inputActions.KeyBoardShared.P2Back1.performed -= Back;
                inputActions.KeyBoardShared.Disable();
                break;
        }
        inputActions.Disable();
    }

    private void Start()
    {
        bool deviceFinded = false;
        int index = 0;
        do
        {
            var device = PeripheralsFinder.instance.GetDevice(priorityInput[index], 0);
            dISPOSITIVE = device.Value;
            if (device.Key != null && device.Value.Equals(dISPOSITIVE))
            {
                SetUpDispositive(device.Key);
                deviceFinded = true;
            }
            else
            {
                index++;
            }

        }
        while (!deviceFinded && index < priorityInput.Length);
    }

    private void Back(InputAction.CallbackContext context)
    {
        GameManager.instance.LoadScene(0);
    }

    public bool SetUpDispositive(InputDevice _inputDevice)
    {
        if (inputDevice == null)
        {
            inputDevice = _inputDevice;

            inputActions = new PlayerController();
            switch (dISPOSITIVE)
            {
                case DISPOSITIVE.KEYBOARD:
                    inputActions.KeyBoard.Enable();
                    inputActions.KeyBoard.Back.performed += Back;
                    break;
                case DISPOSITIVE.GAMEPAD:
                    inputActions.GamePad.Enable();
                    inputActions.GamePad.Back.performed += Back;
                    break;
                case DISPOSITIVE.JOYSTICK:
                    inputActions.JoyStick.Enable();
                    inputActions.JoyStick.Back.performed += Back;
                    break;
                case DISPOSITIVE.NULL:
                    break;
                case DISPOSITIVE.XBOX:
                    inputActions.GamePad.Enable();
                    inputActions.GamePad.Back.performed += Back;
                    break;
                case DISPOSITIVE.PS:
                    inputActions.GamePad.Enable();
                    inputActions.GamePad.Back.performed += Back;
                    break;
                case DISPOSITIVE.SHARED_KEYBOARD_PLAYER1:
                    inputActions.KeyBoardShared.Enable();
                    inputActions.KeyBoardShared.P1Back.performed += Back;
                    break;
                case DISPOSITIVE.SHARED_KEYBOARD_PLAYER2:
                    inputActions.KeyBoardShared.Enable();
                    inputActions.KeyBoardShared.P2Back1.performed += Back;
                    break;
            }
            return true;
        }
        else
        {
            Debug.LogError("Dipositivo null");
            return false;
        }
    }
}
