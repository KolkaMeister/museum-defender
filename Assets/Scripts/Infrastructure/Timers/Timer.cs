using System;
using UnityEngine;

namespace Infrastructure.Timers
{
  [Serializable]
  public class Timer : ITimerable, IComparable, IComparable<Timer>
  {
    [SerializeField] private float _timeLeft;

    public float TimeLeft
    {
      get => _timeLeft;
      set => _timeLeft = value;
    }

    public static implicit operator float(Timer obj) => obj.TimeLeft;
    public static implicit operator Timer(float obj) => new Timer { TimeLeft = obj };

    public int CompareTo(object obj)
    {
      if (obj != null && obj is not Timer)
        throw new ArgumentException($"Object is not a {nameof(Timer)}");
      
      return CompareTo((Timer)obj);
    }

    public int CompareTo(Timer other)
    {
      return other != null ? _timeLeft.CompareTo(other._timeLeft) : 1;
    }
  }
}