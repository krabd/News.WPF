﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using News.CoreModule.Enums;
using News.CoreModule.Interfaces;
using News.CoreModule.Views;
using News.Utils;
using News.Utils.Extensions;

namespace News.CoreModule.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly ConcurrentDictionary<Type, (Window window, IServiceScope scope)> _workspace =
            new ConcurrentDictionary<Type, (Window window, IServiceScope scope)>();

        private readonly ConcurrentDictionary<Window, IServiceScope> _independenceWorkspace =
            new ConcurrentDictionary<Window, IServiceScope>();

        private readonly IServiceProvider _serviceProvider;

        public WorkspaceService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OpenWorkspace<T>(string title) where T : IWorkspace
        {
            try
            {
                Tools.DispatchedInvoke(() =>
                {
                    if (_workspace.ContainsKey(typeof(T)))
                    {
                        _workspace.TryGetValue(typeof(T), out var w);
                        Activate(w.window);
                    }
                    else
                    {
                        var (window, scope) = (CreateWindow(typeof(T)), _serviceProvider.CreateScope());
                        if (_workspace.TryAdd(typeof(T), (window, scope)))
                        {
                            CreateAndInitializeWorkspace(window, () => scope.ServiceProvider.Resolve<T>(), title);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void OpenIndependenceWorkspace<T>(string title) where T : IWorkspace
        {
            try
            {
                Tools.DispatchedInvoke(() =>
                {
                    var (window, scope) = (CreateWindow(typeof(T)), _serviceProvider.CreateScope());
                    if (_independenceWorkspace.TryAdd(window, scope))
                    {
                        CreateAndInitializeWorkspace(window, () => scope.ServiceProvider.Resolve<T>(), title);
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private Window CreateWindow(Type type)
        {
            var window = new BaseWindow
            {
                WindowStyle = WindowStyle.SingleBorderWindow,
                WindowState = WindowState.Maximized
            };

            window.Closed += OnWindowClosed;
            return window;
        }

        private void Activate(Window window)
        {
            if (window.IsActive) return;

            if (window.WindowState == WindowState.Minimized)
                window.WindowState = WindowState.Normal;

            window.Activate();
        }

        private void CreateAndInitializeWorkspace<T>(Window window, Func<T> createWorkspaceFunc, string title) where T : IWorkspace
        {
            var workspace = createWorkspaceFunc();
            window.DataContext = workspace;
            window.Title = title;
            window.Show();
            workspace.InitializeAsync();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            var window = (Window) sender;
            window.Closed -= OnWindowClosed;

            if (_workspace.TryRemove(window.DataContext.GetType(), out var w))
                w.scope?.Dispose();
            else if (_independenceWorkspace.TryRemove(window, out var scope))
                scope?.Dispose();
        }
    }
}