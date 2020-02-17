using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction = new Vector3(0.0f, 1.0f, 0.0f);

    [SerializeField]
    private float laserSpeed = 10.0f;

    private void MoveLaser()
    {
        transform.Translate(direction * laserSpeed * Time.deltaTime);

        if (transform.position.y >= 7.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        MoveLaser();
    }
}