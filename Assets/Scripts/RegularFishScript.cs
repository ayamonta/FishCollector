using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularFishScript : AbstractFishScript 
{

    protected override void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        FishCollectorEvents.givePoints.Invoke(pointValue);
    }

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 10;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
