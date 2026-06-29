using System;

namespace WesternDeckBuilder.Core
{
    /// <summary>Identifies the type of input device currently driving the UI.</summary>
    public enum InputDeviceType
    {
        Unknown,
        Touch,
        Mouse,
        Gamepad,
        Keyboard
    }

    /// <summary>
    /// Device-aware input facade. Exposes high-level game actions as events so UI
    /// components never reference the Input System directly.
    /// Supports Android touch, PC mouse/keyboard, and generic gamepad (Xbox mapping).
    /// </summary>
    public interface IInputRouter
    {
        /// <summary>The type of input device that most recently produced input.</summary>
        InputDeviceType ActiveDevice { get; }

        /// <summary>Fires when the active input device type changes (e.g. player picks up a controller).</summary>
        event Action<InputDeviceType> OnDeviceChanged;

        /// <summary>Confirm / A / tap.</summary>
        event Action OnSubmit;
        /// <summary>Back / B / swipe-back.</summary>
        event Action OnCancel;
        /// <summary>Open overworld map.</summary>
        event Action OnOpenMap;
        /// <summary>Open loadout / inventory.</summary>
        event Action OnOpenLoadout;
        /// <summary>Select a card by hand index (0-based).</summary>
        event Action<int> OnCardSelect;
        /// <summary>Cycle target selection. +1 = next, -1 = previous.</summary>
        event Action<int> OnTargetCycle;
    }
}
