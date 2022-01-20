using Abstract;
using Spawner;

namespace Collectable
{
    public class Crystal : ACrystal
    {
        public override void OnCollected(ref int currentScore, ObjectSpawner spawner)
        {
            base.OnCollected(ref currentScore, spawner);
            HasCollected = true;
            spawner.SendToPool(this);

        }
    }
}
