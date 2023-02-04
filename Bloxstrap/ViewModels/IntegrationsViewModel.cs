﻿using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Mvvm.Contracts;

using Bloxstrap.Helpers;
using Bloxstrap.Views.Pages;

namespace Bloxstrap.ViewModels
{
    public class IntegrationsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        private readonly Page _page;

        public ICommand OpenReShadeFolderCommand => new RelayCommand(OpenReShadeFolder);
        public ICommand ShowReShadeHelpCommand => new RelayCommand(ShowReShadeHelp);

        public bool CanOpenReShadeFolder => App.Settings.Prop.UseReShade;

        public IntegrationsViewModel(Page page)
        {
            _page = page;
        }

        private void OpenReShadeFolder()
        {
            Process.Start("explorer.exe", Path.Combine(Directories.Integrations, "ReShade"));
        }

        private void ShowReShadeHelp()
        {
            ((INavigationWindow)Window.GetWindow(_page)!).Navigate(typeof(ReShadeHelpPage));
        }

        public bool DiscordActivityEnabled
        {
            get => App.Settings.Prop.UseDiscordRichPresence;
            set
            {
                App.Settings.Prop.UseDiscordRichPresence = value;

                if (!value)
                {
                    DiscordActivityJoinEnabled = value;
                    OnPropertyChanged(nameof(DiscordActivityJoinEnabled));
                }
            }
        }

        public bool DiscordActivityJoinEnabled
        {
            get => !App.Settings.Prop.HideRPCButtons;
            set => App.Settings.Prop.HideRPCButtons = !value;
        }

        public bool ReShadeEnabled
        {
            get => App.Settings.Prop.UseReShade;
            set
            {
                App.Settings.Prop.UseReShade = value;
                ReShadePresetsEnabled = value;
                OnPropertyChanged(nameof(ReShadePresetsEnabled));
            }
        }

        public bool ReShadePresetsEnabled
        {
            get => App.Settings.Prop.UseReShadeExtraviPresets;
            set => App.Settings.Prop.UseReShadeExtraviPresets = value;
        }

        public bool RbxFpsUnlockerEnabled
        {
            get => App.Settings.Prop.RFUEnabled;
            set
            {
                App.Settings.Prop.RFUEnabled = value;
                RbxFpsUnlockerAutocloseEnabled = value;
                OnPropertyChanged(nameof(RbxFpsUnlockerAutocloseEnabled));
            }
        }

        public bool RbxFpsUnlockerAutocloseEnabled
        {
            get => App.Settings.Prop.RFUAutoclose;
            set => App.Settings.Prop.RFUAutoclose = value;
        }
    }
}