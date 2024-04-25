using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class FishSpawnScript : MonoBehaviour
{
    public List<GameObject> prefabList;
    public static List<GameObject> SeaOfFish;

    public GameObject RegFishPrefab;
    public GameObject BurnPrefab;
    public GameObject ExplodePrefab;
    public GameObject newFishGuy;

    public static int fishSeaIdx = 0;


    void Awake()
    {
        prefabList = new List<GameObject>();
        SeaOfFish = new List<GameObject>();

        FishCollectorEvents.fishTake.AddListener(RemoveFishFromSea);
    }

    // Start is called before the first frame update
    void Start()
    {
        prefabList.Add(RegFishPrefab);
        prefabList.Add(BurnPrefab);
        prefabList.Add(ExplodePrefab);
        Debug.Log($"sea has elements (in FishSpawn): {SeaOfFish.Any()}, {SeaOfFish.Count}");
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

        Destroy(SeaOfFish[fishSeaIdx]);
        yield return new WaitUntil(() => SeaOfFish[fishSeaIdx] == null);

        SeaOfFish.RemoveAt(fishSeaIdx);
        yield return null;

        // only move to next fish if seaOfFish list has fish
        if (SeaOfFish.Any())
        {
            FishCollectorEvents.fishKilled.Invoke(fishSeaIdx);
        }
    }

    public void RemoveFishFromSea(int idx)
    {
        StartCoroutine(RemoveFishFromSeaDummy());
    }
}
