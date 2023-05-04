using System;

namespace Scorewarrior.Test.Utility
{
    [Serializable]
    public class EnumDictionary<TKey, TValue>
    {
        public TKey[] keys;
        public TValue[] values;

        public EnumDictionary()
        {
            keys = Enum.GetValues(typeof(TKey)) as TKey[];
            values = new TValue[Enum.GetValues(typeof(TKey)).Length];
        }

        public TValue this[TKey key]
        {
            get => values[Convert.ToInt32(key)];
            set => values[Convert.ToInt32(key)] = value;
        }
    }
}