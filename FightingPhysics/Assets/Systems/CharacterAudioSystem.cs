using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioSystem : EgoSystem<
    EgoConstraint<CharacterSFXComponent, ActorComponent>
>
{
    public override void Start()
    {
        EgoEvents<JumpEvent>.AddHandler(Handle);
        EgoEvents<DashEvent>.AddHandler(Handle);
        EgoEvents<StepEvent>.AddHandler(Handle);
        EgoEvents<SetOnGroundEvent>.AddHandler(Handle);
    }

    void Handle(SetOnGroundEvent e)
    {
        constraint.ForEachGameObject((egoComponent, sfx, actor) =>
        {
            if (actor.guid == e.actorGuid)
            {
                sfx.footFloorSound.pitch = Random.Range(0.9f, 1.1f);
                sfx.footFloorSound.Play();
            }
        });
    }

    void Handle(StepEvent e)
    {
        constraint.ForEachGameObject((egoComponent, sfx, actor) =>
        {
            if (actor.guid == e.actorGuid && Time.time- sfx.timeOfLastStepSound > sfx.stepSoundRate)
            {
                int index = Random.Range(0, sfx.footSounds.Length-1);
                sfx.footSounds[index].pitch = Random.Range(0.9f, 1.1f);
                sfx.footSounds[index].Play();
                sfx.timeOfLastStepSound = Time.time;
            }
        });
    }

    void Handle(JumpEvent e)
    {     
        constraint.ForEachGameObject((egoComponent, sfx, actor) =>
        {
            if (actor.guid == e.actorGuid)
            {
                sfx.jumpSound.pitch = Random.Range(0.9f, 1.1f);
                sfx.jumpSound.Play();
            }
        });
    }

    void Handle(DashEvent e)
    {
        constraint.ForEachGameObject((egoComponent, sfx, actor) =>
        {
            if (actor.guid == e.actorGuid)
            {
                sfx.dashSound.pitch = Random.Range(0.9f, 1.1f);
                sfx.dashSound.Play();
            }
        });
    }
}
