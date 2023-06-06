using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private DISPOSITIVE dISPOSITIVE;

    private PlayerController inputActions;

    private InputAction moveActions;

    private PlayerMovement playerMovement;

    private InputDevice inputDevice;

    private PlayerAttack playerAttack;

    private Life life;


    void OnDisable()
    {
        switch (dISPOSITIVE)
        {
            case DISPOSITIVE.KEYBOARD:
                inputActions.KeyBoard.Attack.performed -= AttackPlayer;
                inputActions.KeyBoard.Kick.performed -= KickPlayer;
                inputActions.KeyBoard.Jump.performed -= Jump;
                inputActions.KeyBoard.Block.performed -= BlockPlayer;
                inputActions.KeyBoard.Block.canceled -= EndBlock;
                inputActions.KeyBoard.Disable();
                break;
            case DISPOSITIVE.GAMEPAD:
                inputActions.GamePad.Attack.performed -= AttackPlayer;
                inputActions.GamePad.Block.performed -= BlockPlayer;
                inputActions.GamePad.Block.canceled -= EndBlock;
                inputActions.GamePad.Kick.performed -= KickPlayer;
                inputActions.GamePad.Jump.performed -= Jump;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.JOYSTICK:
                inputActions.JoyStick.Attack.performed -= AttackPlayer;
                inputActions.JoyStick.Block.performed -= BlockPlayer;
                inputActions.JoyStick.Block.canceled -= EndBlock;
                inputActions.JoyStick.Kick.performed -= KickPlayer;
                inputActions.JoyStick.Jump.performed -= Jump;
                inputActions.JoyStick.Disable();
                break;
            case DISPOSITIVE.NULL:
                break;
        }
        moveActions.Disable();
        inputActions.Disable();
    }



    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        life = GetComponent<Life>();
        SetUpDispositive(PeripheralsFinder.instance.GetDevice(dISPOSITIVE));
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
                    inputActions.KeyBoard.Attack.performed += AttackPlayer;
                    inputActions.KeyBoard.Block.performed += BlockPlayer;
                    inputActions.KeyBoard.Kick.performed += KickPlayer;
                    inputActions.KeyBoard.Jump.performed += Jump;
                    break;
                case DISPOSITIVE.GAMEPAD:
                    inputActions.GamePad.Enable();
                    moveActions = inputActions.GamePad.Move;
                    inputActions.GamePad.Attack.performed += AttackPlayer;
                    inputActions.GamePad.Kick.performed += KickPlayer;
                    inputActions.GamePad.Block.performed += BlockPlayer;
                    inputActions.GamePad.Block.canceled += EndBlock;
                    inputActions.GamePad.Jump.performed += Jump;
                    break;
                case DISPOSITIVE.JOYSTICK:
                    inputActions.JoyStick.Enable();
                    moveActions = inputActions.JoyStick.Move;
                    inputActions.JoyStick.Attack.performed += AttackPlayer;
                    inputActions.JoyStick.Kick.performed += KickPlayer;
                    inputActions.JoyStick.Block.canceled += EndBlock;
                    inputActions.JoyStick.Block.performed += BlockPlayer;
                    inputActions.JoyStick.Jump.performed += Jump;
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
        if (!life.IsDeath()) 
            playerMovement.MovePlayer(moveActions.ReadValue<Vector2>());
        //if (moveActions.IsPressed())
        //{
        //    playerMovement.MovePlayer(moveActions.ReadValue<Vector2>());
        //}
    }

    private void AttackPlayer(InputAction.CallbackContext context)
    {
        if (!life.IsDeath())
            playerAttack.AttackAnim();
    }

    private void KickPlayer(InputAction.CallbackContext context)
    {
        if (!life.IsDeath())
        {
            playerAttack.KickAnim();
        }
    }

    private void BlockPlayer(InputAction.CallbackContext context)
    {
        if (!life.IsDeath())
        {
            playerAttack.InitBlock();
        }
    }

    private void EndBlock(InputAction.CallbackContext context)
    {
        if (!life.IsDeath())
        {
            playerAttack.EndBlock();
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!life.IsDeath())
            playerMovement.InitJumpAnimEvent();
    }
}
