using UnityEngine;

public class TouchGroundEvent : EgoEvent
{
    public readonly bool isTouchingGround;
    public readonly System.Guid actorGuid;

    public TouchGroundEvent(System.Guid playerUid, bool isTouchingGround)
    {
        this.actorGuid = playerUid;
        this.isTouchingGround = isTouchingGround;
    }
}
