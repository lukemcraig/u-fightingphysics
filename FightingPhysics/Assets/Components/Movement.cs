// Movement.cs
using UnityEngine;

[DisallowMultipleComponent]
public class Movement : MonoBehaviour
{
    public bool crouched = false;
    public bool onGround = false;
    public bool onPassThrough = false;

    public Vector3 velocity = new Vector3(0f, 0f, 0f);

    public float xSpeed = 3.0f;
    public float ySpeed = 4.0f;
    public int numberOfJumpsRemaining;
    public int[] jumpStrengths;


    public float fallSpeed = -20f;
    public float friction = 5f;
    public float windResistance = 1f;
    
}