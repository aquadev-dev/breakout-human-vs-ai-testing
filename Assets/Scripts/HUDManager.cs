using UnityEngine;
using UnityEngine.UIElements;
using Unity.Properties;

[RequireComponent(typeof(UIDocument))]
public class HUDManager : MonoBehaviour
{
    private BreakoutGameManager gameManager;
    private Label scoreLabel;
    private Label livesLabel;

    private void OnEnable()
    {
        gameManager = Object.FindAnyObjectByType<BreakoutGameManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        scoreLabel = root.Q<Label>("score-label");
        livesLabel = root.Q<Label>("lives-label");

        if (gameManager != null)
        {
            root.dataSource = gameManager;

            scoreLabel.SetBinding("text", new DataBinding
            {
                dataSourcePath = new PropertyPath(nameof(BreakoutGameManager.Score))
            });

            livesLabel.SetBinding("text", new DataBinding
            {
                dataSourcePath = new PropertyPath(nameof(BreakoutGameManager.Lives))
            });
        }
    }

    private void OnDisable()
    {
        if (scoreLabel != null && scoreLabel.HasBinding("text"))
            scoreLabel.ClearBinding("text");
        if (livesLabel != null && livesLabel.HasBinding("text"))
            livesLabel.ClearBinding("text");
    }
}
