using UnityEngine;

[DisallowMultipleComponent]
public class BottomComponent : MonoBehaviour, BodyPart {
    public bool touchingGround;
    public ActorComponent actor;
}
