using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Rewards
{
    public class StarsRenderer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> stars;

        public void Render(int starNumber)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].SetActive(i + 1 <= starNumber);
            }
        }
    }
}