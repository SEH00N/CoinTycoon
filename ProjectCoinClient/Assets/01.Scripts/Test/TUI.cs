using UnityEngine;

namespace ProjectCoin.Tests
{
    public class TUI : MonoBehaviour
    {
        [SerializeField] float upPosition = 700f;
        [SerializeField] GameObject upButton = null;
        [SerializeField] GameObject downButton = null;

        private void Awake()
        {
            SlideDown();
        }

        public void SlideUp()
        {
            (transform as RectTransform).anchoredPosition = new Vector3(0, upPosition);
            upButton.SetActive(false);
            downButton.SetActive(true);
        }

        public void SlideDown()
        {
            (transform as RectTransform).anchoredPosition = new Vector3(0, 0f);
            downButton.SetActive(false);
            upButton.SetActive(true);
        }
    }
}
