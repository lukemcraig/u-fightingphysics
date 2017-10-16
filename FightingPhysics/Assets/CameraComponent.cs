using UnityEngine;

[DisallowMultipleComponent]
public class CameraComponent : MonoBehaviour {
    public GameObject[] following;
    public Vector3 averageFollowingPosition;
    public Vector3 zdistance = new Vector3(0f,0f,10f);
}
