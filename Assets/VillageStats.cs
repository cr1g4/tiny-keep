using UnityEngine;
using TMPro; // Serve per la scritta HP

public class VillageStats : MonoBehaviour
{
    [Header("Statistiche")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public TextMeshProUGUI healthText; // Trascina qui la scritta degli HP

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;
        }
    }

    void GameOver()
    {
        Debug.Log("💀 IL VILLAGGIO È CADUTO!");
        healthText.text = "GAME OVER";
        healthText.color = Color.red;
        
        // Blocca il gioco (tutto si ferma)
        Time.timeScale = 0f; 
    }
}
