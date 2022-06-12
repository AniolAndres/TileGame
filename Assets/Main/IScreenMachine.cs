using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenMachine {
    void Init(StatesCatalog statesCatalog);

    void PresentState(IStateBase state);

    void PushState(IStateBase state);

    void PopState();

}
