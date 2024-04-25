using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class FishCollectorEvents
{
    public static HitEvent fishTake = new HitEvent();
    public static HitEvent fishKilled = new HitEvent();

    public static HitEvent givePoints = new HitEvent();
}

public class HitEvent : UnityEvent<int> { }