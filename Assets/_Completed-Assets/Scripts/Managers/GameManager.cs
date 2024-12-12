using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime; 

namespace Complete
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
        public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
        public GameObject m_TankPrefab;
        public GameObject m_TankPrefab_for_TPS;     // Reference to the prefab the players will control.
        public TankManager[] m_Tanks;               // A collection of managers for enabling and disabling different aspects of the tanks.

        private int m_RoundNumber;                  // Which round the game is currently on.
        private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
        private TankManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
        private TankManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.

        public enum GameState // 状態を表す列挙型
        {
            RoundStarting,  // ゲームの開始処理
            RoundPlaying,   // ゲームのプレイ中
            RoundEnding     // ゲームの終了処理
        }

        public GameState currentGameState; // 現在のゲーム状態を保持する変数

        public event Action<GameState> OnGameStateChanged; // 状態変更時に発生するイベント
    
        public void SetGameState(GameState newState)
        {
            photonView.RPC("SyncGameState", RpcTarget.All, newState);   
        }

        // RPCメソッドで他のクライアントの状態を同期
        [PunRPC]
        private void SyncGameState(GameState newState)
        {
            Debug.Log($"SyncGameState called on {PhotonNetwork.NickName}: {newState}");
            currentGameState = newState;
            OnGameStateChanged?.Invoke(newState);
        }

        private void Start()
        {
            OnGameStateChanged += HandleGameStateChanged;
            PhotonNetwork.AutomaticallySyncScene = true; //画面の自動遷移
            SetGameState(GameState.RoundStarting);

            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            SpawnAllTanks();
            SetCameraTargets();
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(GameLoop());
            }
        }

        private void OnDestroy()
        {
            OnGameStateChanged -= HandleGameStateChanged;
        }

        private void SpawnAllTanks()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogWarning("Not the master client. Skipping tank spawn.");
                return;
            }

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_SpawnPoint == null)
                {
                    Debug.LogError($"Spawn point for tank {i + 1} is missing!");
                    continue;
                }

                m_Tanks[i].m_Instance = PhotonNetwork.Instantiate(
                    "CompleteTank_for_TPS",
                    m_Tanks[i].m_SpawnPoint.position,
                    m_Tanks[i].m_SpawnPoint.rotation
                );

                m_Tanks[i].m_PlayerNumber = i + 1;
                m_Tanks[i].Setup();

                var healthSlider = GameObject.Find($"Player{i + 1}Slider")?.GetComponent<Slider>();
                if (healthSlider != null)
                {
                    m_Tanks[i].m_Instance.GetComponent<TankHealth>().m_Slider = healthSlider;
                }
                else
                {
                    Debug.LogWarning($"Slider for Player {i + 1} not found.");
                }
                if (i == 0)
                {
                    Debug.Log("Assigning local player ownership to Tank 0.");
                    // ローカルプレイヤーは自動的に所有権を持つため特に処理不要
                }
                // リモートプレイヤー用タンク (Player 1)
                else if (i == 1)
                {
                    Debug.Log("Assigning remote player ownership to Tank 1.");
                    // 対戦相手にオーナーシップを移譲
                        
                    Player opponent = PhotonNetwork.PlayerListOthers[0]; // 相手プレイヤーを取得
                    if (photonView.Owner != opponent)
                    {
                        Debug.Log(opponent);
                        photonView.TransferOwnership(opponent);
                    }
                }
                
                
            }
            PhotonView tank0View = m_Tanks[0].m_Instance.GetComponent<PhotonView>();
            if (tank0View != null)
            {
                Debug.Log($"Tank 0 Owner: {tank0View.Owner.NickName} (Player ID: {tank0View.Owner.ActorNumber})");
            }     
            PhotonView tank1View = m_Tanks[1].m_Instance.GetComponent<PhotonView>();
            if (tank1View != null)
            {
                Debug.Log($"Tank 1 Owner: {tank1View.Owner.NickName} (Player ID: {tank1View.Owner.ActorNumber})");
            }
        }
        private void SetCameraTargets()
        {
            if (m_Tanks == null || m_Tanks.Length == 0)
            {
                Debug.LogError("No tanks available for setting camera targets.");
                return;
            }

            Transform[] targets = new Transform[m_Tanks.Length];

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    targets[i] = m_Tanks[i].m_Instance.transform;
                }
            }

            m_CameraControl.m_Targets = targets;
        }

        private void HandleGameStateChanged(GameState newState)
        {
            Debug.Log($"Game state changed to: {newState}");
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            if (m_GameWinner != null)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.LoadLevel("TitleScene");
                }
            }
            else
            {
                StartCoroutine(GameLoop());
            }
        }

        private IEnumerator RoundStarting()
        {
            SetGameState(GameState.RoundStarting);

            ResetAllTanks();
            DisableTankControl();

            m_RoundNumber++;
            m_MessageText.text = $"ROUND {m_RoundNumber}";

            yield return m_StartWait;
        }

        private IEnumerator RoundPlaying()
        {
            SetGameState(GameState.RoundPlaying);

            EnableTankControl();
            m_MessageText.text = string.Empty;

            while (!OneTankLeft())
            {
                yield return null;
            }
        }

        private IEnumerator RoundEnding()
        {
            SetGameState(GameState.RoundEnding);

            DisableTankControl();
            m_RoundWinner = GetRoundWinner();

            if (m_RoundWinner != null)
                m_RoundWinner.m_Wins++;

            m_GameWinner = GetGameWinner();

            m_MessageText.text = EndMessage();

            yield return m_EndWait;
        }

        private bool OneTankLeft()
        {
            int numTanksLeft = 0;

            foreach (var tank in m_Tanks)
            {
                if (tank.m_Instance.activeSelf)
                {
                    numTanksLeft++;
                }
            }

            return numTanksLeft <= 1;
        }

        private TankManager GetRoundWinner()
        {
            foreach (var tank in m_Tanks)
            {
                if (tank.m_Instance.activeSelf)
                {
                    return tank;
                }
            }

            return null;
        }

        private TankManager GetGameWinner()
        {
            foreach (var tank in m_Tanks)
            {
                if (tank.m_Wins == m_NumRoundsToWin)
                {
                    return tank;
                }
            }

            return null;
        }

        private string EndMessage()
        {
            string message = "DRAW!";

            if (m_RoundWinner != null)
            {
                message = $"{m_RoundWinner.m_ColoredPlayerText} WINS THE ROUND!";
            }

            message += "\n\n\n\n";

            foreach (var tank in m_Tanks)
            {
                message += $"{tank.m_ColoredPlayerText}: {tank.m_Wins} WINS\n";
            }

            if (m_GameWinner != null)
            {
                message = $"{m_GameWinner.m_ColoredPlayerText} WINS THE GAME!";
            }

            return message;
        }

        private void ResetAllTanks()
        {
            foreach (var tank in m_Tanks)
            {
                tank.Reset();
            }
        }

        private void EnableTankControl()
        {
            foreach (var tank in m_Tanks)
            {
                tank.EnableControl();
            }
        }

        private void DisableTankControl()
        {
            foreach (var tank in m_Tanks)
            {
                tank.DisableControl();
            }
        }
    }
}