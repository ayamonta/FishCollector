using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class FishCollectorEvents
{
    public static HitEvent fishTake = new HitEvent();
    public static HitEvent fishKilled = new HitEvent();

    public static HitEvent givePoints = new HitEvent();


    // for nearSighted ship/fish
    public static UnityEvent fishHit = new UnityEvent();
    //public static UnityEvent fishReceived = new UnityEvent();
    public static UnityEvent fishRemoved = new UnityEvent();
    public static UnityEvent fishPending = new UnityEvent();
    
}

public class HitEvent : UnityEvent<int> { }