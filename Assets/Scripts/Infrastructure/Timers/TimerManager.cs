using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Timers
{
  public sealed class TimerManager : IFixedTickable
  {
    public static void AddTimer(ITimerable elem) => _timers.Add(elem);
    public static void RemoveTimer(ITimerable elem) => _timers.Remove(elem);

    private static readonly List<ITimerable> _timers = new List<ITimerable>();
    
    public void FixedTick()
    {
      foreach (ITimerable timer in _timers.ToArray())
      {
        timer.TimeLeft -= Time.fixedDeltaTime;
        if (timer.TimeLeft <= 0)
          RemoveTimer(timer);
      }
    }
  }
}