using UnityEngine;

[DisallowMultipleComponent]
public class CharacterSFXComponent : MonoBehaviour {
    public AudioSource jumpSound;
    public AudioSource dashSound;
    public AudioSource footFloorSound;
    public AudioSource[] footSounds;
    public float timeOfLastStepSound;
    public float stepSoundRate = .1f;
}
