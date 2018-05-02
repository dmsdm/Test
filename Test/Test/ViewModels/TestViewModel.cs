using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Test.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private String[] _directions = new String[] {};
        private String _translateDirection = "";
        private String _text = "";
        private String _result;
        private Boolean _translateEnabled;
        private Boolean _saveEnabled;
        private ITranslator translator;
        private IHistory history;
        

        public void SetTranslator(ITranslator translator)
        {
            this.translator = translator;
            translator.GetLangs(this);
        }

        public void SetHistory(IHistory history)
        {
            this.history = history;
        }

        public String[] TranslateDirections
        {
            get => _directions;
            set
            {
                _directions = value;
                OnPropertyChanged();
            }
        }

        public String TranslateDirection
        {
            get => _translateDirection;
            set
            {
                _translateDirection = value;
                toggleTranslateEnabled();
                OnPropertyChanged();
            }
        }

        public String Text
        {
            get => _text;
            set
            {
                _text = value;
                toggleTranslateEnabled();
                OnPropertyChanged();
            }
        }

        private void toggleTranslateEnabled()
        {
            TranslateEnabled = _text.Length > 0 && _translateDirection.Length > 0;
        }

        public String Result
        {
            get => _result;
            set
            {
                _result = value;
                SaveEnabled = true;
                OnPropertyChanged();
            }
        }

        public Boolean TranslateEnabled
        {
            get => _translateEnabled;
            set
            {
                _translateEnabled = value;
                OnPropertyChanged();
            }
        }

        public Boolean SaveEnabled
        {
            get => _saveEnabled;
            set
            {
                _saveEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand Translate
        {
            get
            {
                return new Command( () =>
                {
                    translator.Translate(TranslateDirection, Text, this);
                });
            }
        }

        public ICommand Save
        {
            get
            {
                return new Command(() =>
                {
                    history.Save(TranslateDirection, Text, Result);
                    SaveEnabled = false;
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface ITranslator
    {
        void GetLangs(TestViewModel viewModel);
        void Translate(String direction, String text, TestViewModel viewModel);
    }

    public interface IHistory
    {
        void Save(String direction, String text, String result);
    }
}