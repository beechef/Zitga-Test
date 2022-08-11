using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class VictoryMenu : MonoBehaviour
    {
        [SerializeField] private MapsData mapsData;

        public void Open()
        {
            if (isActiveAndEnabled) return;
            UnlockNextStage();
            gameObject.SetActive(true);
        }

        public void Retry()
        {
            mapsData.currentMap--;
            SceneManager.LoadScene("GamePlay");
        }

        private void UnlockNextStage()
        {
            mapsData.maps[mapsData.currentMap - 1].stars = 3;
            mapsData.currentMap = Mathf.Clamp(mapsData.currentMap + 1, 0, mapsData.maps.Count);
            mapsData.maps[mapsData.currentMap - 1].isLock = false;
        }
    }
}