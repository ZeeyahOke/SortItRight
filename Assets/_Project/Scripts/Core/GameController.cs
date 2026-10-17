using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameStateMachine stateMachine;
    [SerializeField] private UIManager ui;
    [SerializeField] private ARPlacementManager placement;
    [SerializeField] private PlacementReticle reticle;
    [SerializeField] private ScenarioLoader loader;
    [SerializeField] private SimulationManager simulation;
    [SerializeField] private List<ScenarioData> scenarios = new List<ScenarioData>();

    public GameStateMachine StateMachine => stateMachine;
    public UIManager UI => ui;
    public ARPlacementManager Placement => placement;
    public PlacementReticle Reticle => reticle;
    public ScenarioLoader Loader => loader;
    public SimulationManager Simulation => simulation;
    public IReadOnlyList<ScenarioData> Scenarios => scenarios;
    public ScenarioData SelectedScenario { get; set; }

    public StartState StartState { get; private set; }
    public SetupState SetupState { get; private set; }
    public ScenarioSelectState ScenarioSelectState { get; private set; }
    public SimulationState SimulationState { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        StartState = new StartState(this);
        SetupState = new SetupState(this);
        ScenarioSelectState = new ScenarioSelectState(this);
        SimulationState = new SimulationState(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(StartState);
    }
}
