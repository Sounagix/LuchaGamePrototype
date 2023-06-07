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
                inputActions.KeyBoard.Back.performed -= Back;
                inputActions.KeyBoard.Disable();
                break;
            case DISPOSITIVE.GAMEPAD:
                inputActions.GamePad.Attack.performed -= AttackPlayer;
                inputActions.GamePad.Block.performed -= BlockPlayer;
                inputActions.GamePad.Block.canceled -= EndBlock;
                inputActions.GamePad.Kick.performed -= KickPlayer;
                inputActions.GamePad.Jump.performed -= Jump;
                inputActions.GamePad.Back.performed -= Back;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.JOYSTICK:
                inputActions.JoyStick.Attack.performed -= AttackPlayer;
                inputActions.JoyStick.Block.performed -= BlockPlayer;
                inputActions.JoyStick.Block.canceled -= EndBlock;
                inputActions.JoyStick.Kick.performed -= KickPlayer;
                inputActions.JoyStick.Jump.performed -= Jump;
                inputActions.JoyStick.Back.performed -= Back;
                inputActions.JoyStick.Disable();
                break;
            case DISPOSITIVE.NULL:
                break;
            case DISPOSITIVE.XBOX:
                inputActions.GamePad.Attack.performed -= AttackPlayer;
                inputActions.GamePad.Block.performed -= BlockPlayer;
                inputActions.GamePad.Block.canceled -= EndBlock;
                inputActions.GamePad.Kick.performed -= KickPlayer;
                inputActions.GamePad.Jump.performed -= Jump;
                inputActions.GamePad.Back.performed -= Back;
                inputActions.GamePad.Disable();
                break;
            case DISPOSITIVE.PS:
                inputActions.GamePad.Attack.performed -= AttackPlayer;
                inputActions.GamePad.Block.performed -= BlockPlayer;
                inputActions.GamePad.Block.canceled -= EndBlock;
                inputActions.GamePad.Kick.performed -= KickPlayer;
                inputActions.GamePad.Jump.performed -= Jump;
                inputActions.GamePad.Back.performed -= Back;
                inputActions.GamePad.Disable();
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
        var device = PeripheralsFinder.instance.GetDevice(dISPOSITIVE);
        if (device.Value.Equals(dISPOSITIVE))
        {
            SetUpDispositive(device.Key, device.Value);
        }
        else
        {
            dISPOSITIVE = DISPOSITIVE.KEYBOARD;
            var newDevice = PeripheralsFinder.instance.GetDevice(dISPOSITIVE);
            SetUpDispositive(newDevice.Key, newDevice.Value);
        }
    }

    public bool SetUpDispositive(InputDevice _inputDevice, DISPOSITIVE _dISPOSITIVE)
    {
        if (inputDevice == null)
        {
            inputActions = new PlayerController();
            switch (_dISPOSITIVE)
            {
                case DISPOSITIVE.KEYBOARD:
                    inputActions.KeyBoard.Enable();
                    moveActions = inputActions.KeyBoard.Move;
                    inputActions.KeyBoard.Attack.performed += AttackPlayer;
                    inputActions.KeyBoard.Block.performed += BlockPlayer;
                    inputActions.KeyBoard.Kick.performed += KickPlayer;
                    inputActions.KeyBoard.Jump.performed += Jump;
                    inputActions.KeyBoard.Back.performed += Back;
                    break;
                case DISPOSITIVE.GAMEPAD:
                    inputActions.GamePad.Enable();
                    moveActions = inputActions.GamePad.Move;
                    inputActions.GamePad.Attack.performed += AttackPlayer;
                    inputActions.GamePad.Kick.performed += KickPlayer;
                    inputActions.GamePad.Block.performed += BlockPlayer;
                    inputActions.GamePad.Block.canceled += EndBlock;
                    inputActions.GamePad.Jump.performed += Jump;
                    inputActions.GamePad.Back.performed += Back;
                    break;
                case DISPOSITIVE.JOYSTICK:
                    inputActions.JoyStick.Enable();
                    moveActions = inputActions.JoyStick.Move;
                    inputActions.JoyStick.Attack.performed += AttackPlayer;
                    inputActions.JoyStick.Kick.performed += KickPlayer;
                    inputActions.JoyStick.Block.canceled += EndBlock;
                    inputActions.JoyStick.Block.performed += BlockPlayer;
                    inputActions.JoyStick.Jump.performed += Jump;
                    inputActions.JoyStick.Back.performed += Back;
                    break;
                case DISPOSITIVE.NULL:
                    break;
                case DISPOSITIVE.XBOX:
                    inputActions.GamePad.Enable();
                    moveActions = inputActions.GamePad.Move;
                    inputActions.GamePad.Attack.performed += AttackPlayer;
                    inputActions.GamePad.Kick.performed += KickPlayer;
                    inputActions.GamePad.Block.performed += BlockPlayer;
                    inputActions.GamePad.Block.canceled += EndBlock;
                    inputActions.GamePad.Jump.performed += Jump;
                    inputActions.GamePad.Back.performed += Back;
                    break;
                case DISPOSITIVE.PS:
                    inputActions.GamePad.Enable();
                    moveActions = inputActions.GamePad.Move;
                    inputActions.GamePad.Attack.performed += AttackPlayer;
                    inputActions.GamePad.Kick.performed += KickPlayer;
                    inputActions.GamePad.Block.performed += BlockPlayer;
                    inputActions.GamePad.Block.canceled += EndBlock;
                    inputActions.GamePad.Jump.performed += Jump;
                    inputActions.GamePad.Back.performed += Back;
                    break;
                default:
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

    private void Back(InputAction.CallbackContext context)
    {
        GameManager.instance.LoadScene(0);
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
