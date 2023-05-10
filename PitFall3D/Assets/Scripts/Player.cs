using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    bool alive = true;

    public float speed = 5;
    [SerializeField] float horizontalMultiplier = 2;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    float horizontalInput;
    public float speedIncreasePerPoint = 0.1f;

    // Update is called once per frame
    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        if (transform.position.y < -5) {
            Die();
        }
    }

    private void FixedUpdate() {

        if (!alive) {
            return;
        }

        Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;

        rb.MovePosition(rb.position + forwardMovement + horizontalMovement);

    }

    public void Die() {

        alive = false;
        Invoke("Restart", 2);
    }

    void Restart() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump() {
        
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height/ 2) + 0.1f, groundMask);
        rb.AddForce(Vector3.up * jumpForce);
    }
}
