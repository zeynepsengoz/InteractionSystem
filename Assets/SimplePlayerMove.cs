using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal"); // A-D
        float z = Input.GetAxis("Vertical");   // W-S

        Vector3 dir = new Vector3(x, 0f, z);
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}