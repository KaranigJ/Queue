using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelUIService
{
    public void SetTimerText(float time);
    public void SetScreen(LevelState levelState);
}
