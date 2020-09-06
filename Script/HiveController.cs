using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveController : MonoBehaviour
{
    public GameObject Hive;
    public List<Transform> hivePosition = new List<Transform>();

    public void InitializeHiveSpot()
    {
        int index = Random.Range(0, hivePosition.Count);
        Hive.transform.position = hivePosition[index].position;
    }

}
