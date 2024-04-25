using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;


public abstract class AbstractFishScript : MonoBehaviour
{
    [SerializeField]
    protected int pointValue;

    protected abstract void OnDestroy();


    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
