using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement root;
    private Button restartButton;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        
        // Hide by default
        root.style.display = DisplayStyle.None;

        restartButton = root.Q<Button>("restartButton");
        if (restartButton != null)
        {
            restartButton.clicked += RestartGame;
        }
    }

    public void ShowGameOver()
    {
        root.style.display = DisplayStyle.Flex;
        // Optional: Pause the game
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
