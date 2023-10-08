using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class MatchMaker : MonoBehaviour
	{
        [SerializeField] private NetworkRunnerSpawner _networkRunnerSpawner;

        [SerializeField] private TMP_Dropdown _dropDown;
        [SerializeField] private int _testRating;
        [SerializeField] private int _startOffset;
        [SerializeField] private int _step;
        [SerializeField] private int _timeToChangeOffset;
        [SerializeField] private int _maxOffset;

        public bool IsLookingMatch { get; private set; }

        private List<SessionInfo> _sessions = new();
        
        private bool _isFresh;

        private Coroutine _findGameCorun;

        private void OnEnable()
        {
            NetworkRunnerSpawner.SessionsUpdated += OnSessionsUpdated;
        }

        private void OnDisable()
        {
            NetworkRunnerSpawner.SessionsUpdated -= OnSessionsUpdated;
        }

        public void StartLookingGame()
        {
            if (_findGameCorun != null)
            {
                StopCoroutine(_findGameCorun);
            }
            IsLookingMatch = true;
            _findGameCorun = StartCoroutine(FindGameCorun());
        }

        public void StopLookingGame()
        {
            if (_findGameCorun != null)
            {
                StopCoroutine(_findGameCorun);
                _networkRunnerSpawner.Reconnect();
            }
            IsLookingMatch = false;
        }

        private IEnumerator FindGameCorun()
        {
            float currentTime = 0f;
            int currentOffset = _startOffset;
            int myRating = PlayerPrefs.GetInt(Constants.Rating, _testRating);
            string name = PlayerPrefs.GetString(Constants.Name, "Annonymys");

            while (true)
            {
                if (_sessions.Count == 0 && _networkRunnerSpawner.Runner.SessionInfo == null)
                {
                    _networkRunnerSpawner.JoinRatigGame(GameMode.Shared, name, _dropDown.captionText.text, myRating);
                }
                if (_isFresh && _networkRunnerSpawner.Runner.SessionInfo == null)
                {
                    foreach (var session in _sessions)
                    {
                        _isFresh = false;
                        int rating = (int)session.Properties[Constants.Rating].PropertyValue;
                        if (rating <= myRating + currentOffset && rating >= myRating - currentOffset)
                        {
                            _networkRunnerSpawner.JoinRatigGame(GameMode.Shared, session.Name, session.Region, rating);
                            yield break;
                        }
                        currentTime += Time.deltaTime;
                        if (currentTime > _timeToChangeOffset)
                        {
                            currentOffset += _step;
                            currentTime = 0f;
                            _isFresh = true;

                            if (currentOffset > _maxOffset)
                            {
                                _networkRunnerSpawner.JoinRatigGame(GameMode.Shared, name, _dropDown.captionText.text, myRating);
                                break;
                            }
                        }
                    }
                }
                else if(_networkRunnerSpawner.Runner.SessionInfo && _networkRunnerSpawner.Runner.SessionInfo.PlayerCount == 2)
                {
                    _networkRunnerSpawner.Runner.SetActiveScene((SceneRef)1);
                }
                yield return null;
            }
            
        }

        private void OnSessionsUpdated(List<SessionInfo> sessions)
        {
            _sessions = sessions.Where(x=>x.Properties.Count > 0).ToList();
            _isFresh = true;
        }
    }
}
