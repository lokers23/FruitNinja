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

        [SerializeField] private Text scoreText;
        [SerializeField] private Text recordScoreText;
        [SerializeField] private float upDelay;

        public TextMeshPro comboText;

        private int _currentScore = 0;
        private int _recordScore;
        private int _scoreCombo = 0;
        private float _comboDelay = 0.2f;
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
            if (Time.time - _lastTimeSlice > _comboDelay)
            {
                if (_scoreCombo > 2)
                {
                    _textForCombo.Append("<align=\"left\"><size=4>");
                    _textForCombo.Append(_scoreCombo);
                    _textForCombo.Append(" фрукта</size></align>\nсерия\nx");
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
            
            HighScore();
            recordScoreText.text = _recordScore.ToString();
            StartCoroutine(IncreaseScore(score));
        }
    
        private IEnumerator IncreaseScore(int newScore)
        {
            for (int i = 1; i < newScore + 1; i++)
            {
                scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
                yield return new WaitForSeconds(upDelay);
            }
        }

        private void HighScore()
        {
            if (_currentScore > _recordScore)
            {
                _recordScore = _currentScore;
                PlayerPrefs.SetInt(KeyPlayerPrefs, _recordScore);
            }
        }
        
    }
}
