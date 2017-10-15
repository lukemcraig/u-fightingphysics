using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSystem : EgoSystem<
    EgoConstraint<BottomComponent, Transform, BoxCollider, ChildActorComponent>
>
{
    public override void Start()
    {        
        EgoEvents<CollisionEnterEvent>.AddHandler(Handle);
        EgoEvents<CollisionExitEvent>.AddHandler(Handle);
        EgoEvents<JumpEvent>.AddHandler(Handle);
    }

    public override void FixedUpdate()
    {
        //constraint.ForEachGameObject((egoComponent, bottom, transform) =>
        //{
        //    bool noGroundFound = true;

        //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.lossyScale.x *.5f);
        //    int i = 0;
        //    while (i < hitColliders.Length)
        //    {
        //        var hitEgoComponent = hitColliders[i].GetComponent<EgoComponent>();
        //        if (hitEgoComponent.HasComponents<Ground>())
        //        {
        //            SetOnGround(bottom);
        //            noGroundFound = false;
        //        }
        //        i++;
        //    }
        //    if (noGroundFound)
        //    {
        //        SetOffGround(bottom);
        //    }
        //});
    }

    void Handle(CollisionEnterEvent e)
    {
        Debug.Log("CollisionEnterEvent");
        if (e.egoComponent1.HasComponents<BodyPartsComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            BodyPartsComponent bodyPartsComponent;
            
            if (!e.egoComponent1.TryGetComponents(out bodyPartsComponent))
                return;
            foreach (BodyPart part in bodyPartsComponent.bodyParts) {
                if (part.GetType() == typeof(BottomComponent))
                {
                    BottomComponent bottom = (BottomComponent) part;
                    SetOnGround(bottom);
                }
            }
        }
        if (e.egoComponent1.HasComponents<Ground>() && e.egoComponent2.HasComponents<BodyPartsComponent>())
        {
            BodyPartsComponent bodyPartsComponent;

            if (!e.egoComponent2.TryGetComponents(out bodyPartsComponent))
                return;
            foreach (BodyPart part in bodyPartsComponent.bodyParts)
            {
                if (part.GetType() == typeof(BottomComponent))
                {
                    BottomComponent bottom = (BottomComponent)part;
                    SetOnGround(bottom);
                }
            }
        }
    }

    void Handle(CollisionExitEvent e)
    {
        Debug.Log("CollisionExitEvent");
        if (e.egoComponent1.HasComponents<BodyPartsComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            BodyPartsComponent bodyPartsComponent;

            if (!e.egoComponent1.TryGetComponents(out bodyPartsComponent))
                return;
            foreach (BodyPart part in bodyPartsComponent.bodyParts)
            {
                if (part.GetType() == typeof(BottomComponent))
                {
                    BottomComponent bottom = (BottomComponent)part;
                    SetOffGround(bottom);
                }
            }
        }
        if (e.egoComponent1.HasComponents<Ground>() && e.egoComponent2.HasComponents<BodyPartsComponent>())
        {
            BodyPartsComponent bodyPartsComponent;

            if (!e.egoComponent2.TryGetComponents(out bodyPartsComponent))
                return;
            foreach (BodyPart part in bodyPartsComponent.bodyParts)
            {
                if (part.GetType() == typeof(BottomComponent))
                {
                    BottomComponent bottom = (BottomComponent)part;
                    SetOffGround(bottom);
                }
            }
        }
    }
    void Handle(JumpEvent e)
    {
        Debug.Log("JumpEvent");
        constraint.ForEachGameObject((egoComponent, bottomComponent, transform, collider, childActor) =>
        {
            if (childActor.actor.guid == e.actorGuid)
            {
                collider.enabled = false;
            }
        });
    }

    void SetOnGround(BottomComponent bottom)
    {
        if (!bottom.touchingGround)
        {
            ChildActorComponent childActor = bottom.GetComponent<ChildActorComponent>();
            if (childActor!=null)
            {
                var e = new TouchGroundEvent(childActor.actor.guid, true);
                EgoEvents<TouchGroundEvent>.AddEvent(e);
            }
            
        }
        bottom.touchingGround = true;
        
    }
    void SetOffGround(BottomComponent bottom)
    {        
        if (bottom.touchingGround)
        {           
            ChildActorComponent childActor = bottom.GetComponent<ChildActorComponent>();
            if (childActor != null)
            {
                var e = new TouchGroundEvent(childActor.actor.guid, false);
                EgoEvents<TouchGroundEvent>.AddEvent(e);
            }
        }
        bottom.touchingGround = false;
    }
}
