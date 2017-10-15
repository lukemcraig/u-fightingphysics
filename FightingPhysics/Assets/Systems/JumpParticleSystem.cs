using UnityEngine;

public class JumpParticleSystem : EgoSystem<
     EgoConstraint<JumpParticle, ActorComponent>
    >
{

    public override void Start()
    {
        EgoEvents<JumpEvent>.AddHandler(Handle);
        EgoEvents<DashEvent>.AddHandler(Handle);
    }

    void Handle(JumpEvent e)
    {       
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.velocity = -e.velocity.normalized * 4f;
        
        constraint.ForEachGameObject((egoComponent, jumpParticle, actor) =>
        {
            if (actor.guid == e.actorGuid)
            {
                jumpParticle.ps.Emit(emitParams, 3);
            }
        });
    }
    void Handle(DashEvent e)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.velocity = -e.velocity.normalized * 4f;

        constraint.ForEachGameObject((egoComponent, jumpParticle, actor) =>
        {
            if (actor.guid == e.actorGuid)
            {
                jumpParticle.ps.Emit(emitParams, 3);
            }
        });
    }
}