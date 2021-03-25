using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    public GameObject PSpawn;
    public bool usingGamePad = false;

    //Camera controls/Movement keys converted to axis
    Vector2 leftStick = Vector2.zero;
    Vector2 rightStick = Vector2.zero;
    Vector2 mousePos;

    //Interaction keys
    bool buttonSpace = false;
    bool buttonInteract = false;
    bool buttonClose = false;

    //Action Bar
    bool button1 = false;
    bool button2 = false;
    bool button3 = false;
    bool button4 = false;
    bool button5 = false;
    bool button6 = false;
    bool button7 = false;
    bool button8 = false;
    bool button9 = false;
    bool button0 = false;


    //Mouse Controls
    bool mouse_left = false;
    bool mouse_right = false;
    bool mouse_hold = false;

    //WASD
    bool buttonForward = false;
    bool buttonRight = false;
    bool buttonLeft = false;
    bool buttonBackward = false;


    private void Start()
    {
        PossessPawn(myPawn);
    }

    private void Update()
    {
        GetInput();

        //Camera
        ((PlayerPawn)myPawn).SetCamPitch(rightStick.y);
        myPawn.RotatePlayer(rightStick.x);

        //Movement
        myPawn.Move(leftStick.x, leftStick.y);
        myPawn.Jump(buttonSpace);

        //Interaction
        myPawn.Interact(buttonInteract);
        myPawn.Close(buttonClose);

        //Mouse
        myPawn.Fire1(mouse_left, mousePos);

        //Actionbar
        myPawn.ActionBar1(button1);
        myPawn.ActionBar2(button2);
        myPawn.ActionBar3(button3);
        myPawn.ActionBar4(button4);
        myPawn.ActionBar5(button5);
        myPawn.ActionBar6(button6);
        myPawn.ActionBar7(button7);
        myPawn.ActionBar8(button8);
        myPawn.ActionBar9(button9);
        myPawn.ActionBar0(button0);
    }

    private void GetInput()
    {
        leftStick = Vector2.zero;
        rightStick = Vector2.zero;

        GetInputKeyboardMouse();
    }


    private void GetInputKeyboardMouse()
    {
        Keyboard KB = Keyboard.current;
        Mouse mouse = Mouse.current;

        //Mouse
        mousePos = mouse.position.ReadValue();

        //Camera control
        rightStick = mouse.delta.ReadValue();
        mouse_hold = mouse.leftButton.isPressed;

        //Movement Keys
        buttonForward = KB.wKey.isPressed;
        buttonBackward = KB.sKey.isPressed;
        buttonLeft = KB.aKey.isPressed;
        buttonRight = KB.dKey.isPressed;
        buttonSpace = KB.spaceKey.wasReleasedThisFrame;

        //Interaction Keys
        buttonInteract = KB.eKey.wasReleasedThisFrame;
        buttonClose = KB.escapeKey.wasReleasedThisFrame;
        mouse_left = mouse.leftButton.wasReleasedThisFrame;

        //Actionbar
        button1 = KB.digit1Key.wasReleasedThisFrame;
        button2 = KB.digit2Key.wasReleasedThisFrame;
        button3 = KB.digit3Key.wasReleasedThisFrame;
        button4 = KB.digit4Key.wasReleasedThisFrame;
        button5 = KB.digit5Key.wasReleasedThisFrame;
        button6 = KB.digit6Key.wasReleasedThisFrame;
        button7 = KB.digit7Key.wasReleasedThisFrame;
        button8 = KB.digit8Key.wasReleasedThisFrame;
        button9 = KB.digit9Key.wasReleasedThisFrame;
        button0 = KB.digit0Key.wasReleasedThisFrame;




        KeyToAxis();
    }

    private void KeyToAxis() //Converts WASD to axis
    {
        if (buttonForward)
        {
            leftStick.y = 1;
        }
        if (buttonBackward)
        {
            leftStick.y = -1;
        }
        if (buttonRight)
        {
            leftStick.x = 1;
        }
        if (buttonLeft)
        {
            leftStick.x = -1;
        }
    }   


}
