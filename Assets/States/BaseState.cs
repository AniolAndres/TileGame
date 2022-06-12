using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    protected IScreenMachine screenMachine;

    public BaseState(IScreenMachine screenMachine) {
        this.screenMachine = screenMachine;
    }
}
