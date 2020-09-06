using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBeeController : MonoBehaviour
{
    public Transform OtherBeeList;
    public GameObject OtherBee;
    public int numOfBee;
    public bool initialized = false;
    public Vector2 boarderXZ = new Vector2(-370.0f, 370.0f);
    public Vector2 boarderY = new Vector2(20.0f, 370.0f);

    public void InitializeOtherBee()
    {
        for (int index = 0; index < numOfBee; index++)
        {
            GameObject otherBeeObject = Instantiate(OtherBee, OtherBeeList);
            float posX = Random.Range(boarderXZ[0], boarderXZ[1]);
            float posY = Random.Range(boarderY[0], boarderY[1]);
            float posZ = Random.Range(boarderXZ[0], boarderXZ[1]);
            float angle = Random.Range(0.0f, 360.0f);
            otherBeeObject.transform.position = new Vector3(posX, posY, posZ);
            otherBeeObject.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
        initialized = true;
    }
    public void DestroyOtherBee()
    {
        foreach (Transform child in OtherBeeList)
        {
            Destroy(child.gameObject);
        }
        initialized = false;
    }
}
