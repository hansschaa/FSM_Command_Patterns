using UnityEngine;

public abstract class Command 
{
    protected PlayerInputHandler playerInputHandler;

    public Command(PlayerInputHandler playerInputHandler)
    {
        this.playerInputHandler = playerInputHandler;
    }

    public abstract bool IsValid();
    public abstract void Act();
    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }

}