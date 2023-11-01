﻿using AVG_TASK_APP.Views;
using Microsoft.EntityFrameworkCore.Query;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for btnBoard.xamls
    /// </summary>
    public partial class btnBoard : UserControl
    {
        public event EventHandler btnBoard_Click;

        public btnBoard(int id, string name)
        {
            InitializeComponent();

            idBtnBoard.Text = id.ToString();
            content.Text = name;
        }

        private void buttonBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (iconStarBtn.Visibility == Visibility.Hidden)
            {
                iconStarBtn.Visibility = Visibility.Visible;

            }
            if (iconStarBtn.Visibility == Visibility.Visible)
            {
                iconStarBtn.Visibility = Visibility.Hidden;

            }
        }

        private void buttonBoard_MouseEnter(object sender, MouseEventArgs e)
        {
            btnStar.Visibility = Visibility.Visible;

        }

        private void buttonBoard_MouseLeave(object sender, MouseEventArgs e)
        {
            btnStar.Visibility = Visibility.Collapsed;

        }

        private void btnStar_Click(object sender, RoutedEventArgs e)
        {
            iconStarBtn.Foreground = Brushes.Orange;
            changeColorStar();

        }

        private void changeColorStar()
        {
            if (iconStarBtn.Foreground == Brushes.Orange)
            {
                iconStarBtn.Foreground = Brushes.Black;
            }
            if (iconStarBtn.Foreground == Brushes.Black)
            {
                iconStarBtn.Foreground = Brushes.Orange;
            }
        }

        private void ButtonBoard_Loaded(object sender, RoutedEventArgs e)
        {
            btnStar.Visibility = Visibility.Collapsed;
        }

        private void buttonBoard_Click(object sender, EventArgs e)
        {


            int idTable = int.Parse(idBtnBoard.Text);

            ManageTaskLayout manage = new ManageTaskLayout(idTable);
            manage.Show();


            foreach (Window window in Application.Current.Windows)
            {
                if (window is PageLayout)
                {
                    window.Close();
                }
            }
        }
    }
}
