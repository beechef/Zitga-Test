using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class MapAction : MonoBehaviour
    {
        [SerializeField] private MapsData mapsData;
        [SerializeField] private MapsRendererController mapsRendererController;

        private List<Map> _maps;

        private void Start()
        {
            _maps = mapsData.maps;
        }

        public void ResetMap()
        {
            _maps.ResetMaps();
            mapsRendererController.Reload();
        }

        public void RandomMap()
        {
            _maps.RandomStars(Random.Range(0, _maps.Count));
            mapsRendererController.Reload();
        }
    }
}