using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Impostazioni")]
    public float moveSpeed = 5f; // Velocità di movimento
    public VirtualJoystick joystick; // Trascina qui il JoystickBackground

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        // Prende in automatico il componente Rigidbody che abbiamo appena aggiunto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Leggiamo l'input dal Joystick
        if (joystick.InputDirection != Vector2.zero)
        {
            moveInput = joystick.InputDirection;
        }
        else
        {
            // 2. (Opzionale) Aggiungo anche i tasti WASD per quando testi dal computer
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput = moveInput.normalized;
        }
    }

    void FixedUpdate()
    {
        // 3. Muoviamo il personaggio usando la fisica
        // Questo è meglio di "Transform.Translate" perché gestisce le collisioni coi muri
        rb.linearVelocity = moveInput * moveSpeed;
    }
}