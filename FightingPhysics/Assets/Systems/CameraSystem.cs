using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : EgoSystem<
     EgoConstraint<CameraComponent, Camera, Transform>
    >
{
    public override void Update()
    {
        constraint.ForEachGameObject((egoComponent, cameraComponent, camera, transform) =>
        {
            cameraComponent.averageFollowingPosition = Vector3.zero;
            foreach (GameObject go in cameraComponent.following)
            {
                cameraComponent.averageFollowingPosition += go.transform.position;
               
            }
            cameraComponent.averageFollowingPosition /= cameraComponent.following.Length;
            float characterDistance = Vector3.Distance(cameraComponent.following[0].transform.position, cameraComponent.following[1].transform.position);
            transform.position = cameraComponent.averageFollowingPosition + cameraComponent.zdistance + (cameraComponent.zdistance * (characterDistance));
        });
    }
}
