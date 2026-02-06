using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;

    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(
                GameManager.instance.explosion,
                transform.position,
                Quaternion.identity
            );

            GameManager.instance.AddScore(10);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
