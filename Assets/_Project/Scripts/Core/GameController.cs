using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameStateMachine stateMachine;
    [SerializeField] private UIManager ui;
    [SerializeField] private ARPlacementManager placement;
    [SerializeField] private PlacementReticle reticle;

    public GameStateMachine StateMachine => stateMachine;
    public UIManager UI => ui;
    public ARPlacementManager Placement => placement;
    public PlacementReticle Reticle => reticle;

    public StartState StartState { get; private set; }
    public SetupState SetupState { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        StartState = new StartState(this);
        SetupState = new SetupState(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(StartState);
    }
}
