using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : EgoSystem<
    EgoConstraint<InputComponent>
>
{
    public override void Update () {
        // For each GameObject that fits the constraint...
        constraint.ForEachGameObject((egoComponent, input) =>
        {
           
            input.horizontalAxis = Input.GetAxis("Horizontal");
            input.verticalAxis = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                input.jumpKeyPressed = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                input.dashKeyPressed = true;
            }
        });
    }
}
