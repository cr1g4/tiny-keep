using UnityEngine;
using TMPro; // Serve per usare TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Per poterlo chiamare da ovunque

    [Header("UI")]
    public TextMeshProUGUI moneyText; // Trascina qui la scritta

    private int money = 0; // Il tuo tesoro attuale

    void Awake()
    {
        // Questo trucco (Singleton) permette agli altri script di dire "GameManager.instance"
        instance = this;
    }

    void Start()
    {
        UpdateUI(); // Aggiorna la scritta a 0 all'inizio
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "MONETE: " + money.ToString();
        }
    }
}