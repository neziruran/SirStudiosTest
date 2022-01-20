
using Spawner;

namespace Abstract
{
    public interface ICollectable
    {
        public bool HasCollected {set; }

        public void OnCollected(ref int currentScore,ObjectSpawner spawner);
    }
}
