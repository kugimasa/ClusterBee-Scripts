using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHunter : MonoBehaviour
{
    public Vector2 boarderXZ = new Vector2(-300.0f, 300.0f);
    public float boaderY = 10.3f;
    public float speed = 1.5f;
    public AudioClip NetSE;
    private AudioSource audioSource;
    private Animator animator;
    private Vector3 initialPosition;
    void Start()
    {
            float posX = Random.Range(boarderXZ[0], boarderXZ[1]);
            float posY = boaderY;
            float posZ = Random.Range(boarderXZ[0], boarderXZ[1]);
            transform.position = new Vector3(posX, posY, posZ);
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
    }
    public void Move()
    {
        if (InputController.S_KEY)
        {
            transform.Translate(speed, 0.0f, 0.0f);
        }
        if (InputController.W_KEY)
        {
            transform.Translate(-speed, 0.0f, 0.0f);
        }
        if (InputController.D_KEY)
        {
            transform.Translate(0.0f, 0.0f, speed);
        }
        if (InputController.A_KEY)
        {
            transform.Translate(0.0f, 0.0f, -speed);
        }
        if (InputController.SPACE_KEY_UP)
        {
            StartCoroutine(PlayAnimation());
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

    private IEnumerator PlayAnimation()
    {
        animator.SetBool("Net", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Net", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OtherBee")
        {
            audioSource.PlayOneShot(NetSE);
            Destroy(other.gameObject);
            BeeHunterGameController.gameScore++;
            BeeHunterGameController.hunted = true;
        }
    }
}
