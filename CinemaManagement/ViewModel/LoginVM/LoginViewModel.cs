using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.View.LoginWindow;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace CinemaManagement.ViewModel.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCM { get; set; }
        public ICommand PassChangedCM { get; set; }
        public ICommand SaveLoginWindowCM { get; set; }
        public Window LoginWindow { get; set; }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public LoginViewModel() 
        {
            LoginCM = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string user = Username;
                string pass = Password;

                await CheckUserPass(user, pass, p);
            });

            PassChangedCM= new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });

            SaveLoginWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoginWindow = p;
            });
        }

        private async Task CheckUserPass(string user, string pass, Label lbl)
        {
            if(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }

            (bool loginStatus, string messages, NhanVienDTO nhanvien) = await Task<(bool, string, NhanVienDTO)>.Run(() => NhanVienDAL.Instance.Login(user, pass));

            if(loginStatus)
            {
                LoginWindow.Hide();
                if(nhanvien.Staff_Level == 1)
                {
                    MainNavigation w1 = new MainNavigation();
                    w1.Show();
                    LoginWindow.Close();
                    return;
                }
            }
            else
            {
                MessageBox.Show(messages);
                return;
            }
        }
    }
}
