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
        EgoEvents<TriggerEnterEvent>.AddHandler(Handle);
        EgoEvents<CollisionExitEvent>.AddHandler(Handle);
        EgoEvents<JumpEvent>.AddHandler(Handle);
        EgoEvents<FallEvent>.AddHandler(Handle);
        EgoEvents<PassThroughEvent>.AddHandler(Handle);
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

        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<PassThrough>())
        {
            foreach (ContactPoint contact in e.collision.contacts)
            {
                if (contact.normal.y <= 0f)
                {
                    return;
                }
            }
            ActorComponent actor;
            if (!e.egoComponent1.TryGetComponents(out actor))
                return;
            SetOnPassThrough(actor);
        }
        if (e.egoComponent2.HasComponents<BottomComponent>() && e.egoComponent1.HasComponents<PassThrough>())
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
            SetOnPassThrough(actor);
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

        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<PassThrough>())
        {
            ActorComponent actor;
            if (!e.egoComponent1.TryGetComponents(out actor))
                return;
            SetOffPassThrough(actor);
        }
        if (e.egoComponent2.HasComponents<BottomComponent>() && e.egoComponent1.HasComponents<PassThrough>())
        {
            ActorComponent actor;

            if (!e.egoComponent2.TryGetComponents(out actor))
                return;
            SetOffPassThrough(actor);
        }
    }
    void Handle(TriggerEnterEvent e)
    {
        Debug.Log("collision exit");
        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<UnpassThroughComponent>())
        {
            ActorComponent actor;
            if (!e.egoComponent1.TryGetComponents(out actor))
                return;
            SetLayerBack(actor);
        }
        if (e.egoComponent2.HasComponents<BottomComponent>() && e.egoComponent1.HasComponents<UnpassThroughComponent>())
        {
            ActorComponent actor;

            if (!e.egoComponent2.TryGetComponents(out actor))
                return;
            SetLayerBack(actor);
        }

    }
    void Handle(JumpEvent e)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == e.actorGuid)
            {
                // collider.enabled = false;
                egoComponent.gameObject.layer = 10;
            }
        });
    }
    void Handle(FallEvent e)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == e.actorGuid)
            {
                // collider.enabled = true;
                if (egoComponent.gameObject.layer != 13)
                {
                    egoComponent.gameObject.layer = 11;
                }
            }
        });
    }
    void Handle(PassThroughEvent e)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == e.actorGuid)
            {
                Debug.Log("PassThrough");
                bottomComponent.gameObject.layer = 13;
               
            }
        });
    }
    void SetLayerBack(ActorComponent actorComponent)
    {
       
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == actorComponent.guid)
                transform.gameObject.layer = 11;
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
    void SetOnPassThrough(ActorComponent actorComponent)
    {
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == actorComponent.guid)
                if (!bottomComponent.touchingPassThrough)
                {
                    var e = new TouchPassThroughEvent(actor.guid, true);
                    EgoEvents<TouchPassThroughEvent>.AddEvent(e);
                    bottomComponent.touchingPassThrough = true;
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
    void SetOffPassThrough(ActorComponent actorComponent)
    {
        Debug.Log("SetOffPassThrough");
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, actor, collider) =>
        {
            if (actor.guid == actorComponent.guid)
                if (bottomComponent.touchingPassThrough)
                {                   
                    var e = new TouchPassThroughEvent(actor.guid, false);
                    EgoEvents<TouchPassThroughEvent>.AddEvent(e);
                    bottomComponent.touchingPassThrough = false;
                }
        });
    }
}
