using UnityEngine;

public class ActorSystem : EgoSystem<
     EgoConstraint<ActorComponent>
    >
{
	public override void Start()
	{
        constraint.ForEachGameObject((egoComponent, actor) =>
        {       
            actor.guid = System.Guid.NewGuid();   
        });
    }


}