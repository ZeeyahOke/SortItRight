using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private ConveyorBelt belt;
    [SerializeField] private WasteItemFactory factory;
    [SerializeField] private Bin[] bins;

    private readonly SessionStats stats = new SessionStats();
    private SessionRecorder recorder;
    private ScenarioData scenario;

    public SessionStats Stats => stats;
    public IReadOnlyList<Bin> Bins => bins;
    public bool IsRunning { get; private set; }
    public float ElapsedSeconds { get; private set; }
    public int ItemsLeft => scenario == null ? 0 : scenario.SessionItemCount - stats.ItemsHandled;

    private void Awake()
    {
        recorder = new SessionRecorder(stats);
    }

    public void StartSession(ScenarioData data, IReadOnlyDictionary<string, GameObject> models)
    {
        scenario = data;
        stats.Reset();
        ElapsedSeconds = 0f;

        foreach (Bin bin in bins)
        {
            bin.ResetBin();
        }

        recorder.StopListening();
        recorder.StartListening();
        factory.SetModels(models);
        belt.StartBelt(data);
        IsRunning = true;
        EventBus.Publish(new SessionStartedEvent(data.ScenarioId));
    }

    public void StopSession()
    {
        IsRunning = false;
        recorder.StopListening();
        belt.StopBelt();
        factory.SetModels(null);
    }

    private void OnDisable()
    {
        if (IsRunning)
        {
            StopSession();
        }
    }

    private void Update()
    {
        if (!IsRunning)
        {
            return;
        }

        ElapsedSeconds += Time.deltaTime;
        bool failed = SessionRules.HasFailed(stats);

        if (failed || SessionRules.IsComplete(stats, scenario.SessionItemCount))
        {
            EndSession(failed);
        }
    }

    private void EndSession(bool failed)
    {
        StopSession();
        SessionOutcome outcome = stats.Outcome(failed);
        EventBus.Publish(new SessionEndedEvent(scenario.ScenarioId, outcome, ElapsedSeconds, stats));
    }
}
