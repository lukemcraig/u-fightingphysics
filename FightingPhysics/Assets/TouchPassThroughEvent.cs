using UnityEngine;

public class TouchPassThroughEvent : EgoEvent {

    public readonly bool isTouchingPassThrough;
    public readonly System.Guid actorGuid;

    public TouchPassThroughEvent(System.Guid playerUid, bool isTouchingGround)
    {
        this.actorGuid = playerUid;
        this.isTouchingPassThrough = isTouchingGround;
    }
}
