using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 10; // Quanto vale questa moneta?

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // CHIAMA LA BANCA: "Ehi GameManager, aggiungi 10 monete!"
            if (GameManager.instance != null)
            {
                GameManager.instance.AddMoney(value);
            }

            // Distruggimi
            Destroy(gameObject);
        }
    }
}