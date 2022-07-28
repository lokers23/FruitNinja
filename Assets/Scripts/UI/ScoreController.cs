using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        public const string KeyPlayerPrefs = "SaveRecordScore";
        [SerializeField] private float upDelay;

        [SerializeField] private TextMeshProUGUI currentScoreText;
        [SerializeField] private TextMeshProUGUI recordScoreText;
        [SerializeField] private float comboDelay = 0.3f;
        
        public TextMeshPro comboText;

        private int _currentScore = 0;
        private int _recordScore;
        private int _scoreCombo = 0;
        
        private float _lastTimeSlice = 0f;
        private StringBuilder _textForCombo;
        private void Awake()
        {
            _textForCombo = new StringBuilder();
            Fruit.FruitController.EventSlice += IncreasingPoints;
            if (PlayerPrefs.HasKey(KeyPlayerPrefs))
            {
                _recordScore = PlayerPrefs.GetInt(KeyPlayerPrefs);
                recordScoreText.text = _recordScore.ToString();
            }
        }

        private void OnDisable()
        {
            Fruit.FruitController.EventSlice -= IncreasingPoints;
        }

        private void Update()
        {
            if (Time.time - _lastTimeSlice > comboDelay)
            {
                if (_scoreCombo > 2)
                {
                    _textForCombo.Append("<align=\"left\"><size=12><color=yellow>");
                    _textForCombo.Append(_scoreCombo);
                    _textForCombo.Append(" фрукта</color></size></align>\nсерия\nx");
                    _textForCombo.Append(_scoreCombo);

                    comboText.text = _textForCombo.ToString();
                    var objectText = Instantiate(comboText, Vector3.zero, Quaternion.identity);
                    Destroy(objectText.gameObject, 1f);
                    _textForCombo.Length = 0;
                }
                
                _scoreCombo = 0;
            }
        }

        private void IncreasingPoints(int score)
        {
            _scoreCombo++;
            if (_scoreCombo > 2)
            {
                score += _scoreCombo;
            }
            
            _lastTimeSlice = Time.time;
            _currentScore += score;
            
            CheckRecordScore();
            StartCoroutine(IncreaseScore(score, currentScoreText));
        }
    
        private IEnumerator IncreaseScore(int newScore, TextMeshProUGUI text)
        {
            for (int i = 1; i < newScore + 1; i++)
            {
                text.text = (int.Parse(currentScoreText.text) + 1).ToString();
                yield return new WaitForSeconds(upDelay);
            }
        }

        private void CheckRecordScore()
        {
            if (_currentScore > _recordScore)
            {
                StartCoroutine(IncreaseScore(_currentScore - _recordScore, recordScoreText));
                _recordScore = _currentScore;
                PlayerPrefs.SetInt(KeyPlayerPrefs, _recordScore);
            }
        }
        
    }
}
