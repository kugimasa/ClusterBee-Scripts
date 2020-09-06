using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBee : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float maxStep = 100;
    public float interval = 2.5f;
    [SerializeField] float movedStep = 0.0f;
    bool isMoving = true;
    bool fliped = false;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Mathf.Abs(movedStep) > maxStep)
            {
                isMoving = false;
                fliped = false;
            }
            transform.Translate(0.0f, 0.0f, moveSpeed);
            movedStep += moveSpeed;
        }
        else
        {
            StartCoroutine(Flip());
        }
    }

    IEnumerator Flip()
    {
        yield return new WaitForSeconds(interval);
        if (!fliped)
        {
            movedStep = 0.0f;
            moveSpeed *= -1.0f;
            fliped = true;
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x, scale.y, scale.z * -1.0f);
            isMoving = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            StartCoroutine(Flip());
        }
    }
}
