using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;  // Photonライブラリを使用するための名前空間
using Photon.Realtime; 

namespace Complete
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public int m_NumRoundsToWin = 5;            // 1プレイヤーがゲームに勝つためのラウンド数
        public float m_StartDelay = 3f;             // RoundStarting と RoundPlaying の間の遅延
        public float m_EndDelay = 3f;               // RoundPlaying と RoundEnding の間の遅延
        public CameraControl m_CameraControl;       // カメラ制御用スクリプトへの参照
        public Text m_MessageText;                  // 終了時のメッセージを表示するUIテキスト
        public GameObject m_TankPrefab;
        public GameObject m_TankPrefab_for_TPS;     // プレイヤーが操作するタンクのプレハブ
        public TankManager[] m_Tanks;               // タンク管理者の配列（各タンクの管理を行う）

        private int m_RoundNumber;                  // 現在のラウンド番号
        private WaitForSeconds m_StartWait;         // ラウンド開始までの待機時間
        private WaitForSeconds m_EndWait;           // ラウンド終了後の待機時間
        private TankManager m_RoundWinner;          // 現ラウンドの勝者
        private TankManager m_GameWinner;           // ゲームの勝者

        public enum GameState // ゲーム状態を示す列挙型
        {
            RoundStarting,  // ゲームの開始処理
            RoundPlaying,   // ゲームのプレイ中
            RoundEnding     // ゲームの終了処理
        }

        public GameState currentGameState; // 現在のゲーム状態
        public event Action<GameState> OnGameStateChanged; // ゲーム状態が変更された時に発火するイベント

        public void SetGameState(GameState newState)
        {
            if (currentGameState != newState)
            {
                currentGameState = newState;
                OnGameStateChanged?.Invoke(newState); // ゲーム状態が変更されたらイベントを発火
            }
        }

        private void Start()
        {
            // ゲーム開始時の待機時間と終了時の待機時間を設定
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            // タンクを生成し、カメラターゲットを設定
            SpawnAllTanks();
            SetCameraTargets();

            // ゲームループを開始
            StartCoroutine(GameLoop());
        }

        private void SpawnAllTanks()
        {
            int localPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

            // ローカルプレイヤーが1番の場合のみ、タンク0を生成
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                // 自分のタンクを生成
                if (localPlayerNumber == i + 1)
                {
                    // すでにタンクが生成されている場合はスキップ
                    if (m_Tanks[i].m_Instance == null)
                    {
                        GameObject tankInstance = PhotonNetwork.Instantiate(m_TankPrefab_for_TPS.name, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation);
                        if (tankInstance != null)
                        {
                            m_Tanks[i].m_Instance = tankInstance;
                            m_Tanks[i].m_PlayerNumber = i + 1;
                            m_Tanks[i].Setup();
                            m_Tanks[i].m_Instance.GetComponent<TankHealth>().m_Slider = GameObject.Find("Player" + (i + 1) + "Slider").GetComponent<Slider>();
                        }
                        else
                        {
                            Debug.LogError("Failed to instantiate tank for player " + (i + 1));
                        }
                    }
                }
                else
                {
                    // 対戦相手のタンクが生成されている場合、同期を確認
                    if (m_Tanks[i].m_Instance == null)
                    {
                        // 対戦相手がタンクを生成しているかチェック
                        GameObject tankInstance = PhotonNetwork.Instantiate(m_TankPrefab_for_TPS.name, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation);
                        if (tankInstance != null)
                        {
                            m_Tanks[i].m_Instance = tankInstance;
                            m_Tanks[i].m_PlayerNumber = i + 1;
                            m_Tanks[i].Setup();
                            m_Tanks[i].m_Instance.GetComponent<TankHealth>().m_Slider = GameObject.Find("Player" + (i + 1) + "Slider").GetComponent<Slider>();
                        }
                        else
                        {
                            Debug.LogError("Failed to instantiate tank for opponent " + (i + 1));
                        }
                    }
                }
            }
        }



        private void SetCameraTargets()
        {
            // カメラのターゲットとなるタンクの位置を設定
            Transform[] targets = new Transform[m_Tanks.Length];
            m_CameraControl.m_Targets = targets;
        }

        // ゲームループ。各フェーズ（RoundStarting, RoundPlaying, RoundEnding）を順番に実行
        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            if (m_GameWinner != null)
            {
                SceneManager.LoadScene(Scenenames.TitleScene); // ゲームの勝者が決まったらタイトル画面に戻る
            }
            else
            {
                StartCoroutine(GameLoop()); // ゲームが続く場合はループを再開
            }
        }

        private IEnumerator RoundStarting()
        {
            SetGameState(GameState.RoundStarting); // ゲーム状態を更新

            ResetAllTanks(); // タンクをリセットし、操作を無効化
            DisableTankControl();

            m_RoundNumber++;
            m_MessageText.text = "ROUND " + m_RoundNumber;

            yield return m_StartWait; // ラウンド開始まで待機
        }

        private IEnumerator RoundPlaying()
        {
            SetGameState(GameState.RoundPlaying); // ゲーム状態を更新

            EnableTankControl(); // タンクの操作を有効化
            m_MessageText.text = string.Empty;

            // 1台のタンクが残るまでループ
            while (!OneTankLeft())
            {
                yield return null;
            }
        }

        private IEnumerator RoundEnding()
        {
            SetGameState(GameState.RoundEnding); // ゲーム状態を更新

            DisableTankControl(); // タンクの操作を無効化

            m_RoundWinner = GetRoundWinner(); // ラウンド勝者を決定

            if (m_RoundWinner != null)
                m_RoundWinner.m_Wins++;

            m_GameWinner = GetGameWinner(); // ゲーム全体の勝者を決定

            string message = EndMessage();
            m_MessageText.text = message;

            yield return m_EndWait; // ラウンド終了まで待機

            if (m_GameWinner == null)
            {
                StartCoroutine(GameLoop()); // 次のラウンドへ
            }
        }

        private bool OneTankLeft()
        {
            int numTanksLeft = 0;
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance.activeSelf)
                    numTanksLeft++;
            }
            return numTanksLeft <= 1;
        }

        private TankManager GetRoundWinner()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance.activeSelf)
                    return m_Tanks[i];
            }
            return null;
        }

        private TankManager GetGameWinner()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                    return m_Tanks[i];
            }
            return null;
        }

        private string EndMessage()
        {
            string message = "DRAW!";
            if (m_RoundWinner != null)
                message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
            message += "\n\n\n\n";

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
            }

            if (m_GameWinner != null)
                message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

            return message;
        }

        private void ResetAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].Reset();
            }
        }

        private void EnableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].EnableControl();
            }
        }

        private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].DisableControl();
            }
        }

        // プレイヤーが退出した際の処理
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            m_RoundWinner = m_Tanks[0].m_Instance.activeSelf ? m_Tanks[0] : m_Tanks[1];
            m_MessageText.text = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
            StartCoroutine(ReturnToTitle());
        }

        private IEnumerator ReturnToTitle()
        {
            yield return new WaitForSeconds(5f);
            PhotonNetwork.LoadLevel(Scenenames.TitleScene); // タイトルシーンに遷移
        }
    }
}
