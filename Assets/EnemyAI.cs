using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;

    // Salviamo i riferimenti ai due possibili bersagli
    private Transform playerTransform;
    private Transform villageTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. Trova il Player
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTransform = p.transform;

        // 2. Trova il Villaggio
        GameObject v = GameObject.FindGameObjectWithTag("Village");
        if (v != null) villageTransform = v.transform;
    }

    void FixedUpdate()
    {
        // Chiediamo al cervello: "Chi devo inseguire adesso?"
        Transform currentTarget = GetClosestTarget();

        if (currentTarget != null)
        {
            // Muoviti verso il bersaglio scelto
            Vector2 direction = (currentTarget.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    // Funzione intelligente che calcola le distanze
    Transform GetClosestTarget()
    {
        // Se non esiste nessuno dei due, stai fermo
        if (playerTransform == null && villageTransform == null) return null;
        
        // Se uno dei due manca (es. Player morto), vai verso l'altro
        if (playerTransform == null) return villageTransform;
        if (villageTransform == null) return playerTransform;

        // Calcola la distanza da entrambi
        float distPlayer = Vector2.Distance(transform.position, playerTransform.position);
        float distVillage = Vector2.Distance(transform.position, villageTransform.position);

        // Chi è più vicino?
        if (distVillage < distPlayer)
        {
            return villageTransform; // Vai al villaggio
        }
        else
        {
            return playerTransform; // Vai al player
        }
    }

    // Gestione danno (Kamikaze)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Village"))
        {
            VillageStats village = other.GetComponent<VillageStats>();
            if (village != null)
            {
                village.TakeDamage(10);
                Destroy(gameObject); // <--- Questo li fa "esplodere" dopo il danno
            }
        }
    }
}
