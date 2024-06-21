using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Serializable Map collection for storing values.</summary>
    */
    [Serializable]
    public partial class SerializedDictionary<K,V>
    {
        // MARK: Variables
        /**
        <summary>Internal collection for managing data.</summary>
        */
        private Dictionary<K,V> dictionary = new();
        /**
        <summary>Internal collection for managing key serialization.</summary>
        */
        [SerializeField] List<K> keys = new();
        /**
        <summary>Internal collection for managing value serialization.</summary>
        */
        [SerializeField] List<V> values = new();

        // MARK: Initializers
        public SerializedDictionary() {}

        public SerializedDictionary(IDictionary<K,V> dictionary)
        {
            foreach (var pair in dictionary) {
                this.dictionary.Add(pair.Key, pair.Value);
            }
        }
    }

    #region IDictionary Implementation
    public partial class SerializedDictionary<K,V>: IDictionary<K,V>
    {
        public V this[K key] {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public ICollection<K> Keys => dictionary.Keys;
        public ICollection<V> Values => dictionary.Values;
        public int Count => dictionary.Count;
        public bool IsReadOnly => false;

        public void Add(K key, V value) => dictionary.Add(key, value);
        public void Add(KeyValuePair<K, V> item) => dictionary.Add(item.Key, item.Value);

        public void Clear()
        {
            dictionary.Clear();
            keys.Clear();
            values.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item) => dictionary.ContainsKey(item.Key);
        public bool ContainsKey(K key) => dictionary.ContainsKey(key);
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            foreach (var pair in dictionary) {
                array[arrayIndex++] = pair;
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => dictionary.GetEnumerator();
        public bool Remove(K key) => dictionary.Remove(key);
        public bool Remove(KeyValuePair<K, V> item) => dictionary.Remove(item.Key);
        public bool TryGetValue(K key, out V value) => dictionary.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();
    }
    #endregion

    #region ISerializationCallbackReceiver Implementation
    public partial class SerializedDictionary<K,V>: ISerializationCallbackReceiver
    {
        public void OnAfterDeserialize()
        {
            dictionary.Clear();

            if (keys.Count != values.Count) throw new System.Exception("Serialized Array Mismatch");

            for (int index = 0; index < keys.Count; index++) {
                dictionary.Add(keys[index], values[index]);
            }
        }

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var pair in dictionary) {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
    }
    #endregion
}