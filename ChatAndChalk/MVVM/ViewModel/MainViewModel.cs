using ChatAndChalk_Client.MVVM.Core;
using ChatAndChalk_Client.MVVM.Model;
using ChatAndChalk_Client.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatAndChalk_Client.MVVM.ViewModel
{
    class MainViewModel
    {

        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }

        public RelayCommand ConnectToSeverCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }


        public string Username { get; set; }
        public string Message { get; set; }

        private Sever _sever;
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();

            _sever = new Sever();
            _sever.connectedEvent += UserConnected;
            _sever.msgReceivedEvent += MessageReceived;
            _sever.userDisconnectEvent += RemoveUser; ;

            ConnectToSeverCommand = new RelayCommand(o => _sever.ConnectToSever(Username), o => !string.IsNullOrEmpty(Username));
            
            SendMessageCommand = new RelayCommand(o => _sever.SendMessageToSever(Message), o => !string.IsNullOrEmpty(Message));
        }

        private void RemoveUser()
        {
            var uid = _sever.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));

        }

        private void MessageReceived()
        {
            var msg = _sever.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }

        private  void UserConnected()
        {
            var user = new UserModel
            {
                Username = _sever.PacketReader.ReadMessage(),
                UID = _sever.PacketReader.ReadMessage(),
            };

            if (!Users.Any(x => x.UID == user.UID)) 
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}
