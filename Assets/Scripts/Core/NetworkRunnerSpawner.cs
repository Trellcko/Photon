using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trellcko.MonstersVsMonsters.Core
{
    public class NetworkRunnerSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private string _gameSceneName;

        private NetworkRunner _runner;

        public event Action JoinedToSession;
        public event Action JoinedToLobby;

        public event Action<List<SessionInfo>> SessionsUpdated;

      
        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            Debug.Log("Cannot, connect");
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
            Debug.Log("Disconnected");
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            JoinedToSession?.Invoke();
            Debug.Log("Joined");
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Left");
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            SessionsUpdated?.Invoke(sessionList);
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public async void JoinLobby()
        {
            SetRunner();

            var task = await _runner.JoinSessionLobby(SessionLobby.Custom, "LobbyId");
            if(task.Ok)
            {
                JoinedToLobby?.Invoke();
            }
        }

        public async void JoinGame(GameMode mode, string roomName)
        {
            print("start joining to the session");
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            SetRunner();
            _runner.ProvideInput = true;

            await _runner.StartGame(new StartGameArgs()
            {
                PlayerCount = 2,
                GameMode = mode,
                SessionName = roomName,
                Scene =(SceneRef)1,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                CustomLobbyName = "LobbyID"
            });
        }

        private NetworkRunner SetRunner()
        {
            if (_runner)
            {
                return _runner;
            }
            NetworkRunner networkRunner = null;
            if (!TryGetComponent(out networkRunner))
            {
                _runner = gameObject.AddComponent<NetworkRunner>();
            }
            else
            {
                _runner = networkRunner;
            }
            return _runner;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
        }
    }
}