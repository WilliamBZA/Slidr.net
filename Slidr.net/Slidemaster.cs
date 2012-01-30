using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace Slidr.net
{
    public class Slidemaster : Hub
    {
        private static int? _currentSlideNumber;
        private static int _maxSlideNumber = 4;
        private static Dictionary<string, string> _usersOnline;

        public int JoinSlideshow(string username)
        {
            string clientId = Context.ClientId;
            AddUser(clientId, username);

            return CurrentSlideNumber;
        }

        public void LeaveSlideshow()
        {
            string clientId = Context.ClientId;
            RemoveUser(clientId);
        }

        public void StartSlideshow(string username, string password)
        {
            if (password == "Go!")
            {
                string clientId = Context.ClientId;
                AddUser(clientId, username);

                SetSlide(1);
            }
        }

        public void NextSlide()
        {
            CurrentSlideNumber = CurrentSlideNumber + 1;

            if (CurrentSlideNumber > _maxSlideNumber)
            {
                CurrentSlideNumber = 1;
            }

            SetSlide(CurrentSlideNumber);
        }

        public void PreviousSlide()
        {
            CurrentSlideNumber = CurrentSlideNumber - 1;

            if (CurrentSlideNumber <= 0)
            {
                CurrentSlideNumber = _maxSlideNumber;
            }

            SetSlide(CurrentSlideNumber);
        }

        protected int CurrentSlideNumber
        {
            get
            {
                if (_currentSlideNumber == null)
                {
                    _currentSlideNumber = -1;
                }

                return _currentSlideNumber.Value;
            }

            set
            {
                _currentSlideNumber = value;
            }
        }

        protected Dictionary<string, string> UsersOnline
        {
            get
            {
                if (_usersOnline == null)
                {
                    _usersOnline = new Dictionary<string, string>();
                }

                return _usersOnline;
            }
        }

        private void SetSlide(int slideNumber)
        {
            Clients.SlideChanged(slideNumber);
        }

        private void RemoveUser(string clientId)
        {
            if (UsersOnline.ContainsKey(clientId))
            {
                UsersOnline.Remove(clientId);
            }
        }

        private void AddUser(string clientId, string username)
        {
            if (!UsersOnline.ContainsKey(clientId))
            {
                UsersOnline.Add(clientId, username);
            }
        }
    }
}