using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public Transform muzzleSpawnPosition;

    void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, y, 0) * speed * Time.deltaTime);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(missile, missileSpawnPosition.position, Quaternion.identity);
            GameObject muzzle = Instantiate(
                GameManager.instance.muzzleFlash,
                muzzleSpawnPosition.position,
                Quaternion.identity
            );

            Destroy(muzzle, 0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(
                GameManager.instance.explosion,
                transform.position,
                Quaternion.identity
            );

            Destroy(other.gameObject);
            Destroy(gameObject);

            GameManager.instance.GameOver();
        }
    }
}
