using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Player player;

    [SerializeField] float bulletSpeed = 20f;

    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<Player>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        myRigidbody.linearVelocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
