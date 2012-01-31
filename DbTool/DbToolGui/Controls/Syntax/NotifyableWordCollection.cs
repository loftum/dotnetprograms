using System;
using System.Collections;
using System.Collections.Generic;

namespace DbToolGui.Controls.Syntax
{
    public class NotifyableWordCollection : IList<string>
    {
        private readonly IList<string> _words;

        public event EventHandler ListChanged;
        public event EventHandler ListCleared;

        public NotifyableWordCollection()
        {
            _words = new List<string>();
        }

        public NotifyableWordCollection(IList<string> list)
        {
            _words = list;
        }

        public NotifyableWordCollection(IEnumerable<string> collection)
        {
            _words = new List<string>(collection);
        }

        protected virtual void OnListChanged(EventArgs e)
        {
            if (ListChanged != null)
                ListChanged(this, e);
        }

        protected virtual void OnListCleared(EventArgs e)
        {
            if (ListCleared != null)
                ListCleared(this, e);
        }

        public int IndexOf(string item)
        {
            return _words.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            _words.Insert(index, item);
            OnListChanged(EventArgs.Empty);
        }

        public void RemoveAt(int index)
        {
            string item = _words[index];
            _words.Remove(item);
            OnListChanged(EventArgs.Empty);
        }

        public string this[int index]
        {
            get { return _words[index]; }
            set
            {
                _words[index] = value;
                OnListChanged(EventArgs.Empty);
            }
        }

        public void Add(string item)
        {
            _words.Add(item);
            OnListChanged(EventArgs.Empty);
        }

        public void Clear()
        {
            _words.Clear();
            OnListCleared(new EventArgs());
        }

        public bool Contains(string item)
        {
            return _words.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            var index = arrayIndex;
            var arrayLength = array.Length;
            foreach (var word in _words)
            {
                if (index > arrayLength -1)
                {
                    return;
                }
                array[index] = word;
                index++;
            }
        }

        public int Count
        {
            get { return _words.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string item)
        {
            lock (this)
            {
                if (_words.Remove(item))
                {
                    OnListChanged(EventArgs.Empty);
                    return true;
                }
                return false;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _words.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_words).GetEnumerator();
        }
    }
}