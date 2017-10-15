using UnityEngine;

public class DashEvent : EgoEvent
{
    public readonly Vector3 velocity;
    public readonly System.Guid actorGuid;

    public DashEvent(System.Guid playerUid, Vector3 velocity)
    {
        this.actorGuid = playerUid;
        this.velocity = velocity;
    }
}
