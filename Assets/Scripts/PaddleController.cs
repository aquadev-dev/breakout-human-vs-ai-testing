using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float xRange = 8f;

    private InputAction moveAction;

    private void Start()
    {
        if (InputSystem.actions != null)
        {
            moveAction = InputSystem.actions.FindAction("Move");
        }
    }

    private void Update()
    {
        if (moveAction == null) return;

        float moveInput = moveAction.ReadValue<float>();
        Vector3 pos = transform.position;
        pos.x += moveInput * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -xRange, xRange);
        transform.position = pos;
    }
}
