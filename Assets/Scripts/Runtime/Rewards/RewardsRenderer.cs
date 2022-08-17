using System.Collections.Generic;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Runtime.Rewards
{
    public class RewardsRenderer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private RewardGroupRenderer cellViewPrefab;
        [SerializeField] private EnhancedScroller scroller;

        [SerializeField] private float animationTime = .2f;
        [SerializeField] private float animationDelayTime = .05f;

        private List<Resource> _rewards;
        private bool _isEnable = false;

        private void Start()
        {
            scroller.Delegate = this;
            _rewards = new List<Resource>()
            {
                new Resource()
                {
                    number = 10000,
                    type = 0,
                },
                new Resource()
                {
                    number = 1000000,
                    type = 0,
                },
                new Resource()
                {
                    number = 1000000000,
                    type = 0,
                },
                new ItemResource()
                {
                    level = 4,
                    number = 1000,
                    type = 1,
                },
                new ItemResource()
                {
                    level = 2,
                    number = 100,
                    type = 1,
                },
                new ItemResource()
                {
                    level = 3,
                    number = 100000,
                    type = 1,
                },
                new ItemResource()
                {
                    level = 1,
                    number = 100000,
                    type = 1,
                },
                new CharacterResource()
                {
                    star = 5,
                    number = 2,
                    type = 2,
                    level = 100,
                },new CharacterResource()
                {
                    star = 6,
                    number = 2,
                    type = 2,
                    level = 5,
                },new CharacterResource()
                {
                    star = 2,
                    number = 2,
                    type = 2,
                    level = 1,
                },new CharacterResource()
                {
                    star = 4,
                    number = 2,
                    type = 2,
                    level = 20,
                },
                new CharacterResource()
                {
                    star = 1,
                    number = 2,
                    type = 2,
                    level = 9999,
                },
            };
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
            return Mathf.CeilToInt(_rewards.Count * 1.0f / RewardGroupRenderer.MaxElement);
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
                rewardGroupRenderer.Render(_rewards, dataIndex);
            }

            return rewardGroupRenderer;
        }
    }
}