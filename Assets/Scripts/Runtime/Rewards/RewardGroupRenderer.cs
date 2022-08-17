using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Runtime.Rewards
{
    public class RewardGroupRenderer : EnhancedScrollerCellView
    {
        public const int MaxElement = 8;

        [SerializeField] private List<RewardRenderer> rewardRenderers;
        public List<RewardRenderer> RewardRenderers => rewardRenderers;

        public void Render(List<Reward> rewards, int startIndex)
        {
            startIndex *= MaxElement;

            for (int i = 0; i < MaxElement; i++)
            {
                if (i < 0 || i >= rewardRenderers.Count) continue;
                var index = startIndex + i;
                if (index < 0 || index >= rewards.Count)
                {
                    rewardRenderers[i].gameObject.SetActive(false);
                    continue;
                }

                var reward = rewards[startIndex + i];
                var rewardRenderer = rewardRenderers[i];
                rewardRenderer.gameObject.SetActive(true);

                rewardRenderer.Render(reward);
            }
        }
    }
}