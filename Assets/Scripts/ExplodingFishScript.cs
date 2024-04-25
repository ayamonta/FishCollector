using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodingFishScript : AbstractFishScript
{
    
    // animation when fish explodes when destroyed
    public GameObject explode;

    protected override void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        FishCollectorEvents.givePoints.Invoke(pointValue);
        Instantiate(explode, transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
