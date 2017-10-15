using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildActorSystem : EgoSystem<
     EgoConstraint<ChildActorComponent, ActorComponent>
    >
{
    public override void Start()
    {
        // make all the actor components on a child actor entity match their parent actors guid
        constraint.ForEachGameObject((egoComponent, childActor, actor) =>
        {
            actor.guid = childActor.actor.guid;
        });
    }
}
