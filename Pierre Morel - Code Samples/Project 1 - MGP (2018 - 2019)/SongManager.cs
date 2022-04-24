using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _songZoneCenter;
    [SerializeField] private SongGameplay _songGameplay;

    private enum Action
    {
        Idle,
        WaitStartSongEffects,
        Song,
        WaitEndSongEffects
    }
    private Action _action;
    static private SongManager _instance;

    private bool _cameraReady;
    private bool _actorsMoverReady;
    private Enemy _attackedEnemy;
    //private GameManager _gManager;
    #endregion

    #region Properties
    static public SongManager Instance
    {
        get { return _instance; }
    }

    public Transform SongZoneCenter
    {
        get { return _songZoneCenter; }
    }
    #endregion

    #region Methods
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //_gManager = GameManager.Instance;
        _action = Action.Idle;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSong(Enemy enemy)
    {
        Debug.Log("COOL, start song");

        // Teleport Camera and Actors
        _action = Action.WaitStartSongEffects;
        GameManager.Instance.SetState(GameManager.Gamestate.song);
        EventsManager.FireSongStartEvent(enemy);
        _attackedEnemy = enemy;
    }

    private void StartSongGameplay()
    {
        _songGameplay.StartGameplay(_attackedEnemy);
        _action = Action.WaitEndSongEffects;
    }

    public void EndSong()
    {
        GameManager.Instance.SetState(GameManager.Gamestate.playing);
        EventsManager.FireSongEndEvent();
    }

    public void EndSongGameplay()
    {
        Debug.Log("OK, END song GAMEPLAY");
        EventsManager.FireSongGameplayEndEvent();
    }

    public void OnCameraReady()
    {
        Debug.Log("CAMERA READY");
        _cameraReady = true;
        CheckReadyState();
    }

    public void OnActorsMoversReady()
    {
        Debug.Log("MOVER READY");
        _actorsMoverReady = true;
        CheckReadyState();
    }

    private void CheckReadyState()
    {
        if (_cameraReady == false || _actorsMoverReady == false)
            return;

        _cameraReady = false;
        _actorsMoverReady = false;

        switch (_action)
        {
            case Action.WaitStartSongEffects:
                Debug.Log("START SONG GAMEPLAY");
                StartSongGameplay();
                break;
            case Action.WaitEndSongEffects:
                Debug.Log("END SONG");
                EndSong();
                break;
        }
    }
    #endregion
}
