using UnityEngine;

public class JumpEvent : EgoEvent
{
    public readonly Vector3 velocity;
    public readonly System.Guid actorGuid;

    public JumpEvent(System.Guid playerUid, Vector3 velocity)
    {
        this.actorGuid = playerUid;
        this.velocity = velocity;
    }
}
