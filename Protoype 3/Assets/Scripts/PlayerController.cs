using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public float jumpForce = 10;
    public float gravityModifier = 2;
    public bool isOnGround = true;

    public bool gameOver = false;
    public bool canDoubleJump = false;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip doubleJumpSound;

    public float timer = 0.0f;

    public Vector3 startRunPos;
    public float speed = 10f;
    private bool gameStarted = false;

    public bool getBool()
    {
        return this.gameStarted;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!gameStarted)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
         if (1.25 <= transform.position.x)
        {
            gameStarted = true;
            Debug.Log(gameStarted);
            playerAnim.SetFloat("Speed_f", 1.0f);
        }

        if (gameStarted)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!gameOver)
                {
                    if (isOnGround)
                    {
                        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isOnGround = false;
                        canDoubleJump = true;
                        playerAnim.SetTrigger("Jump_trig");
                        dirtParticle.Stop();
                        playerAudio.PlayOneShot(jumpSound, 1.0f);
                    }
                    else if (canDoubleJump)
                    {
                        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isOnGround = false;
                        canDoubleJump = false;
                        dirtParticle.Stop();
                        playerAudio.PlayOneShot(doubleJumpSound, 1.0f);
                    }
                }


            }


        }

        /*if (!gameOver)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        else
        {
            Debug.Log(timer);
        }*/


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        } 
    }
}
