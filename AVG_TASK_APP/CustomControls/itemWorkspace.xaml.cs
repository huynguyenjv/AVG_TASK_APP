﻿using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.ViewModels;
using AVG_TASK_APP.Views;
using FontAwesome.WPF;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace AVG_TASK_APP.CustomControls
{
    /// <summary>
    /// Interaction logic for itemWorkspace.xaml
    /// </summary>
    public partial class itemWorkspace : UserControl
    {
        private int idWorkspaceCurrent;
        public event EventHandler itemWorkspace_Click;

        public itemWorkspace(int idWorkspaceDB)
        {
            ItemWorkspaceViewModel viewModel = new ItemWorkspaceViewModel();
            DataContext = viewModel;

            InitializeComponent();
            userControl.Height = 50;
            StackPanel stackPanel = itemMenuWorkspace;
            stackPanel.Visibility = Visibility.Collapsed;

            idWorkspace.Text = idWorkspaceDB.ToString();
            idWorkspaceCurrent = idWorkspaceDB;
            nameWorkspace.Text = viewModel.getName();

            iconMenu.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.CaretUp;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = itemMenuWorkspace;
            if (stackPanel.Visibility == Visibility.Collapsed)
            {
                stackPanel.Visibility = Visibility.Visible;
                iconMenu.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.CaretDown;
                userControl.Height = 222;

            }
            else
            {
                stackPanel.Visibility = Visibility.Collapsed;
                iconMenu.Icon = (FontAwesome.Sharp.IconChar)FontAwesomeIcon.CaretUp;
                userControl.Height = 50;
            }

        }

        private void btnItemBoard_Click(object sender, RoutedEventArgs e)
        {
            PageLayout pageLayout = (PageLayout)Window.GetWindow(this);
            pageLayout.areaUserControl.Children.Clear();
            BoardUserControl boardView = new BoardUserControl(int.Parse(idWorkspace.Text));
            pageLayout.areaUserControl.Children.Add(boardView);
        }

        private void btnItemMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnItemSetting_Click(object sender, EventArgs e)
        {
            SettingView settingView = new SettingView(idWorkspaceCurrent);
            settingView.ShowDialog();

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
