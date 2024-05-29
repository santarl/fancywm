using System;
using System.Collections.Generic;
using System.Linq;

using FancyWM.Utilities;

namespace FancyWM.Models
{
    public class ActivationHotkey
    {
        public KeyCode[] ModifierKeys { get; }
        public KeyCode Key { get; }
        public string Description { get; }

        public static IReadOnlyList<ActivationHotkey> AllowedHotkeys { get; } =
        [
            new([KeyCode.LWin], "LWin ⊞"),
            new([KeyCode.LeftAlt], "LAlt"),
            new([KeyCode.LeftCtrl], "LCtrl"),
            new([KeyCode.LeftShift], "LShift ⇧"),
            new([KeyCode.LeftAlt], KeyCode.LWin, "LAlt + LWin ⊞"),
            new([KeyCode.LeftAlt], KeyCode.LeftCtrl, "LAlt + LCtrl"),
            new([KeyCode.LeftAlt], KeyCode.LeftShift, "LAlt + LShift ⇧"),
            new([KeyCode.LeftCtrl], KeyCode.LWin, "Ctrl + LWin ⊞"),
            new([KeyCode.LeftCtrl], KeyCode.LeftShift, "Ctrl + LShift ⇧"),
            new([KeyCode.LeftShift], KeyCode.LWin, "Shift ⇧ + LWin ⊞"),
            new([KeyCode.LeftAlt, KeyCode.LeftCtrl], KeyCode.LWin, "LAlt + LCtrl + LWin ⊞"),
            new([KeyCode.LeftAlt, KeyCode.LeftCtrl], KeyCode.LeftShift, "LAlt + LCtrl + LShift ⇧"),
            new([KeyCode.LeftAlt, KeyCode.LeftShift], KeyCode.LWin, "LAlt + LShift ⇧ + LWin ⊞"),
            new([KeyCode.LeftCtrl, KeyCode.LeftShift], KeyCode.LWin, "Ctrl + LShift ⇧ + LWin ⊞"),
            new([KeyCode.None], KeyCode.None, "Disabled"),
        ];

        public static ActivationHotkey Default { get; } = AllowedHotkeys.First(x => x.Description == "Shift ⇧ + LWin ⊞");

        private ActivationHotkey(KeyCode[] modifierKeys, KeyCode key, string description)
        {
            ModifierKeys = [..modifierKeys.OrderBy(x => (int)x)];
            Key = key;
            Description = description;
        }

        public override bool Equals(object? obj)
        {
            return obj is ActivationHotkey hotkey &&
                   ModifierKeys.Equals(hotkey.ModifierKeys) &&
                   Key == hotkey.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ModifierKeys, Key);
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
