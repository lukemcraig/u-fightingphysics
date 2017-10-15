using UnityEngine;

public class RunParticleSystem : EgoSystem<
     EgoConstraint<RunParticleComponent, Movement>
    >
{

    public override void Update()
    {
        // For each GameObject that fits the constraint...
        constraint.ForEachGameObject((egoComponent, runParticleComponent, movement) =>
        {
            if (movement.onGround && Mathf.Abs(movement.velocity.x)>0.1f)
            {
                var emitParams = new ParticleSystem.EmitParams();
                

                var sh = runParticleComponent.ps.shape;
                Vector3 rotation = sh.rotation;
                Mathf.Sign(movement.velocity.x);
                rotation.x = Mathf.Lerp( -16.5f, 198f, Mathf.Sign(movement.velocity.x));
                sh.rotation = rotation;

                runParticleComponent.ps.startSpeed = Mathf.Abs(movement.velocity.x)*runParticleComponent.intensity;

                runParticleComponent.ps.Emit(emitParams, 1);
            }
        });
    }
}