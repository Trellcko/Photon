using Fusion;
using Fusion.Photon.Realtime;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trellcko.MonstersVsMonsters.Core
{
    public class NetworkRunnerSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private string _gameSceneName;
        public NetworkRunner Runner { get; private set; }

        public static event Action<PlayerRef> LeaveSession;
        public static event Action JoinedToSession;
        public static event Action JoinedToLobby;

        public static event Action<List<SessionInfo>> SessionsUpdated;

      
        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            Debug.Log("Cannot connect");
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
            LeaveSession?.Invoke(player);
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
            print("Updated");
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

            var task = await Runner.JoinSessionLobby(SessionLobby.Custom, "LobbyId");
            if(task.Ok)
            {
                JoinedToLobby?.Invoke();
            }
        }

        public async void Reconnect()
        {
            await Runner.Shutdown();
            JoinLobby();
        }

        public async void JoinCustomGame(GameMode mode, string roomName, string region)
        {
            
            await StartSession(mode, roomName, region, Constants.Custom).ContinueWith(x => { Runner.SetActiveScene((SceneRef)1); });
        }

       
        public async void JoinRatigGame(GameMode mode, string roomName, string region, int rating)
        {
            Dictionary<string, SessionProperty> sessionProperties = new()
            {
                { Constants.Rating, SessionProperty.Convert(rating) }
            };

            await StartSession(mode, roomName, region, Constants.Rating, sessionProperties);
        }

        private async Task StartSession(GameMode mode, string roomName, string region,
           string lobbyName, Dictionary<string, SessionProperty> sessionProperties = null)
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            SetRunner();
            Runner.ProvideInput = true;


            AppSettings customAppSetting = BuildCustomAppSetting(region);
            await Runner.StartGame(new StartGameArgs()
            {
                PlayerCount = 2,
                GameMode = mode,
                SessionName = roomName,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                CustomLobbyName = lobbyName,
                CustomPhotonAppSettings = customAppSetting,
                SessionProperties = sessionProperties

            });
        }

        private AppSettings BuildCustomAppSetting(string region, string customAppID = null, string appVersion = "1.0.0")
        {

            var appSettings = PhotonAppSettings.Instance.AppSettings.GetCopy();

            appSettings.UseNameServer = true;
            appSettings.AppVersion = appVersion;

            if (string.IsNullOrEmpty(customAppID) == false)
            {
                appSettings.AppIdFusion = customAppID;
            }

            if (string.IsNullOrEmpty(region) == false)
            {
                appSettings.FixedRegion = region.ToLower();
            }

            return appSettings;
        }


        private NetworkRunner SetRunner()
        {
            if (Runner)
            {
                return Runner;
            }
            NetworkRunner networkRunner;
            if (!TryGetComponent(out networkRunner))
            {
                Runner = gameObject.AddComponent<NetworkRunner>();
            }
            else
            {
                Runner = networkRunner;
            }
            return Runner;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
        }
    }
}