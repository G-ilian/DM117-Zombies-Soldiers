using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angleSpeed;
    [SerializeField] float ClipLength = 1f;
    [SerializeField] AudioSource AudioClip;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPivot;
    [SerializeField] bool shot;
    [SerializeField] HPComponent hpComponent;

    float moveX, moveZ;
    Rigidbody rigidBody;
    Animator animator;

    bool onGround;
    bool isJumping;
    bool isShooting;

    int currentHealth;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        AudioClip.Stop();
        currentHealth = 100;

    }
    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if (moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isShooting");
            isShooting = true;
            StartCoroutine(ShootGun());

        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (!onGround) return;
        rigidBody.velocity = transform.forward
        * moveZ
        * speed;

        rigidBody.MoveRotation(rigidBody.rotation *
            Quaternion.Euler(0f, angleSpeed * moveX, 0f));

        if (isJumping)
        {
            rigidBody.AddForce(Vector3.up * speed, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("ZombieHands"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            isJumping = false;
        }
    }

    IEnumerator ShootGun() {
        Instantiate(bullet, bulletPivot.transform.position, transform.localRotation);
        AudioClip.Play();
        yield return new WaitForSeconds(ClipLength);
        isShooting = false;
    }

    public void TakeDamage()
    {
        currentHealth -= 5;
        Debug.Log(currentHealth);
        hpComponent.TakeDamage(5);
        if (currentHealth <= 0)
        {
            animator.SetTrigger("died");
        }
    }
}
