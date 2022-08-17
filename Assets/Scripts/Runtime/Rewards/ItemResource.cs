using System.Collections.Generic;

namespace Runtime.Rewards
{
    public class ItemResource : Resource
    {
        public int inventoryId;
        public int level;

        public List<int> statTypes;
        public List<float> baseValues;
        public List<float> option1;
        public List<float> option2;
    }
}