﻿using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace KSP_WPF
{
    /// <summary>
    /// ImageViewerWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ImageViewerWindow : MetroWindow
    {
        public Image currentImage;
        public ImageViewerWindow()
        {
            InitializeComponent();
            IMG_Main.PreviewMouseRightButtonDown += GlobalHelper.CopyImageHandler;
        }

        private void MetroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                GlobalHelper.CopyImageHandler(IMG_Main, null);
                e.Handled = true;
            }
            else if (e.Key == Key.W && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Close();
                e.Handled = true;
            }
            else if (e.Key == Key.F5 || (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control))
            {
                ZB_Main.FitToBounds();
                e.Handled = true;
            }
            else if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                BT_Save_Click(BT_Save, null);
                e.Handled = true;
            }
            else if (e.Key == Key.F && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ZB_Main.FillToBounds();
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                if (currentImage.Tag is Image[] nextPrev)
                {
                    if (nextPrev[0] != null)
                    {
                        currentImage = nextPrev[0];
                        IMG_Main.Source = currentImage.Source;
                        ZB_Main.FitToBounds();
                    }
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                if (currentImage.Tag is Image[] nextPrev)
                {
                    if (nextPrev[1] != null)
                    {
                        currentImage = nextPrev[1];
                        IMG_Main.Source = currentImage.Source;
                        ZB_Main.FitToBounds();
                    }
                }
                e.Handled = true;
            }
        }

        private void BT_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Image img = IMG_Main;
                GlobalHelper.SaveImageToFile(img);
            }
            catch (Exception)
            {
                GlobalHelper.SaveGIFImage(IMG_Main);
            }
        }

        private void BT_Reset_Click(object sender, RoutedEventArgs e)
        {
            ZB_Main.FitToBounds();
        }

        private void MetroWindow_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!(e.Source is System.Windows.Controls.TextBox))
                e.Handled = true;
        }

        private void BT_Clipboard_Click(object sender, RoutedEventArgs e)
        {
            GlobalHelper.CopyImageHandler(IMG_Main, null);
        }
    }
}
