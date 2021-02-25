using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public GameObject PSpawn;
    public bool usingGamePad = false;

    //Controller Inputs//
    Vector2 leftStick = Vector2.zero;
    Vector2 rightStick = Vector2.zero;
    float leftTrigger = 0.0f;
    float rightTrigger = 0.0f;

    bool buttonSouth = false;
    bool buttonWest = false;
    bool buttonEast = false;
    bool buttonNorth = false;
    bool buttonSpace = false;
    bool buttonInteract = false;
    bool buttonClose = false;

    bool mouse_left = false;
    bool mouse_right = false;

    bool dpad_up = false;
    bool dpad_right = false;
    bool dpad_left = false;
    bool dpad_down = false;

    bool leftShoulder = false;
    bool rightShoulder = false;

    bool leftStickButton = false;
    bool rightStickButton = false;
    //Controller Inputs//

    private void Start()
    {
        PossessPawn(myPawn);
    }

    private void Update()
    {
        GetInput();

        ((PlayerPawn)myPawn).SetCamPitch(rightStick.y);
        myPawn.RotatePlayer(rightStick.x);
        myPawn.Move(leftStick.x, leftStick.y);
        myPawn.Jump(buttonSpace);
        myPawn.Interact(buttonInteract);
        myPawn.Close(buttonClose);
        myPawn.Fire1(mouse_left);
        
    }

    private void GetInput()
    {
        leftStick = Vector2.zero;
        rightStick = Vector2.zero;

        if(usingGamePad)
        {
            bool gamePadConnected = GetInputGamePad();
            if (gamePadConnected)
            {
                return;
            }
        }

        GetInputKeyboardMouse();
    }

    private bool GetInputGamePad()
    {
        Gamepad gPad = Gamepad.current;

        if(gPad == null)
        {
            return false;
        }

        //TO-DO adding gamepad input here
        //
        //
        //
        //

        return true;
    }

    private void GetInputKeyboardMouse()
    {
        Keyboard KB = Keyboard.current;
        Mouse mouse = Mouse.current;

        rightStick = mouse.delta.ReadValue();
        dpad_up = KB.wKey.isPressed;
        dpad_down = KB.sKey.isPressed;
        dpad_left = KB.aKey.isPressed;
        dpad_right = KB.dKey.isPressed;
        buttonSpace = KB.spaceKey.wasReleasedThisFrame;
        buttonInteract = KB.eKey.wasReleasedThisFrame;
        buttonClose = KB.escapeKey.wasReleasedThisFrame;
        mouse_left = mouse.leftButton.wasReleasedThisFrame;

        KeyToAxis();
    }

    private void KeyToAxis()
    {
        if (dpad_up)
        {
            leftStick.y = 1;
        }
        if (dpad_down)
        {
            leftStick.y = -1;
        }
        if (dpad_right)
        {
            leftStick.x = 1;
        }
        if (dpad_left)
        {
            leftStick.x = -1;
        }
    }   


}
