using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameModeSelectManager : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        var breakoutButton = root.Q<Button>("breakoutButton");
        if (breakoutButton != null)
        {
            breakoutButton.clicked += () => SceneManager.LoadScene("Breakout");
        }

        var cavityButton = root.Q<Button>("cavityButton");
        if (cavityButton != null)
        {
            cavityButton.clicked += () => SceneManager.LoadScene("Cavity");
        }

        var doubleButton = root.Q<Button>("doubleButton");
        if (doubleButton != null)
        {
            doubleButton.clicked += () => SceneManager.LoadScene("Double");
        }

        var backButton = root.Q<Button>("backButton");
        if (backButton != null)
        {
            backButton.clicked += () => SceneManager.LoadScene("TitleScreen");
        }
    }
}
