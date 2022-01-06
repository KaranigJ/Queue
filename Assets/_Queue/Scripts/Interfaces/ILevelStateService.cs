using System;

public interface ILevelStateService
{
    public event Action LevelStarted;
    public event Action Won;
    public event Action Lost; 

    public void TryStartLevel();
    public void OnTimeOver();
    public void OnPlayerServed();
}
