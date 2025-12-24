using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; //  for UI text

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 20;   //  Separate max health
    [SerializeField] TMP_Text healthUI;    //  Drag UI Text here in Inspector

    int currentHealth;
    public static bool IsPlayerAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (currentHealth <= 0 && IsPlayerAlive)
        {
            IsPlayerAlive = false;
            SelfDestroy();
            ReloadScene();
            IsPlayerAlive = true;  // Reset for next scene
        }
    }

    public void TakeDamage(int damage)
    {
        if (!IsPlayerAlive) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // prevent negative health
        Debug.Log($"Player health: {currentHealth}");

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthUI != null)
        {
            healthUI.text = $"Player Health: {currentHealth}/{maxHealth}";
        }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void ReloadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
}
