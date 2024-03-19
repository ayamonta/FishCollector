using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject FishObject;
    public List<GameObject> fishList;
    // Start is called before the first frame update
    void Start()
    {
        new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AddFish();
            Debug.Log("Pressed mouse right-click.");
        }
    }
    


    //public class FishSpawn{
        public void AddFish()
        {
            // find location in world coordinate
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
            // create fish prefab at world coordinate
            GameObject fish = Instantiate<GameObject>(FishObject);
            fish.transform.position = worldPosition;

            // add fish into [uncollected] list
            fishList = new List<GameObject>();
            fishList.Add(fish);
        }
    //}



}
