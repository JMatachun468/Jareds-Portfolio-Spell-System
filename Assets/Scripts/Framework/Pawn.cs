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
    public virtual void EndInteract()
    {

    }
    public virtual void Fire1(bool clicked)
    {

    }


}
