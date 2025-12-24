using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] TMP_Text enemyLeftUI;   // TextMeshPro UI for enemies left
    [SerializeField] GameObject winScreen;   // Panel for Win Screen

    int totalEnemies;   // total enemies at start
    int currentEnemies; // enemies alive right now

    private void Start()
    {
        if (winScreen != null)
            winScreen.SetActive(false); // Hide win screen initially

        // Ensure cursor is hidden/locked at the start of every play
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Count enemies at the start of the scene
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        currentEnemies = totalEnemies;

        UpdateEnemyUI();
    }


    private void Update()
    {
        // Count remaining enemies
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateEnemyUI();

        // Check win condition
        if (currentEnemies == 0)
        {
            ShowWinScreen();
        }
    }

    void UpdateEnemyUI()
    {
        if (enemyLeftUI != null)
            enemyLeftUI.text = $"Enemies Left: {currentEnemies} / {totalEnemies}";
    }

    public void ShowWinScreen()
    {
        if (winScreen != null)
            winScreen.SetActive(true);

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Freeze time (this already stops player + AI)
        //Time.timeScale = 0f;
    }




    // Called by Replay button
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




    // Called by Exit button
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit!");
    }
}

