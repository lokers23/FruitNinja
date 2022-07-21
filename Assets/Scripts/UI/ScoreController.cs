using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private int upScore;
        [SerializeField] private float upDelay;
        private void Awake()
        {
            Fruit.FruitController.EventSlice += IncreasingPoints;
        }

        private void IncreasingPoints()
        {
            StartCoroutine(IncreaseScore(upScore));
        }
    
        private IEnumerator IncreaseScore(int newScore)
        {
            for (int i = 1; i < newScore + 1; i++)
            {
                text.text = (int.Parse(text.text) + 1).ToString();
                yield return new WaitForSeconds(upDelay);
            }
        }
    }
}
