using UnityEngine;
using UnityEngine.UIElements;

public class RainbowTextEffect : MonoBehaviour
{
    public float speed = 1.0f;
    private Label titleLabel;
    private float hue = 0;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        titleLabel = root.Q<Label>("titleLabel");
    }

    private void Update()
    {
        if (titleLabel != null)
        {
            hue = (hue + Time.deltaTime * speed) % 1.0f;
            titleLabel.style.color = Color.HSVToRGB(hue, 1.0f, 1.0f);
        }
    }
}
