using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProp : MonoBehaviour
{

    [SerializeField] GameObject mmm;

    public void spawnMildlyMetallicMan(){
        Instantiate(mmm, (transform.position - new Vector3(0, .25f, 0)), transform.rotation);
    }

}
