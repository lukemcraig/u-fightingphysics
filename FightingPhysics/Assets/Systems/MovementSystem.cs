// MovementSystem.cs
using UnityEngine;

// MovementSystem updates any GameObject with a Transform & Movement Component
public class MovementSystem : EgoSystem<
    EgoConstraint<Rigidbody, InputComponent, Movement, ActorComponent>
>
{
    public override void Start()
    {
        constraint.ForEachGameObject((egoComponent, rigidbody, input, movement, actor) =>
        {
            movement.numberOfJumpsRemaining = movement.jumpStrengths.Length;
        });

        EgoEvents<TouchGroundEvent>.AddHandler(Handle);
        EgoEvents<TouchPassThroughEvent>.AddHandler(Handle);
    }

    public override void FixedUpdate()
    {
        // For each GameObject that fits the constraint...
        constraint.ForEachGameObject((egoComponent, rigidbody, input, movement, actor) =>
        {
            Vector3 intendedVector = new Vector3(input.horizontalAxis * movement.xSpeed, input.verticalAxis * movement.ySpeed, 0f);
            movement.velocity.x = Mathf.Lerp(movement.velocity.x, intendedVector.x, Time.fixedDeltaTime);


            if (!movement.onGround)
            {
                if (intendedVector.y < 0)
                {
                    Dive(movement, intendedVector);
                }
                if (intendedVector.y >= 0f)
                {
                   Fall(movement);
                }
                movement.velocity.x = Mathf.Lerp(movement.velocity.x, 0f, Time.fixedDeltaTime * movement.windResistance);
            }
            if (movement.canPassThrough)
            {
                if (intendedVector.y < 0)
                {
                    movement.canPassThrough = false;
                    
                    rigidbody.MovePosition(rigidbody.position - (Vector3.up));
                    var pE = new PassThroughEvent(actor.guid);
                    EgoEvents<PassThroughEvent>.AddEvent(pE);
                }
            }
            if (movement.onGround)
            {
                movement.falling = false;
                if (Mathf.Abs(movement.velocity.x) > 3f)
                {
                    var e = new StepEvent(actor.guid);
                    EgoEvents<StepEvent>.AddEvent(e);
                }
                BecomeStationary(movement);
            }
            if (input.jumpKeyPressed)
            {
                TryJump(movement, rigidbody, actor);
                input.jumpKeyPressed = false;
            }
            if (input.dashKeyPressed)
            {
                TryDash(movement, actor);
                input.dashKeyPressed = false;
            }

            SetVelocity(rigidbody, movement);
            if (movement.velocity.y < 0f && !movement.falling)
            {
                var fallEvent = new FallEvent(actor.guid);
                EgoEvents<FallEvent>.AddEvent(fallEvent);
                movement.falling = true;
            }
        });
    }

    private static void Dive(Movement movement, Vector3 intendedVector)
    {      
        movement.velocity.y = Mathf.Lerp(movement.velocity.y, intendedVector.y, Time.fixedDeltaTime);
    }

    private static void Fall(Movement movement)
    {       
        movement.velocity.y = Mathf.Lerp(movement.velocity.y, movement.fallSpeed, Time.fixedDeltaTime);
    }

    private static void BecomeStationary(Movement movement)
    {
       
        float t = movement.friction * Time.fixedDeltaTime;
        if (Mathf.Abs(movement.velocity.x) < 0.01f)
        {
            t = 1f;
        }
        movement.velocity.x = Mathf.Lerp(movement.velocity.x, 0f, t);

    }

    private static void SetVelocity(Rigidbody rigidbody, Movement movement)
    {
        rigidbody.velocity = new Vector3(movement.velocity.x, movement.velocity.y, movement.velocity.z);       
    }

    void TryJump(Movement movement, Rigidbody rigidbody, ActorComponent actor)
    {
        if (movement.numberOfJumpsRemaining > 0)
        {
            float jumpStrength = movement.jumpStrengths[movement.numberOfJumpsRemaining - 1];
            if (movement.velocity.y <= 0f)
            {                
                movement.velocity.y = jumpStrength;
                
            }
            else
            {               
                movement.velocity.y += jumpStrength;
            }
            movement.falling = false;

            var e = new JumpEvent(actor.guid, movement.velocity);
            EgoEvents<JumpEvent>.AddEvent(e);

            movement.numberOfJumpsRemaining--;                  
        }
    }

    // Event Handler Methods
    void Handle(TouchGroundEvent e)
    {
        constraint.ForEachGameObject((egoComponent, rigidbody, input, movement, actor) =>
        {
            if (e.actorGuid == actor.guid)
            {
                if (e.isTouchingGround && movement.velocity.y <= 0f)
                {
                    SetOnGround(movement);
                    var setOnGroundEvent = new SetOnGroundEvent(actor.guid);
                    EgoEvents<SetOnGroundEvent>.AddEvent(setOnGroundEvent);

                }
                else
                {
                    movement.onGround = false;
                }
            }
        });
    }
    void Handle(TouchPassThroughEvent e)
    {
        constraint.ForEachGameObject((egoComponent, rigidbody, input, movement, actor) =>
        {
            if (e.actorGuid == actor.guid)
            {
                movement.canPassThrough=e.isTouchingPassThrough;
            }
        });
    }
    void SetOnGround(Movement movement)
    {
       
        movement.onGround = true;
        movement.velocity.y = 0;
        movement.numberOfJumpsRemaining = movement.jumpStrengths.Length;
       
    }
    void TryDash(Movement movement, ActorComponent actor)
    {
        
        movement.velocity.x = (-movement.velocity.x) + Mathf.Sign(movement.velocity.x) * 30f;
        movement.velocity.y -= movement.velocity.y;
        var e = new DashEvent(actor.guid, movement.velocity);
        EgoEvents<DashEvent>.AddEvent(e);
    }
}