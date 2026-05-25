using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    public Vector3 moveInputVelocity = Vector3.zero;
    public Vector3 lookInputVelocity = Vector3.zero;
    public float moveSpeed = 10.0f;
    
    public GameObject topAxis;
    public GameObject cannonAxis;

    public GameObject bulletPrefab;
    public GameObject shotPoint;

    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        //move.x = moveInputVelocity.x;
        move.z = moveInputVelocity.y;

        Vector3 bodyTorque = Vector3.zero;
        bodyTorque.y = moveInputVelocity.x;
        
        transform.Translate(move * moveSpeed * Time.deltaTime);
        transform.Rotate(bodyTorque * Time.deltaTime * 90);

        // === タンク上部の回転 ===
        Vector3 topTorque = Vector3.zero;
        topTorque.y = lookInputVelocity.x;
        topAxis.transform.Rotate(topTorque * Time.deltaTime * 90);

        // === タンク砲の回転 ===
        Vector3 cannonTorque = Vector3.zero;
        cannonTorque.x = lookInputVelocity.y * -1;
        cannonAxis.transform.Rotate(cannonTorque * Time.deltaTime * 90);
    }

    void OnMove(InputValue value)
    {
        Debug.Log($"move value is {value.Get()}");
        moveInputVelocity = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        Debug.Log($"look value if { value.Get()}");
        lookInputVelocity = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        Debug.Log($"attack value if { value.Get()}");

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotPoint.transform.forward * 25, ForceMode.Impulse);
    }
}
