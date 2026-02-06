using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;

    private bool isDead = false;
    private SpriteRenderer sprite;
    private AudioSource audioSource;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile") && !isDead)
        {
            isDead = true;

            GetComponent<Collider2D>().enabled = false;
            sprite.enabled = false;

            
            audioSource.Play();

            GameManager.instance.AddScore(10);

            Destroy(other.gameObject);
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
