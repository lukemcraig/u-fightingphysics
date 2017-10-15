
using UnityEngine;

public class SetOnGroundEvent : EgoEvent
{   
    public readonly System.Guid actorGuid;

    public SetOnGroundEvent(System.Guid playerUid)
    {
        this.actorGuid = playerUid;
       
    }

}
