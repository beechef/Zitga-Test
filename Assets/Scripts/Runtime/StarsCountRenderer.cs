using TMPro;
using UnityEngine;

namespace Runtime
{
    public class StarsCountRenderer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI starsCount;

        public void Render(int count)
        {
            starsCount.text = count.ToString();
        }
    }
}