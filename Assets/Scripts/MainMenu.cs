using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public void Play(int gameMode)
    {
        SceneManager.LoadScene(gameMode);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
