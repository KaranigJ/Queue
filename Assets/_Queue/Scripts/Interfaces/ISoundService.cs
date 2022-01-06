

using System;

public interface ISoundService
{
    public void ToggleMute();
    public bool GetMuteState();

    public event Action<bool> muteToggled;
}
