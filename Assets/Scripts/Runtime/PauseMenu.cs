using UnityEngine;

namespace Runtime
{
    public class PauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 0f;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
    }
}