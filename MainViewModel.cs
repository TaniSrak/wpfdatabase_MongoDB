using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace wpfdatabase
{
    internal class MainViewModel : PropertyChangeBase //связующее между данными и интерфейсом
    {
        public ObservableCollection<User> Users { get; set; }
        

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        private RelayCommand _addUser;
        public RelayCommand AddUser
        {
            get
            {
                return _addUser ??
                    (_addUser = new RelayCommand(obj =>
                    {
                        User user = new User
                        {
                            Name = Name,
                            Email = Email,
                            Age = Age
                        };
                        Users.Add(user);
                        NetController.WriteUser(user).GetAwaiter().GetResult();
                  
                    },
                    obj =>
                    {
                        return !string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_email);
                    }));
            }
        }

        public MainViewModel() 
        { 
            Users = new ObservableCollection<User>();
            string usersJson = NetController.GetAllUsers().GetAwaiter().GetResult();

            BsonValue[] users = BsonSerializer.Deserialize<BsonArray>(usersJson).ToArray();

            foreach (BsonValue user in users)
            {
                Users.Add(BsonSerializer.Deserialize<User>(user.AsBsonDocument));
            }
        }
    }
}
