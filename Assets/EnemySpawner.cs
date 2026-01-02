using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Cosa generare")]
    public GameObject enemyPrefab; // Il nemico rosso

    [Header("Impostazioni")]
    public float spawnInterval = 2f; // Ogni quanti secondi nasce un nemico
    public float spawnRadius = 15f;  // Distanza dal giocatore (deve essere fuori dallo schermo)

    private Transform player;
    private float timer;

    void Start()
    {
        // Trova il giocatore per sapere dove spawnare i nemici attorno a lui
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // 1. Calcola una posizione casuale in un cerchio attorno al player
        Vector2 randomPos = Random.insideUnitCircle.normalized * spawnRadius;
        
        // 2. La posizione finale è: Posizione Giocatore + Posizione Casuale
        Vector3 spawnPos = player.position + (Vector3)randomPos;

        // 3. Crea il nemico
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}