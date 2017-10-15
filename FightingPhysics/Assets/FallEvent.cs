using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEvent : EgoEvent
{
  
    public readonly System.Guid actorGuid;

    public FallEvent(System.Guid playerUid)
    {
        this.actorGuid = playerUid;
       
    }

}
