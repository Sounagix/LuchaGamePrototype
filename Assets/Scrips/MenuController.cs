using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private DISPOSITIVE dISPOSITIVE;

    [SerializeField]
    private Image gameButton;

    [SerializeField]
    private Image optionsButton;

    [SerializeField]
    private Image exitButton;

    private int index = 0;

    private PlayerController inputActions;

    private InputAction moveActions;

    private InputDevice inputDevice;

    private bool buttonDown;

    private void OnDisable()
    {
        switch (dISPOSITIVE)
        {
            case DISPOSITIVE.KEYBOARD:
                inputActions.KeyBoard.Jump.performed -= ClicBoton;
                inputActions.KeyBoard.Disable();
                break;
            case DISPOSITIVE.GAMEPAD:
                inputActions.GamePad.Jump.performed -= ClicBoton;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.JOYSTICK:
                inputActions.JoyStick.Jump.performed -= ClicBoton;
                inputActions.JoyStick.Disable();
                break;
            case DISPOSITIVE.NULL:
                break;
        }
        moveActions.Disable();
        inputActions.Disable();
    }

    private void Start()
    {
        SetUpDispositive(PeripheralsFinder.instance.GetDevice(dISPOSITIVE));
        Showbutton();
    }

    private void MueveEntreBotones(Vector2 dir)
    {
        buttonDown = true;
        if (dir.y  < -0.5f)
        {
            index++;
            if (index > 2)
            {
                index = 0;
                Showbutton(2);
            }
            else
            {
                Showbutton(index - 1);
            }
        }
        else if (dir.y > 0.5f)
        {
            index--;
            if (index < 0)
            {
                index = 2;
                Showbutton(0);
            }
            else 
            {
                Showbutton(index + 1);
            }
        }
        
        Invoke(nameof(BackToTakeInput), 0.5f);
    }

    private void BackToTakeInput()
    {
        buttonDown = false;
    }

    private void ClicBoton(InputAction.CallbackContext context)
    {
        switch (index)
        {
            case 0:
                GameManager.instance.LoadScene(1);
                break;
            case 1:
                GameManager.instance.LoadScene(2);
                break;
            case 2:
                GameManager.instance.CloseApp();
                break;

        }
    }

    private void Showbutton(int anterior = -1)
    {
        if (anterior != -1)
        {
            switch (anterior)
            {
                case 0:
                    gameButton.color = Color.white;
                    break;
                case 1:
                    optionsButton.color = Color.white;
                    break;
                case 2:
                    exitButton.color = Color.white;
                    break;

            }
        }


        if (index == 0.0f)
        {
            gameButton.color = Color.red;
        }
        else if (index == 1.0f)
        {
            optionsButton.color = Color.red;
        }
        else if (index == 2.0f)
        {
            exitButton.color = Color.red;
        }
    }

    public bool SetUpDispositive(InputDevice _inputDevice)
    {
        if (inputDevice == null)
        {
            inputDevice = _inputDevice;
            InputDeviceDescription deviceDescription = _inputDevice.description;

            if (deviceDescription.deviceClass.Contains("PC") || deviceDescription.product.Contains("PC"))
            {
                dISPOSITIVE = DISPOSITIVE.JOYSTICK;
                print("JoyStick asignado al player 1");
            }
            else if (deviceDescription.deviceClass.Contains("Keyboard") || deviceDescription.product.Contains("Keyboard"))
            {
                dISPOSITIVE = DISPOSITIVE.KEYBOARD;
                print("Keyboard asignado al player 1");
            }
            else if (deviceDescription.deviceClass.Contains("Gamepad") || deviceDescription.product.Contains("Gamepad"))
            {
                dISPOSITIVE = DISPOSITIVE.GAMEPAD;
                print("Gamepad asignado al player 1");
            }
            else
            {
                print(deviceDescription.deviceClass + "NO CONTROLADO");
                dISPOSITIVE = DISPOSITIVE.NULL;
            }


            inputActions = new PlayerController();
            switch (dISPOSITIVE)
            {
                case DISPOSITIVE.KEYBOARD:
                    inputActions.KeyBoard.Enable();
                    moveActions = inputActions.KeyBoard.Move;
                    inputActions.KeyBoard.Jump.performed += ClicBoton;
                    break;
                case DISPOSITIVE.GAMEPAD:
                    inputActions.GamePad.Enable();
                    moveActions = inputActions.GamePad.Move;
                    inputActions.GamePad.Jump.performed += ClicBoton;
                    break;
                case DISPOSITIVE.JOYSTICK:
                    inputActions.JoyStick.Enable();
                    moveActions = inputActions.JoyStick.Move;
                    inputActions.JoyStick.Jump.performed += ClicBoton;
                    break;
                case DISPOSITIVE.NULL:
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

    private void FixedUpdate()
    {
        if (!buttonDown && !moveActions.ReadValue<Vector2>().Equals(Vector2.zero))
        {
            MueveEntreBotones(moveActions.ReadValue<Vector2>());
        }

        //if (moveActions.IsPressed())
        //{
        //    playerMovement.MovePlayer(moveActions.ReadValue<Vector2>());
        //}
    }
}
