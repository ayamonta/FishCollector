using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public class ShipMove
    //{
        // on left mouse click, move ship to fish
        public void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
            // TODO:move ship to first fish
            // move ship to fishList[0]

            // remove (current first) Fish prefab from fishList (in FishSpawner)
            // add (current first) Fish prefab to fishList (in ShipScript)

            //gameObject.GetComponent<FishSpawner>().fishList.Add(PlaceHolder)
                Debug.Log("Pressed mouse left-click.");
            }
        }
        
         // add Fish prefab from fishList 
        public void CollectFish()
        {
            // OnTriggerEnter
        }

        // move to next fish in queue
        public void MoveToNextFish()
        {
        
        } 


    //}


}
