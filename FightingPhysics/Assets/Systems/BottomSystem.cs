using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSystem : EgoSystem<
    EgoConstraint<BottomComponent, Transform, ActorComponent, BoxCollider>
>
{
    public override void Start()
    {        
        EgoEvents<CollisionEnterEvent>.AddHandler(Handle);
        EgoEvents<CollisionExitEvent>.AddHandler(Handle);
        EgoEvents<JumpEvent>.AddHandler(Handle);
        EgoEvents<FallEvent>.AddHandler(Handle);
    }

    void Handle(CollisionEnterEvent e)
    {

        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            foreach (ContactPoint contact in e.collision.contacts)
            {
                if (contact.normal.y <=0f)
                {
                    return;
                }
            }
            ActorComponent actor;
            if (!e.egoComponent1.TryGetComponents(out actor))
                return;           
            SetOnGround(actor);             
        }
        if (e.egoComponent2.HasComponents<BottomComponent>() && e.egoComponent1.HasComponents<Ground>())
        {
            foreach (ContactPoint contact in e.collision.contacts)
            {
                if (contact.normal.y <= 0f)
                {
                    return;
                }
            }
            ActorComponent actor;
            if (!e.egoComponent2.TryGetComponents(out actor))
                return;
            SetOnGround(actor);
        }
    }

    void Handle(CollisionExitEvent e)
    {
        Debug.Log("collision exit");
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
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == e.actorGuid)
            {
                collider.enabled = false;
            }
        });
    }
    void Handle(FallEvent e)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == e.actorGuid)
            {
                collider.enabled = true;
            }
        });
    }

    void SetOnGround(ActorComponent actorComponent)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
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
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
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
