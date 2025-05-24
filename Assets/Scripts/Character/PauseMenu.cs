using System;
using UnityEngine;
public static class PauseMenu
{
    public static bool IsPaused;
    internal static void Toggle()
    {
        if (IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public static void Resume()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
    }
    public static void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }
}