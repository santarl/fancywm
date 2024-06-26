﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FancyWM.Utilities;

namespace FancyWM.Models
{
    public class Keybinding(IReadOnlySet<KeyCode> keys, bool isDirectMode) : IEquatable<Keybinding>
    {
        public IReadOnlySet<KeyCode> Keys { get; } = keys ?? throw new ArgumentNullException(nameof(keys));
        public bool IsDirectMode { get; } = isDirectMode;

        public bool Equals(Keybinding? other)
        {
            if (other == null)
                return false;

            return other.Keys.SequenceEqual(Keys)
                && other.IsDirectMode == IsDirectMode;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Keybinding);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Keys, IsDirectMode);
        }
    }

    public class KeybindingDictionary : Dictionary<BindableAction, Keybinding?>
    {
        public KeybindingDictionary(bool useDefaults = true)
        {
            if (useDefaults)
            {
                var keybindingSet = new HashSet<IReadOnlySet<KeyCode>?>(EqualityComparer<KeyCode>.Default.ToSequenceComparer());
                var members = typeof(BindableAction).GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (var member in members)
                {
                    var keys = member.GetCustomAttribute<DefaultKeybindingAttribute>()!.Keys.ToHashSet();
                    if (keybindingSet.Add(keys))
                    {
                        var keybinding = new Keybinding(keys, false);
                        Add((BindableAction)member.GetValue(null)!, keybinding);
                    }
                }
            }
        }

        public KeybindingDictionary(IEnumerable<KeyValuePair<BindableAction, Keybinding?>> collection) : base(collection)
        {
        }
    }
}
