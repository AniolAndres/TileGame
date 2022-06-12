using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBase
{
    string GetId();

    void LinkViews(UiView uiView, WorldView worldView);

    void DestroyViews();

    void OnBringToFront();

    void OnSendToBack();

    void OnCreate();

    void OnDestroy();

}
