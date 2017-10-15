using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : EgoSystem<
    EgoConstraint<AnimationComponent, Movement, Animator, Rigidbody, InputComponent, Transform>
>
{
    int jumpHash = Animator.StringToHash("Jump");
    int horizontalSpeedHash = Animator.StringToHash("HorizontalSpeed");
    int turnHash = Animator.StringToHash("Turn");
    int turningStateHash = Animator.StringToHash("Base Layer.turning");
    public override void Start()
    {

    }

    public override void Update()
    {
        // For each GameObject that fits the constraint...
        constraint.ForEachGameObject((egoComponent, animation, movement, animator, rigidbody, input, transform) =>
        {
            float horizontalSpeed = input.horizontalAxis;
           
            if (horizontalSpeed < 0f)
            {
                transform.rotation = Quaternion.Euler(0f,-90f,0f);

            }
            else if(horizontalSpeed> 0f)
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            }
            animator.SetFloat(horizontalSpeedHash, Mathf.Abs(horizontalSpeed));
            //if (movement.jumped)
            //{
            //    animator.SetTrigger(jumpHash);
            //}
        });
    }
}
