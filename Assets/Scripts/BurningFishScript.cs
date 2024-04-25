using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningFishScript : AbstractFishScript
{

    public GameObject burn;

    protected override void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        FishCollectorEvents.givePoints.Invoke(pointValue);
        Instantiate(burn, transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
