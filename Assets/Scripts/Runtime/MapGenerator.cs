using UnityEngine;

namespace Runtime
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapsData mapsData;

        [SerializeField] private int capacity = 999;

        [SerializeField] private bool generate;

        private void Update()
        {
            if (generate)
            {
                generate = false;

                mapsData.maps = MapUtilities.CreateMaps(capacity);
            }
        }
    }
}