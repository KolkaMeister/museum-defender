namespace Pools
{
    public interface IPoolable  
    {
        public PoolLocator Locator { get; set; }
    }
}