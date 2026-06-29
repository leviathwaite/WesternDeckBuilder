using System;
using UnityEngine;

// Guard all new Input System API behind the compile symbol that Unity sets
// when the Input System package is active.
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace WesternDeckBuilder.Core
{
    /// <summary>
    /// Wraps Unity's Input System and exposes device-aware game-action events.
    /// Created and owned by <see cref="GameBootstrap"/>; do not place manually in scenes.
    /// <para>
    /// When the Input System package (com.unity.inputsystem) is installed and the active
    /// input handler includes it, full device-change detection is enabled. Otherwise the
    /// component falls back to the legacy <c>Input</c> class for Submit/Cancel only.
    /// </para>
    /// </summary>
    [DefaultExecutionOrder(-900)]
    public class InputRouter : MonoBehaviour, IInputRouter
    {
        public InputDeviceType ActiveDevice { get; private set; } = InputDeviceType.Unknown;

        public event Action<InputDeviceType> OnDeviceChanged;
        public event Action OnSubmit;
        public event Action OnCancel;
        public event Action OnOpenMap;
        public event Action OnOpenLoadout;
        public event Action<int> OnCardSelect;
        public event Action<int> OnTargetCycle;

        private void Awake()
        {
            DetectInitialDevice();
        }

        private void OnEnable()
        {
#if ENABLE_INPUT_SYSTEM
            InputSystem.onActionChange += HandleActionChange;
#endif
        }

        private void OnDisable()
        {
#if ENABLE_INPUT_SYSTEM
            InputSystem.onActionChange -= HandleActionChange;
#endif
        }

        private void Update()
        {
#if !ENABLE_INPUT_SYSTEM
            PollLegacyInput();
#endif
        }

        // ── Device detection ───────────────────────────────────────────────────

        private void DetectInitialDevice()
        {
#if UNITY_ANDROID || UNITY_IOS
            SetDevice(InputDeviceType.Touch);
#elif ENABLE_INPUT_SYSTEM
            if (Gamepad.current != null)
                SetDevice(InputDeviceType.Gamepad);
            else
                SetDevice(InputDeviceType.Mouse);
#else
            SetDevice(InputDeviceType.Mouse);
#endif
        }

        private void SetDevice(InputDeviceType device)
        {
            if (ActiveDevice == device) return;
            ActiveDevice = device;
            Debug.Log($"[InputRouter] Active device: {device}");
            OnDeviceChanged?.Invoke(device);
        }

#if ENABLE_INPUT_SYSTEM
        private void HandleActionChange(object obj, InputActionChange change)
        {
            if (change != InputActionChange.ActionPerformed) return;
            if (!(obj is InputAction action)) return;

            var device = action.activeControl?.device;
            if (device is Touchscreen)   SetDevice(InputDeviceType.Touch);
            else if (device is Gamepad)  SetDevice(InputDeviceType.Gamepad);
            else if (device is Mouse)    SetDevice(InputDeviceType.Mouse);
            else if (device is Keyboard) SetDevice(InputDeviceType.Keyboard);
        }
#endif

        // ── Legacy input fallback ──────────────────────────────────────────────

        private void PollLegacyInput()
        {
            if (Input.GetButtonDown("Submit")) OnSubmit?.Invoke();
            if (Input.GetButtonDown("Cancel")) OnCancel?.Invoke();
        }

        // ── Manual raise helpers (called by UI code) ───────────────────────────

        /// <summary>Raise a card-select event for the given hand index (0-based).</summary>
        public void RaiseCardSelect(int index)   => OnCardSelect?.Invoke(index);
        /// <summary>Raise a target-cycle event. +1 = next target, -1 = previous.</summary>
        public void RaiseTargetCycle(int direction) => OnTargetCycle?.Invoke(direction);
        /// <summary>Raise the open-map action (called by a UI button).</summary>
        public void RaiseOpenMap()               => OnOpenMap?.Invoke();
        /// <summary>Raise the open-loadout action (called by a UI button).</summary>
        public void RaiseOpenLoadout()           => OnOpenLoadout?.Invoke();
        /// <summary>Raise the submit action programmatically.</summary>
        public void RaiseSubmit()                => OnSubmit?.Invoke();
        /// <summary>Raise the cancel action programmatically.</summary>
        public void RaiseCancel()                => OnCancel?.Invoke();
    }
}
