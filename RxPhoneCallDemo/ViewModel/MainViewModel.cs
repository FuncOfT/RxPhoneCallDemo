using GalaSoft.MvvmLight;
using GenerateSomeData;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace RxPhoneCallDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private readonly ConversationFileWatcher _watcher;
        private IDisposable _conversationSubscription;
        private string _brokerId;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            _brokerId = "Broker_0";
            _watcher = new ConversationFileWatcher();
            Conversations = new ObservableCollection<Conversation>();

            SubscribeToNewBroker();
        }

        private ObservableCollection<Conversation> LoadInitialConversations()
        {
            return new ObservableCollection<Conversation>();
        }

        private void OnNewConversation(Conversation conversation)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                Conversations.Add(conversation);
            }));
        }

        public ObservableCollection<Conversation> Conversations { get; private set; }

        public string BrokerId
        {
            get
            {
                return _brokerId;
            }
            set
            {
                _brokerId = value;

                SubscribeToNewBroker();

                RaisePropertyChanged();
            }
        }

        private void SubscribeToNewBroker()
        {
            if (_conversationSubscription != null)
            {
                _conversationSubscription.Dispose();
            }

            Conversations.Clear();

            _conversationSubscription = _watcher.GetConversationObservable()
                .Where(x => x.BrokerId.Equals(_brokerId, StringComparison.InvariantCultureIgnoreCase))
                .Subscribe(OnNewConversation);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _conversationSubscription.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MainViewModel() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}