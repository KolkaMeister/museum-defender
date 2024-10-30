namespace Infrastructure.Timers
{
  public interface ITimerable
  {
    public float TimeLeft { get; set; }
  }
}