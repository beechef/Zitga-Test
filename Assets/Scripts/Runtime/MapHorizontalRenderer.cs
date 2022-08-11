using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    public class MapHorizontalRenderer : EnhancedScrollerCellView
    {
        public const int MaxElement = 4;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform mapRender;
        [SerializeField] private HorizontalLayoutGroup layoutGroup;
        [SerializeField] private MapRenderer[] mapRenderers = new MapRenderer[MaxElement];
        [SerializeField] private Image connectLine;
        [SerializeField] private Image backgroundLine;

        public void Render(List<Map> maps, int startIndex)
        {
            int deactivateCount = 0;
            layoutGroup.reverseArrangement = startIndex % 2 != 0;

            startIndex *= MaxElement;

            connectLine.enabled = startIndex != 0;


            for (int i = 0; i < mapRenderers.Length; i++)
            {
                int mapIndex = startIndex + i;

                if (mapIndex >= maps.Count)
                {
                    mapRenderers[i].gameObject.SetActive(false);
                    deactivateCount++;

                    continue;
                }

                mapRenderers[i].gameObject.SetActive(true);

                mapRenderers[i].Render(maps[mapIndex]);
            }

            var sizeDelta = rectTransform.sizeDelta;

            backgroundLine.rectTransform.offsetMin = new Vector2(100, backgroundLine.rectTransform.offsetMin.y);
            backgroundLine.rectTransform.offsetMax = new Vector2(-100, backgroundLine.rectTransform.offsetMax.y);

            if (layoutGroup.reverseArrangement)
            {
                backgroundLine.rectTransform.offsetMin = new Vector2((sizeDelta.x * deactivateCount / MaxElement) + 100,
                    backgroundLine.rectTransform.offsetMin.y);
            }
            else
            {
                backgroundLine.rectTransform.offsetMax = new Vector2(
                    (-(sizeDelta.x * deactivateCount / MaxElement) - 100),
                    backgroundLine.rectTransform.offsetMax.y);
            }
        }
    }
}