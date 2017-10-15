using UnityEngine;

public class StepEvent : EgoEvent
{
    public readonly System.Guid actorGuid;
    public StepEvent(System.Guid playerUid)
    {
        this.actorGuid = playerUid;       
    }
}
