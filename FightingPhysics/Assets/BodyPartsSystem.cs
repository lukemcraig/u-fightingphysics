using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsSystem : EgoSystem<
     EgoConstraint<BodyPartsComponent, Transform>
    >
{
    public override void Start()
    {    
        constraint.ForEachGameObject((egoComponent, bodyPartsComponent, transform) =>
        {
            bodyPartsComponent.bodyParts = new List<BodyPart>();
            foreach (Transform child in transform)
            {
                BodyPart bodyPart;
                bodyPart = child.GetComponent<BodyPart>();
                if (bodyPart != null)
                {
                    bodyPartsComponent.bodyParts.Add(bodyPart);
                }
            }
        });
    }


}