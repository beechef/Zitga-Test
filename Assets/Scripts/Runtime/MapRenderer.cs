using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime
{
    public class MapRenderer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private List<Image> stars;
        [SerializeField] private Image lockImage;
        [SerializeField] private Image tutorialImage;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private MapsData mapsData;

        private Map _map;

        public void Render(Map map)
        {
            _map = map;
            lockImage.enabled = map.isLock;
            tutorialImage.enabled = map.level == 1;

            RenderStars(map);

            levelText.enabled = !tutorialImage.enabled;
            levelText.text = map.level.ToString();
        }

        private void RenderStars(Map map)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].enabled = i < map.stars;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_map.isLock) return;
            mapsData.currentMap = _map.level;
            SceneManager.LoadScene("GamePlay");
        }
    }
}