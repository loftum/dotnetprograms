using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Encoder.Core;

namespace Encoder.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _decoded;
        private string _encoded;

        private readonly ITextEncoder _encoder;
        private StringEncoding _currentEncoding;
        private bool _urlEncode;
        private string _infoText;

        public IEnumerable<StringEncoding> Encodings { get; private set; }

        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                OnPropertyChanged();
            }
        }

        public bool UrlEncode
        {
            get { return _urlEncode; }
            set
            {
                _urlEncode = value;
                OnPropertyChanged();
            }
        }

        public StringEncoding CurrentEncoding
        {
            get { return _currentEncoding; }
            set
            {
                _currentEncoding = value;
                OnPropertyChanged();
            }
        }

        public string Decoded
        {
            get { return _decoded; }
            set
            {
                _decoded = value;
                OnPropertyChanged();
                Encode();
            }
        }

        public string Encoded
        {
            get { return _encoded; }
            set
            {
                _encoded = value;
                OnPropertyChanged();
                Decode();
            }
        }

        public MainWindowViewModel()
        {
            _encoder = new TextEncoder();
            Encodings = Enum.GetValues(typeof(StringEncoding)).Cast<StringEncoding>();
            CurrentEncoding = StringEncoding.Base64;
        }

        private void Encode()
        {
            Do(() =>
            {
                _encoded = _encoder.Encode(_decoded, CurrentEncoding, UrlEncode);
                OnPropertyChanged("Encoded");    
            });
        }

        private void Decode()
        {
            Do(() =>
            {
                _decoded = _encoder.Decode(_encoded, CurrentEncoding, UrlEncode);
                OnPropertyChanged("Decoded");    
            });
        }

        private void Do(Action action)
        {
            InfoText = "";
            try
            {
                action();
            }
            catch (Exception ex)
            {
                InfoText = ex.Message;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}