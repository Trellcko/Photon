using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class MatchMaker : NetworkBehaviour
	{
        [SerializeField] private NetworkRunnerSpawner _networkRunnerSpawner;

        [SerializeField] private TMP_Dropdown _dropDown;
        [SerializeField] private int _testRating;
        [SerializeField] private int _startOffset;
        [SerializeField] private int _step;
        [SerializeField] private int _timeToChangeOffset;
        [SerializeField] private int _maxOffset;

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

        public void StartFindingGame()
        {
            if (_findGameCorun != null)
            {
                StopCoroutine(_findGameCorun);
            }
            _findGameCorun = StartCoroutine(FindGameCorun());
        }

        public void StopFindingGame()
        {
            StopCoroutine(_findGameCorun);
            if (Runner)
            {
                Runner.Shutdown();
            }
        }

        private IEnumerator FindGameCorun()
        {
            float currentTime = 0f;
            int currentOffset = _startOffset;
            int myRating = PlayerPrefs.GetInt(Constants.Rating, _testRating);
            string name = PlayerPrefs.GetString(Constants.Name, "Annonymys");

            yield return null;
            if(_sessions.Count == 0)
            {
                
                _networkRunnerSpawner.JoinRatigGame(GameMode.Shared, name, _dropDown.captionText.text, myRating);
                yield break;
            }
            if (_isFresh && Runner.SessionInfo == null)
            {
                foreach(var session in _sessions)
                {
                    _isFresh = false;
                    int rating = (int)session.Properties[Constants.Rating].PropertyValue;
                    if (rating <= myRating + currentOffset && rating >= myRating - currentOffset)
                    {
                        _networkRunnerSpawner.JoinRatigGame(GameMode.Shared, session.Name, session.Region, rating);
                        yield break;
                    }
                    currentTime += Time.deltaTime;
                    if(currentTime > _timeToChangeOffset)
                    {
                        currentOffset += _step;
                        currentTime = 0f;
                        _isFresh = true;

                        if(currentOffset > _maxOffset)
                        {
                            _networkRunnerSpawner.JoinRatigGame(GameMode.Shared, name, _dropDown.captionText.text, myRating);
                            yield break;
                        }
                    }
                }
            }
            
        }

        private void OnSessionsUpdated(List<SessionInfo> sessions)
        {
            _sessions = sessions.Where(x=>x.Properties.Count > 0).ToList();
            _isFresh = true;
        }
    }
}
