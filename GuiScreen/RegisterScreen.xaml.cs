﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TexasHoldem.GuiScreen
{
    /// <summary>
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : Window
    {
        private ClientLogic cl;
        private Window parentScreen;
        private int Id;
        private string name;
        private string username;
        private string email;
        private string firstPassword;
        private string secPassword;
        private int money;
         
        public RegisterScreen(Window parent)
        {
            InitializeComponent();
            parentScreen = parent;
            cl = new ClientLogic();
        }

        private void IDtextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string temp = IDtextBox.Text;
            bool isValid = int.TryParse(temp, out Id);
            if (!isValid)
            {
                MessageBox.Show("Invalid input");
            }
            
        }

        private void NametextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = NametextBox.Text;
        }

        private void UserNametextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            username = UserNametextBox.Text;
        }

        private void EmailtextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = EmailtextBox.Text;
        }

        private void MoneytextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string temp = MoneytextBox.Text;
            bool isValid = int.TryParse(temp, out money);
            if (!isValid)
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void PasswordFirstTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            firstPassword = PasswordFirstTextBox.Text;
        }

        private void PasswordSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            secPassword = PasswordSecTextBox.Text;
        }

        private void BackToMainButton_Click(object sender, RoutedEventArgs e)
        {
            parentScreen.Show();
            this.Hide();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!firstPassword.Equals(secPassword))
            {
                MessageBox.Show("password dont match");
                return;
            }
            
            bool registerOk = cl.register(name, username, firstPassword, money, email);
            if (registerOk)
            {
                //todo open main window
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }
    }
}