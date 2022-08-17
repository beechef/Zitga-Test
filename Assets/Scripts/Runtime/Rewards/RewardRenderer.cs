using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Rewards
{
    public class RewardRenderer : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI quantity;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private StarsRenderer starsRenderer;

        [SerializeField] private Sprite moneyIcon;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private Sprite characterIcon;

        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;

        public void Render(Resource resource)
        {
            quantity.text = resource.number.UseSuffix();

            var resourceType = resource.type;

            switch (resourceType)
            {
                case 0:
                {
                    icon.sprite = moneyIcon;
                    level.text = "";
                    starsRenderer.Render(0);
                    break;
                }
                case 1:
                {
                    if (resource.GetType() != typeof(ItemResource)) break;
                    var itemResource = resource as ItemResource;

                    icon.sprite = itemIcon;
                    level.text = "";
                    starsRenderer.Render(itemResource.level);
                    break;
                }
                case 2:
                {
                    if (resource.GetType() != typeof(CharacterResource)) break;
                    var characterResource = resource as CharacterResource;

                    icon.sprite = characterIcon;
                    level.text = $"Lv.{characterResource.level}";
                    starsRenderer.Render(characterResource.star);
                    break;
                }
            }
        }
    }
}