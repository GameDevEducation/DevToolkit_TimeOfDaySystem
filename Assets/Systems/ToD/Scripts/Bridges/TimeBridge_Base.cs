using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeBridge_Base : MonoBehaviour
{
    public abstract void OnTick(float CurrentTime);
}
