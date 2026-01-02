using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    
    [Header("Loot")]
    public GameObject coinPrefab; // <--- NUOVA VARIABILE: Trascina qui la moneta!

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 1. PRIMA di distruggere il nemico, crea la moneta nella sua posizione
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, other.transform.position, Quaternion.identity);
            }

            // 2. Distruggi nemico e proiettile
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}