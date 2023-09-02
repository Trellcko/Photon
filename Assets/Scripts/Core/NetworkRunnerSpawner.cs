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

        //private void OnGUI()
        //{
        //    if (_runner == null)
        //    {
        //        if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
        //        {
        //            JoinSession(GameMode.Shared, "TEST ROOM", _gameSceneName);
        //        }
        //        if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
        //        {
        //            JoinSession(GameMode.Shared, "", _gameSceneName);
        //        }
        //    }
        //}

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
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public async void JoinLobby()
        {
            await _runner.JoinSessionLobby(SessionLobby.Custom, "LobbyId");
        }

        public async void JoinGame(GameMode mode, string roomName, string sceneName)
        {
            NetworkRunner networkRunner = null;
            if (!TryGetComponent(out networkRunner))
            {
                _runner = gameObject.AddComponent<NetworkRunner>();
            }
            else
            {
                _runner = networkRunner;
            }
            _runner.ProvideInput = true;
            
            await _runner.StartGame(new StartGameArgs()
            {
                PlayerCount = 2,
                GameMode = mode,
                SessionName = roomName,
                Scene = SceneManager.GetSceneByName(sceneName).buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                CustomLobbyName = "LobbyID"
                
            });
         
        }
    }
}