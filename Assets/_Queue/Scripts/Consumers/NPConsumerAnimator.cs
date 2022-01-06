using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPConsumerAnimator : ConsumerAnimator
{
    public override void Idle()
    {
        State = ConsumerAnimationState.Idle;
    }
}
