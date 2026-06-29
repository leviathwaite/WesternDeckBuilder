using UnityEngine;
using WesternDeckBuilder.Data;

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Entry point MonoBehaviour for the game.
    /// Place on a GameObject named <c>[GameBootstrap]</c> in the <b>Boot</b> scene.
    /// <para>
    /// On <c>Awake</c> it initialises all core services and registers them with
    /// <see cref="ServiceRegistry"/>. On <c>Start</c> it navigates to the Loadout scene.
    /// The GameObject is marked DontDestroyOnLoad so services survive scene transitions.
    /// </para>
    /// No inspector references are required — all services are created in code.
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            // Prevent duplicate bootstraps when re-entering Boot scene in the Editor.
            var existing = FindObjectsOfType<GameBootstrap>();
            if (existing.Length > 1)
            {
                Debug.LogWarning("[GameBootstrap] Duplicate detected — destroying extra instance.");
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            InitializeServices();
        }

        private void Start()
        {
            Services.UIFlow?.NavigateTo(SceneNames.Loadout);
        }

        private void InitializeServices()
        {
            var registry = ServiceRegistry.Instance;

            // ── Data registry (first — other services may depend on it) ────────
            var dataRegistry = new DataRegistry();
            dataRegistry.LoadAll();
            registry.Register<IDataRegistry>(dataRegistry);

            // ── Core stateless/plain-C# services ──────────────────────────────
            registry.Register<IRunManager>(new RunManager());
            registry.Register<IHeatManager>(new HeatManager());
            registry.Register<IUIFlowService>(new UIFlowService());
            registry.Register<IRewardService>(new RewardService());

            // ── InputRouter needs a MonoBehaviour host ─────────────────────────
            var inputHost = new GameObject("[InputRouter]");
            DontDestroyOnLoad(inputHost);
            var inputRouter = inputHost.AddComponent<InputRouter>();
            registry.Register<IInputRouter>(inputRouter);

            // ── Startup validation ─────────────────────────────────────────────
            StartupValidator.Run(dataRegistry);

            Debug.Log("[GameBootstrap] All services initialised successfully.");
        }
    }
}
