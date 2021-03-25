using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Actor
{
    public Controller control;
    public GameObject CameraControl;
    public void Possessed(Controller c)
    {
        control = c;
        
        OnPossessed();
    }

    public void OnPossessed()
    {

    }

    public void UnPossess()
    {

    }

    public virtual void RotatePlayer(float MouseX)
    {

    }
    public virtual void Move(float h, float v)
    {

    }
    public virtual void Jump(bool s)
    {
       
    }

    public virtual void Interact(bool e)
    {

    }
    public virtual void Close(bool escape)
    {

    }

    public virtual void Fire1(bool clicked, Vector2 mousePos)
    {

    }

    public virtual void ActionBar1(bool input)
    {
        
    }
    public virtual void ActionBar2(bool input)
    {

    }
    public virtual void ActionBar3(bool input)
    {

    }
    public virtual void ActionBar4(bool input)
    {

    }
    public virtual void ActionBar5(bool input)
    {

    }
    public virtual void ActionBar6(bool input)
    {

    }
    public virtual void ActionBar7(bool input)
    {

    }
    public virtual void ActionBar8(bool input)
    {

    }
    public virtual void ActionBar9(bool input)
    {

    }

    public virtual void ActionBar0(bool input)
    {

    }


}
