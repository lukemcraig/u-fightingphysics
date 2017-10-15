using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSystem : EgoSystem<
    EgoConstraint<BottomComponent, Transform>
>
{
    public override void Start()
    {        
        EgoEvents<CollisionEnterEvent>.AddHandler(Handle);
        EgoEvents<CollisionExitEvent>.AddHandler(Handle);
    }

    public override void Update()
    {
        constraint.ForEachGameObject((egoComponent, bottom, transform) =>
        {
            bool noGroundFound = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.lossyScale.x *.5f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                var hitEgoComponent = hitColliders[i].GetComponent<EgoComponent>();
                if (hitEgoComponent.HasComponents<Ground>())
                {
                    SetOnGround(bottom);
                    noGroundFound = false;
                }
                i++;
            }
            if (noGroundFound)
            {
                SetOffGround(bottom);
            }
        });
    }

    void Handle(CollisionEnterEvent e)
    {
        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            BottomComponent bottom;
            if (!e.egoComponent1.TryGetComponents(out bottom))
                return;
            SetOnGround(bottom);
        }
        if (e.egoComponent1.HasComponents<Ground>() && e.egoComponent2.HasComponents<BottomComponent>())
        {
            BottomComponent bottom;
            if (!e.egoComponent2.TryGetComponents(out bottom))
                return;
            SetOnGround(bottom);
        }
    }
    void Handle(CollisionExitEvent e)
    {
        if (e.egoComponent1.HasComponents<BottomComponent>() && e.egoComponent2.HasComponents<Ground>())
        {
            BottomComponent bottom;
            if (!e.egoComponent1.TryGetComponents(out bottom))
                return;
            SetOffGround(bottom);
        }
        if (e.egoComponent1.HasComponents<Ground>() && e.egoComponent2.HasComponents<BottomComponent>())
        {
            BottomComponent bottom;
            if (!e.egoComponent2.TryGetComponents(out bottom))
                return;
            SetOffGround(bottom);
        }
    }

    void SetOnGround(BottomComponent bottom)
    {
        if (!bottom.touchingGround)
        {
            var e = new TouchGroundEvent(bottom.actor.guid, true);
            EgoEvents<TouchGroundEvent>.AddEvent(e);
        }
        bottom.touchingGround = true;
        
    }
    void SetOffGround(BottomComponent bottom)
    {        
        if (bottom.touchingGround)
        {
            var e = new TouchGroundEvent(bottom.actor.guid, false);
            EgoEvents<TouchGroundEvent>.AddEvent(e);
        }
        bottom.touchingGround = false;
    }
}
