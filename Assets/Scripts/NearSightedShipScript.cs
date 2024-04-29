using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NearSightedShipScript : MonoBehaviour
{

    public Rigidbody2D ShipRB2D;
    public float ImpulseForce = 3.5f;

    public static int closestIdx; 
    public int fishNetIdx = 0;
    public List<GameObject> NetOfFish;
    public List<GameObject> SeaList;
    
    void Awake()
    {
        NetOfFish = new List<GameObject>();
        FishCollectorEvents.fishRemoved.AddListener(FindClosestFish);
        FishCollectorEvents.fishPending.AddListener(TakeFishFromSeaToNet);
    }

    // Start is called before the first frame update
    void Start()
    {
        SeaList = FishSpawnNearScript.SeaOfFish;
        Debug.Log($"sea has elements (in ShipNearScript): {SeaList.Any()}, {SeaList.Count}");
        Debug.Log($"net has elements (in ShipNearScript): {NetOfFish.Any()}, {NetOfFish.Count}");

        ShipRB2D.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // on left mouse click, move ship to fish
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)
            && SeaList.Any())
        {
            FindClosestFish();
        }
    }

    //when OnMouseDown() left-click triggers, move ShipPrefab to next fish in List/queue
    public void MoveToNextFish(int idx)
    {
        Vector2 direction = new(
            SeaList[idx].transform.position.x - transform.position.x,
            SeaList[idx].transform.position.y - transform.position.y);

        direction.Normalize();
        ShipRB2D.AddForce(direction * ImpulseForce, ForceMode2D.Impulse);
    }

    public void FindClosestFish()
    {
        closestIdx = 0;

        for (int i = 1; i < SeaList.Count; ++i)
        {
            if ( Vector2.Distance(transform.position, SeaList[i].transform.position) <
                 Vector2.Distance(transform.position, SeaList[closestIdx].transform.position) )
            {
                closestIdx = i;
            }
        }

        MoveToNextFish(closestIdx);
    }

    // AddFishToNet() triggers when ShipPrefab collides w/FishPrefab, then RemoveFishFromSea()
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == SeaList[closestIdx])
        {
            ShipRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            ShipRB2D.constraints = RigidbodyConstraints2D.None;

            FishCollectorEvents.fishHit.Invoke();
        }
    }

    public void TakeFishFromSeaToNet()
    {
        NetOfFish.Add(Instantiate(SeaList[closestIdx]));
        NetOfFish[fishNetIdx].SetActive(false);
        ++fishNetIdx;
    }
}

