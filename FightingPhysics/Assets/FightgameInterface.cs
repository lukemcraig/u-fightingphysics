using UnityEngine;
using System.Collections.Generic;

public class FightgameInterface : MonoBehaviour
{
	static FightgameInterface()
	{
        //Add Systems here:
        EgoSystems.Add(
            new ActorSystem(),            
            new InputSystem(),           
            new MovementSystem(),
            new BottomSystem(),

            new JumpParticleSystem(),
            new RunParticleSystem(),
            new CharacterAudioSystem()
            //new AnimationSystem()
        );
    }

    void Start()
    {
    	EgoSystems.Start();
	}
	
	void Update()
	{
		EgoSystems.Update();
	}
	
	void FixedUpdate()
	{
		EgoSystems.FixedUpdate();
	}
}
