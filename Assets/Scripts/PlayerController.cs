using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed = 5.0f;
  private Rigidbody playerRb;
  private GameObject focalPoint;
  public GameObject powerupIndicator;
  public bool hasPowerup;
  private float powerupStrength = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");


    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

          powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Powerup")) {
        hasPowerup = true;
        Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownRoutine());
        powerupIndicator.gameObject.SetActive(true);
      }
    }

    IEnumerator PowerupCountdownRoutine() {
      yield return new WaitForSeconds(7);
      hasPowerup = false;
      powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {
        Debug.Log("Collided with" +collision.gameObject.name + "with powerup set to " +hasPowerup);

        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
        enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
      }
    }
}