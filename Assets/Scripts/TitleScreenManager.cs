using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        var playButton = root.Q<Button>("playButton");
        if (playButton != null)
        {
            playButton.clicked += () => SceneManager.LoadScene("GameModeSelect");
        }

        var quitButton = root.Q<Button>("quitButton");
        if (quitButton != null)
        {
            quitButton.clicked += () => {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            };
        }
    }
}
