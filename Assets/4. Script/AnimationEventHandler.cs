using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action closeDoorListener;
    public void CloseDoor()
    {
        closeDoorListener?.Invoke();
    }
}
