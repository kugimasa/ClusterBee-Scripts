using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float initialSpeed = 1.5f;
    public float speedUpStep = 0.2f;
    public float rotationSpeed = 2.0f;
    public Transform rotationLeftOrigin;
    public Transform rotationRightOrigin;
    public Transform Hive;
    public GameObject Cluster;
    public GameObject Pointer;
    public GameObject HiveImage;
    public Transform BeeOrigin;
    public Transform ClusterOrigin;
    public AudioClip ClusterSE;
    private Transform origin;
    private AudioSource audioSource;
    private CapsuleCollider capsuleCollider;
    float speed;
    int numOfCluster = 0;
    float setClusterSide = 1.0f;

    void Start()
    {
        origin = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (GameController.gamePlaying)
        {
            ClusterCollected();
        }
    }

    public void Initialize()
    {
        origin.position = BeeOrigin.position;
        DestroyClusterBee();
        Hive = GameObject.Find("Hive").transform;
        Pointer.SetActive(false);
        HiveImage.SetActive(false);
        speed = initialSpeed;
        numOfCluster = 0;
        setClusterSide = 1.0f;
    }
    public void MoveHorizontally()
    {
        // Rotate
        if (InputController.A_KEY && InputController.SPACE)
        {
            origin.RotateAround(rotationLeftOrigin.position,
                                   new Vector3(0.0f, 1.0f, 0.0f),
                                   -rotationSpeed);
        }
        if (InputController.D_KEY && InputController.SPACE)
        {
            origin.RotateAround(rotationRightOrigin.position,
                                   new Vector3(0.0f, 1.0f, 0.0f),
                                   rotationSpeed);
        }

        origin.Translate(0.0f, 0.0f, speed);
        if (InputController.A_KEY)
        {
            origin.Translate(-speed, 0.0f, 0.0f);
        }
        if (InputController.D_KEY)
        {
            origin.Translate(speed, 0.0f, 0.0f);
        }
    }

    public void MoveVertically()
    {
        if (InputController.W_KEY)
        {
            origin.Translate(0.0f, speed, 0.0f);
        }
        if (InputController.S_KEY)
        {
            origin.Translate(0.0f, -speed, 0.0f);
        }
    }

    private void AddCluster()
    {
        if (GameController.remainingBeeNum > 0)
        {
            numOfCluster++;
            audioSource.PlayOneShot(ClusterSE);
            GameObject ClusterObject = Instantiate(Cluster, ClusterOrigin);
            ClusterObject.transform.localPosition = PositionCluster(numOfCluster);
            setClusterSide *= -1.0f;
            speed += speedUpStep;
            GameController.remainingChanged = true;
        }
    }
    public void StartBuzzing()
    {
        audioSource.Play();
    }
    public void StopBuzzing()
    {
        audioSource.Stop();
    }

    private void ClusterCollected()
    {
        if (GameController.remainingBeeNum == 0)
        {
            speed = 2.5f;
            HiveImage.SetActive(true);
            Pointer.SetActive(true);
            Pointer.transform.rotation = Quaternion.LookRotation(Hive.position - Pointer.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hive")
        {
            if (GameController.remainingBeeNum == 0)
            {
                GameController.gameOver = true;
            }
            else
            {
                // Alert
            }
        }
        if (other.gameObject.tag == "OtherBee")
        {
            Destroy(other.gameObject);
            AddCluster();
        }
    }

    public void DestroyClusterBee()
    {
        foreach (Transform child in ClusterOrigin)
        {
            Destroy(child.gameObject);
        }
    }

    private Vector3 PositionCluster(int num)
    {
        switch (num)
        {
            case 1:
                return new Vector3(1, 0, 0);
            case 2:
                return new Vector3(-1, 0, 0);
            case 3:
                return new Vector3(0, 0, -1);
            case 4:
                capsuleCollider.height = 4;
                return new Vector3(0, 1, 0);
            case 5:
                capsuleCollider.height = 6;
                return new Vector3(0, -1.5f, 0);
            case 6:
                capsuleCollider.radius = 3;
                return new Vector3(2, 0, 0);
            case 7:
                return new Vector3(-2, 0, 0);
            case 8:
                return new Vector3(1, 0, -1);
            case 9:
                return new Vector3(-1, 0, -1);
            case 10:
                return new Vector3(0, 0, -2);
            default:
                return new Vector3(0, 0, 0);
        }
    }

}
