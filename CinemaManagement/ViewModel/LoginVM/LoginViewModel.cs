﻿using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using CinemaManagement.View;
using CinemaManagement.View.LoginWindow;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace CinemaManagement.ViewModel.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        #region Icommand
        public ICommand LoginCM { get; set; }
        public ICommand PassChangedCM { get; set; }
        public ICommand SaveLoginWindowCM { get; set; }
        public Window LoginWindow { get; set; }
        #endregion

        #region Thuộc tính

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
        private bool IsLoading { get; set; }
        #endregion

        public LoginViewModel() 
        {
            LoginCM = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string user = Username;
                string pass = Password;

                IsLoading = true;
                await CheckUserPass(user, pass, p);
                IsLoading = false;
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
                    NavigationWindow w1 = new NavigationWindow();
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
