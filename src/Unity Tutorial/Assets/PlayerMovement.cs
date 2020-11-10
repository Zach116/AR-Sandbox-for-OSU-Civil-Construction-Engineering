using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;    

    // Update is called once per frame
    // Use FixedUpdate when utilizing physics
    void FixedUpdate() {
        // Make sure to multiply by Time.deltaTime so its independent of frame rate
        rb.AddForce(0, 0, 500 * Time.deltaTime);
    }
}
