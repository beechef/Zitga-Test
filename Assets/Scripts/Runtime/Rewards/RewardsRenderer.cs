using System.Collections.Generic;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Runtime.Rewards
{
    public class RewardsRenderer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private List<Reward> rewards;
        [SerializeField] private RewardGroupRenderer cellViewPrefab;
        [SerializeField] private EnhancedScroller scroller;

        [SerializeField] private float animationTime = .2f;
        [SerializeField] private float animationDelayTime = .05f;

        private bool _isEnable = false;

        private void Start()
        {
            scroller.Delegate = this;
        }

        private void OnEnable()
        {
            _isEnable = true;
        }

        private void Update()
        {
            if (_isEnable)
            {
                _isEnable = false;
                Animate();
            }
        }

        private void Animate()
        {
            scroller.ReloadData();

            var activeViews = scroller.ActiveCellViews;
            var rewardGroups = new List<RewardGroupRenderer>();
            for (int i = 0; i < activeViews.Count; i++)
            {
                var rewardGroup = activeViews[i].GetComponent<RewardGroupRenderer>();
                if (rewardGroup == null) continue;
                rewardGroups.Add(rewardGroup);
                foreach (var rewardRenderer in rewardGroup.RewardRenderers)
                {
                    rewardRenderer.rectTransform.pivot += Vector2.up;
                    rewardRenderer.canvasGroup.alpha = 0f;
                }
            }

            var delay = animationDelayTime;
            foreach (var rewardGroup in rewardGroups)
            {
                foreach (var rewardRenderer in rewardGroup.RewardRenderers)
                {
                    rewardRenderer.rectTransform.DOPivotY(0.5f, animationTime).SetEase(Ease.InSine).SetDelay(delay);
                    rewardRenderer.canvasGroup.DOFade(1f, animationTime).SetEase(Ease.InCirc).SetDelay(delay);
                    delay += animationDelayTime;
                }
            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Mathf.CeilToInt(rewards.Count * 1.0f / RewardGroupRenderer.MaxElement);
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 100f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var rewardGroupRenderer = scroller.GetCellView(cellViewPrefab) as RewardGroupRenderer;

            if (rewardGroupRenderer != null)
            {
                rewardGroupRenderer.Render(rewards, dataIndex);
            }

            return rewardGroupRenderer;
        }
    }
}