using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughEvent : EgoEvent
{    
    public readonly System.Guid actorGuid;

    public PassThroughEvent(System.Guid playerUid)
    {
        this.actorGuid = playerUid;       
    }
}
