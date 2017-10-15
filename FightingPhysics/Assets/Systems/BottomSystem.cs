using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSystem : EgoSystem<
    EgoConstraint<BottomComponent, Transform, ActorComponent>
>
{
    public override void Start()
    {        
        EgoEvents<CollisionEnterEvent>.AddHandler(Handle);
        EgoEvents<CollisionExitEvent>.AddHandler(Handle);
        EgoEvents<JumpEvent>.AddHandler(Handle);
    }

    void Handle(CollisionEnterEvent e)
    {
        Debug.Log("CollisionEnterEvent");
        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            ActorComponent actor;
            if (!e.egoComponent1.TryGetComponents(out actor))
                return;           
            SetOnGround(actor);             
        }
        if (e.egoComponent2.HasComponents<BottomComponent>() && e.egoComponent1.HasComponents<Ground>())
        {
            ActorComponent actor;

            if (!e.egoComponent2.TryGetComponents(out actor))
                return;
            SetOnGround(actor);
        }
    }

    void Handle(CollisionExitEvent e)
    {
        Debug.Log("CollisionExitEvent");
        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            ActorComponent actor;
            if (!e.egoComponent1.TryGetComponents(out actor))
                return;
            SetOffGround(actor);
        }
        if (e.egoComponent2.HasComponents<BottomComponent>() && e.egoComponent1.HasComponents<Ground>())
        {
            ActorComponent actor;

            if (!e.egoComponent2.TryGetComponents(out actor))
                return;
            SetOffGround(actor);
        }
    }
    void Handle(JumpEvent e)
    {
        Debug.Log("JumpEvent");
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor) =>
        {
            if (actor.guid == e.actorGuid)
            {
                bottomComponent.collider.enabled = false;
            }
        });
    }

    void SetOnGround(ActorComponent actorComponent)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor) =>
        {
            if (actor.guid == actorComponent.guid)
                if (!bottomComponent.touchingGround)
                {
                    var e = new TouchGroundEvent(actor.guid, true);
                    EgoEvents<TouchGroundEvent>.AddEvent(e);
                    bottomComponent.touchingGround = true;
                }
        });
    }
    void SetOffGround(ActorComponent actorComponent)
    {        
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor) =>
        {
            if (actor.guid == actorComponent.guid)
                if (bottomComponent.touchingGround)
                {
                    var e = new TouchGroundEvent(actor.guid, false);
                    EgoEvents<TouchGroundEvent>.AddEvent(e);
                    bottomComponent.touchingGround = false;
                }
        });
    }
}
