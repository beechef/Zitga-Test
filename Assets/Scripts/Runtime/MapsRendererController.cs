using System.Runtime.CompilerServices;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Runtime
{
    public class MapsRendererController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private MapsData mapsData;
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private MapHorizontalRenderer cellViewPrefab;
        [SerializeField] private bool isReverse = true;

        [SerializeField] private StarsCountRenderer starsCountRenderer;

        private void Start()
        {
            scroller.Delegate = this;
            Reload();
        }

        public void Reload()
        {
            scroller.ReloadData();
            scroller.ScrollPosition = scroller.GetScrollPositionForDataIndex(
                Mathf.CeilToInt((mapsData.maps.Count * 1.0f) / MapHorizontalRenderer.MaxElement),
                EnhancedScroller.CellViewPositionEnum.After);
            starsCountRenderer.Render(mapsData.maps.CountStars());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetNumberOfCells(EnhancedScroller scroller) =>
            Mathf.CeilToInt((mapsData.maps.Count * 1.0f) / MapHorizontalRenderer.MaxElement);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) => 100f;

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            MapHorizontalRenderer horizontalRenderer = scroller.GetCellView(cellViewPrefab) as MapHorizontalRenderer;

            dataIndex = isReverse
                ? Mathf.CeilToInt((mapsData.maps.Count * 1.0f) / MapHorizontalRenderer.MaxElement) - dataIndex - 1
                : dataIndex;

            horizontalRenderer?.Render(mapsData.maps, dataIndex);

            return horizontalRenderer;
        }
    }
}