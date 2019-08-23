using UnityEngine;
using System.Collections;
using System;

public class CommandProcessor 
{
    public Command CurrentCommand;

    void Start()
    {
        CurrentCommand = null;
    }

    public void Execute(Command command)
    {
        if (command.IsValid())
        {
            if (CurrentCommand != null)
                CurrentCommand.DoBeforeLeaving();

            //For new Command
            CurrentCommand = command;
            command.DoBeforeEntering();
            command.Act();
        }
    }
}