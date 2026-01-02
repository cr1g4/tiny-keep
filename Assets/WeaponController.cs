using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Configurazione")]
    public GameObject bulletPrefab; // Trascina qui il Prefab del Proiettile
    public float fireRate = 0.5f;   // Spara ogni 0.5 secondi
    public float range = 10f;       // Distanza massima di tiro

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            ShootNearestEnemy();
            fireTimer = fireRate;
        }
    }

    void ShootNearestEnemy()
    {
        // 1. Cerca TUTTI gli oggetti con il tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // SPIA 1: Quanti ne vede?
        Debug.Log("🔍 Controllo Radar: Ho trovato " + enemies.Length + " nemici.");

        if (enemies.Length == 0) return; // Se dice 0, il problema è il TAG!

        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            
            // SPIA 2: A che distanza sono?
            Debug.Log("📏 Nemico rilevato a distanza: " + distance + " (Il mio Range è: " + range + ")");

            if (distance < minDistance && distance <= range)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            Shoot(nearestEnemy.transform);
        }
        else
        {
            Debug.Log("❌ Nemici trovati, ma sono TROPPO LONTANI!");
        }
    }

    void Shoot(Transform target)
    {
        // Crea il proiettile nella posizione del giocatore
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // Calcola la direzione verso il nemico
        Vector2 direction = target.position - transform.position;
        
        // Ruota il proiettile in modo che la sua parte "alta" (Up) punti al nemico
        bullet.transform.up = direction;
        void Shoot(Transform target)
    {
        Debug.Log("BANG! Ho sparato!"); // <--- AGGIUNGI QUESTO
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // ... resto del codice ...
    }
    }
    
    // Disegna un cerchio rosso nell'editor per farti vedere il raggio d'azione
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
}