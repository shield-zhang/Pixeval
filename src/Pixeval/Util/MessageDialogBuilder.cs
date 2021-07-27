﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Pixeval.Pages;

namespace Pixeval.Util
{
    public class MessageDialogBuilder
    {
        private readonly ContentDialog _contentDialog = new();

        public static MessageDialogBuilder Create() => new();

        public static ContentDialog CreateOkCancel(UserControl owner, string title, string content)
        {
            return Create().WithTitle(title)
                .WithContent(content)
                .WithPrimaryButtonText(MessageContentDialogResources.OkButtonContent)
                .WithCloseButtonText(MessageContentDialogResources.CancelButtonContent)
                .WithDefaultButton(ContentDialogButton.Primary)
                .Build(owner);
        }

        public static ContentDialog CreateOkCancel(Window owner, string title, string content)
        {
            return Create().WithTitle(title)
                .WithContent(content)
                .WithPrimaryButtonText(MessageContentDialogResources.OkButtonContent)
                .WithCloseButtonText(MessageContentDialogResources.CancelButtonContent)
                .WithDefaultButton(ContentDialogButton.Primary)
                .Build(owner);
        }

        public static ContentDialog CreateAcknowledgement(UserControl owner, string title, string content)
        {
            return Create().WithTitle(title)
                .WithContent(content)
                .WithPrimaryButtonText(MessageContentDialogResources.OkButtonContent)
                .WithDefaultButton(ContentDialogButton.Primary)
                .Build(owner);
        }

        public static ContentDialog CreateAcknowledgement(Window owner, string title, string content)
        {
            return Create().WithTitle(title)
                .WithContent(content)
                .WithPrimaryButtonText(MessageContentDialogResources.OkButtonContent)
                .WithDefaultButton(ContentDialogButton.Primary)
                .Build(owner);
        }

        public MessageDialogBuilder WithTitle(string title)
        {
            _contentDialog.Title = title;
            return this;
        }

        public MessageDialogBuilder WithPrimaryButtonText(string text)
        {
            _contentDialog.PrimaryButtonText = text;
            return this;
        }

        public MessageDialogBuilder WithSecondaryButtonText(string text)
        {
            _contentDialog.SecondaryButtonText = text;
            return this;
        }

        public MessageDialogBuilder WithCloseButtonText(string text)
        {
            _contentDialog.CloseButtonText = text;
            return this;
        }

        public MessageDialogBuilder WithDefaultButton(ContentDialogButton button)
        {
            _contentDialog.DefaultButton = button;
            return this;
        }

        public MessageDialogBuilder WithContent(string message)
        {
            _contentDialog.Content = new MessageDialogContent(message);
            return this;
        }

        public ContentDialog Build(UserControl owner) // the owner argument is a workaround for issue #4870
        {
            _contentDialog.XamlRoot = owner.Content.XamlRoot;
            return _contentDialog;
        }

        public ContentDialog Build(Window owner) // the owner argument is a workaround for issue #4870
        {
            _contentDialog.XamlRoot = owner.Content.XamlRoot;
            return _contentDialog;
        }
    }
}