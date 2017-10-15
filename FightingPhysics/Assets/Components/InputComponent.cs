using UnityEngine;

[DisallowMultipleComponent]
public class InputComponent : MonoBehaviour {
    public bool jumpKeyPressed = false;
    public float horizontalAxis = 0.0f;
    public float verticalAxis = 0.0f;
    internal bool dashKeyPressed;
}
