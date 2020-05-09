using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 60;
    public bool hasPowerup = false;
    private float powerUpStrength = 7;
    public bool mouseInput = false;
    public GameObject powerUpIndicator;
    public Vector3 jump;
    public float jumpForce = 4;
    public bool isGrounded;
  

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput;
        if (Input.GetKeyDown(KeyCode.C)){
            mouseInput = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            mouseInput = false;
        }

        if (mouseInput)
        {
            forwardInput = Input.GetAxis("Mouse Y");
        }
        else
        {
            forwardInput = Input.GetAxis("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            //asi se llama a la corutina
            StartCoroutine(PowerupCountDownRoutine());
        }
    }
    //para que el powerup dure 7 segundos es una corutina no una funcion
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerUpIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigibody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigibody.AddForce(awayFromPlayer * powerUpStrength,ForceMode.Impulse);
            Debug.Log("Player collided with: "+ collision.gameObject.name + " with powerup set to: " + hasPowerup);
        }
    }
}
