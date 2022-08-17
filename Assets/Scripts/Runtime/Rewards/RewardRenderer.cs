using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Rewards
{
    public class RewardRenderer : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI quantity;

        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;

        public void Render(Reward reward)
        {
            quantity.text = reward.number.ToString();
        }
    }
}