using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class FishSpawnNearScript : MonoBehaviour
{
    public List<GameObject> prefabList;
    public static List<GameObject> SeaOfFish;

    public GameObject RegFishPrefab;
    public GameObject BurnPrefab;
    public GameObject ExplodePrefab;
    public GameObject newFishGuy;

    void Awake()
    {
        prefabList = new List<GameObject>();
        SeaOfFish = new List<GameObject>();

        FishCollectorEvents.fishHit.AddListener(RemoveFishFromSea);
    }

    // Start is called before the first frame update
    void Start()
    {
        //fishSeaIdx = NearSightedShipScript.closestIdx;

        prefabList.Add(RegFishPrefab);
        prefabList.Add(BurnPrefab);
        prefabList.Add(ExplodePrefab);
        Debug.Log($"sea has elements (in FishNearSpawn): {SeaOfFish.Any()}, {SeaOfFish.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && ScreenUtils.WithinBoundary())
        {
            AddFishToSea();
        }
    }

    public void AddFishToSea()
    {
        // create fish prefab at world coordinate
        newFishGuy = Instantiate(prefabList[Random.Range(0, prefabList.Count)], 
                                    ScreenUtils.CursorLocation(), transform.rotation) as GameObject;
        // add fish into seaOfFish list
        SeaOfFish.Add(newFishGuy);

        Debug.Log($"spawned fish: {SeaOfFish.Last().transform.position} | " +
                    $"sea has elements: {SeaOfFish.Any()}, {SeaOfFish.Count}");
    }

    public IEnumerator RemoveFishFromSeaDummy()
    {
        yield return null;

        FishCollectorEvents.fishPending.Invoke();
        yield return null;

        Destroy(SeaOfFish[NearSightedShipScript.closestIdx]);
        yield return new WaitUntil(() => SeaOfFish[NearSightedShipScript.closestIdx] == null);

        SeaOfFish.RemoveAt(NearSightedShipScript.closestIdx);
        yield return null;

        // only move to next fish if seaOfFish list has fish
        if (SeaOfFish.Any())
        {
            FishCollectorEvents.fishRemoved.Invoke();
        }
    }

    public void RemoveFishFromSea()
    {
        StartCoroutine(RemoveFishFromSeaDummy());
    }
}
