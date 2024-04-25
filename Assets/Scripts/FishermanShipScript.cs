using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FishermanShipScript : MonoBehaviour
{

    public Rigidbody2D ShipRB2D;
    public float ImpulseForce = 3.5f;
    public List<GameObject> NetOfFish;

    public List<GameObject> SeaList;
    public int fishNetIdx = 0;
    public int seaIdx;


    void Awake()
    {
        NetOfFish = new List<GameObject>();

        FishCollectorEvents.fishKilled.AddListener(MoveToNextFish);
    }

    // Start is called before the first frame update
    void Start()
    {
        SeaList = FishSpawnScript.SeaOfFish;
        seaIdx = FishSpawnScript.fishSeaIdx;
        Debug.Log($"sea has elements (in ShipScript): {SeaList.Any()}, {SeaList.Count}");
        Debug.Log($"net has elements (in ShipScript): {NetOfFish.Any()}, {NetOfFish.Count}");

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
            MoveToNextFish(seaIdx);
        }
    }

    // when OnMouseDown() left-click triggers, move ShipPrefab to next fish in List/queue
    public void MoveToNextFish(int idx)
    {
        Vector2 direction = new(
            SeaList[seaIdx].transform.position.x - transform.position.x,
            SeaList[seaIdx].transform.position.y - transform.position.y);

        direction.Normalize();
        ShipRB2D.AddForce(direction * ImpulseForce, ForceMode2D.Impulse);
    }

    public void TakeFishFromSeaToNet()
    {
        NetOfFish.Add(Instantiate(SeaList[seaIdx]));
        NetOfFish[fishNetIdx].SetActive(false);
        ++fishNetIdx;
    }

    // AddFishToNet() triggers when ShipPrefab collides w/FishPrefab, then RemoveFishFromSea()
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == SeaList[seaIdx])
        {
            ShipRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            ShipRB2D.constraints = RigidbodyConstraints2D.None;

            Debug.Log($"sea has elements (in ShipScript): {SeaList.Any()}, {SeaList.Count}");
            
            StartCoroutine(TakeThenRemove());
        }
    }

    public IEnumerator TakeThenRemove()
    {
        TakeFishFromSeaToNet();
        yield return null;

        FishCollectorEvents.fishTake.Invoke(seaIdx);
        yield return null;
    }
}

