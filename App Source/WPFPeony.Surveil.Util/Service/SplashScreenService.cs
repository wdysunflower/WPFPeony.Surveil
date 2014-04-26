// ***********************************************************************
// <copyright file="SplashScreenService.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.Util
// Author           : wdysunflower
// Created          : 04-18-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-18-2014
// ***********************************************************************

using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Mvvm;
using DevExpress.Xpf.Mvvm.Native;
using DevExpress.Xpf.Mvvm.UI;

/// <summary>
/// Class SplashScreenService.
/// </summary>
public class SplashScreenService : ViewServiceBase, ISplashScreenService
{
    #region Member

    // Fields
    /// <summary>
    /// The _splash screen is shown on loading
    /// </summary>
    private bool _splashScreenIsShownOnLoading;

    /// <summary>
    /// The _splash screen view model
    /// </summary>
    private static volatile ISupportSplashScreen _splashScreenViewModel;

    #endregion

    #region Prperty

    // Properties
    /// <summary>
    /// [To be supplied]
    /// </summary>
    /// <value>[To be supplied]</value>
    bool ISplashScreenService.IsSplashScreenActive
    {
        get { return DXSplashScreen.IsActive; }
    }

    /// <summary>
    /// The show splash screen on loading property
    /// </summary>
    public static readonly DependencyProperty ShowSplashScreenOnLoadingProperty;

    /// <summary>
    /// Gets or sets a value indicating whether [show splash screen on loading].
    /// </summary>
    /// <value><c>true</c> if [show splash screen on loading]; otherwise, <c>false</c>.</value>
    public bool ShowSplashScreenOnLoading
    {
        get { return (bool) GetValue(ShowSplashScreenOnLoadingProperty); }
        set { base.SetValue(ShowSplashScreenOnLoadingProperty, value); }
    }

    /// <summary>
    /// The splash screen startup location property
    /// </summary>
    public static readonly DependencyProperty SplashScreenStartupLocationProperty;

    /// <summary>
    /// Gets or sets the splash screen startup location.
    /// </summary>
    /// <value>The splash screen startup location.</value>
    public WindowStartupLocation SplashScreenStartupLocation
    {
        get { return (WindowStartupLocation) base.GetValue(SplashScreenStartupLocationProperty); }
        set { base.SetValue(SplashScreenStartupLocationProperty, value); }
    }

    /// <summary>
    /// The splash screen type property
    /// </summary>
    public static readonly DependencyProperty SplashScreenTypeProperty = DependencyProperty.Register(
        "SplashScreenType", typeof (Type), typeof (SplashScreenService), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the type of the splash screen.
    /// </summary>
    /// <value>The type of the splash screen.</value>
    public Type SplashScreenType
    {
        get { return (Type) base.GetValue(SplashScreenTypeProperty); }
        set { base.SetValue(SplashScreenTypeProperty, value); }
    }

    /// <summary>
    /// The splash screen window style property
    /// </summary>
    public static readonly DependencyProperty SplashScreenWindowStyleProperty;

    /// <summary>
    /// Gets or sets the splash screen window style.
    /// </summary>
    /// <value>The splash screen window style.</value>
    public Style SplashScreenWindowStyle
    {
        get { return (Style) base.GetValue(SplashScreenWindowStyleProperty); }
        set { base.SetValue(SplashScreenWindowStyleProperty, value); }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes static members of the <see cref="SplashScreenService"/> class.
    /// </summary>
    static SplashScreenService()
    {
        SplashScreenWindowStyleProperty = DependencyProperty.Register("SplashScreenWindowStyle", typeof (Style),
            typeof (SplashScreenService),
            new PropertyMetadata(null,
                delegate(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {
                    ((SplashScreenService) d).OnSplashScreenWindowStyleChanged((Style) e.OldValue, (Style) e.NewValue);
                }));
        SplashScreenStartupLocationProperty = DependencyProperty.Register("SplashScreenStartupLocation",
            typeof (WindowStartupLocation), typeof (SplashScreenService),
            new PropertyMetadata(WindowStartupLocation.CenterScreen));
        ShowSplashScreenOnLoadingProperty = DependencyProperty.Register("ShowSplashScreenOnLoading", typeof (bool),
            typeof (SplashScreenService), new PropertyMetadata(false));
        _splashScreenViewModel = null;
    }

    #endregion

    #region Method

    // Methods
    /// <summary>
    /// Creates the splash screen.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>System.Object.</returns>
    private static object CreateSplashScreen(object parameter)
    {
        object[] objArray = (object[]) parameter;
        string documentType = objArray[0] as string;
        IViewLocator viewLocator = objArray[1] as IViewLocator;
        DataTemplate viewTemplate = objArray[2] as DataTemplate;
        Type type = objArray[3] as Type;
        _splashScreenViewModel = new SplashScreenData();
        object input = null;
        if (type != null)
        {
            input = Activator.CreateInstance(type);
            input.With<object, FrameworkElement>(x => (x as FrameworkElement))
                .Do<FrameworkElement>(delegate(FrameworkElement x) { x.DataContext = _splashScreenViewModel; });
            return input;
        }
        return ViewHelper.CreateAndInitializeView(viewLocator, documentType, _splashScreenViewModel, null, null,
            viewTemplate, null);
    }

    /// <summary>
    /// Creates the splash screen window.
    /// </summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>Window.</returns>
    private static Window CreateSplashScreenWindow(object parameter)
    {
        object[] objArray = (object[]) parameter;
        Style style = (Style) objArray[0];
        WindowStartupLocation location = (WindowStartupLocation) objArray[1];
        if (style != null)
        {
            return new Window {Style = style, WindowStartupLocation = location};
        }
        Window window = DXSplashScreen.DefaultSplashScreenWindowCreator(null);
        window.WindowStartupLocation = location;
        WindowFadeAnimationBehavior.SetEnableAnimation(window, true);
        return window;
    }

    /// <summary>
    /// Hides the splash screen.
    /// </summary>
    void ISplashScreenService.HideSplashScreen()
    {
        if (DXSplashScreen.IsActive)
        {
            DXSplashScreen.Close();
            _splashScreenViewModel = null;

            if (base.AssociatedObject == null)
                Application.Current.MainWindow.Activate();
            else
            {
                Window window = Window.GetWindow(base.AssociatedObject);
                if (window != null)
                {
                    window.Activate();
                }
            }
        }
    }

    /// <summary>
    /// [To be supplied]
    /// </summary>
    /// <param name="progress">[To be supplied]</param>
    /// <param name="maxProgress">[To be supplied]</param>
    /// <exception cref="System.InvalidOperationException">Show splash screen before calling this method.</exception>
    void ISplashScreenService.SetSplashScreenProgress(double progress, double maxProgress)
    {
        if ((this.SplashScreenType != null) && typeof (Window).IsAssignableFrom(this.SplashScreenType))
        {
            DXSplashScreen.Progress(progress);
        }
        else
        {
            if (!DXSplashScreen.IsActive)
            {
                throw new InvalidOperationException("Show splash screen before calling this method.");
            }
            _splashScreenViewModel.IsIndeterminate = false;
            _splashScreenViewModel.MaxProgress = maxProgress;
            _splashScreenViewModel.Progress = progress;
        }
    }

    /// <summary>
    /// [To be supplied]
    /// </summary>
    /// <param name="state">[To be supplied]</param>
    /// <exception cref="System.InvalidOperationException">
    /// This method is not supported when SplashScreenType is used.
    /// or
    /// Show splash screen before calling this method.
    /// </exception>
    void ISplashScreenService.SetSplashScreenState(object state)
    {
        if ((this.SplashScreenType != null) && typeof (Window).IsAssignableFrom(this.SplashScreenType))
        {
            throw new InvalidOperationException("This method is not supported when SplashScreenType is used.");
        }
        if (!DXSplashScreen.IsActive)
        {
            throw new InvalidOperationException("Show splash screen before calling this method.");
        }
        _splashScreenViewModel.State = state;
    }

    /// <summary>
    /// [To be supplied]
    /// </summary>
    /// <param name="documentType">[To be supplied]</param>
    /// <exception cref="System.InvalidOperationException">Cannot use ViewLocator, ViewTemplate and DocumentType if SplashScreenType is set. If you set the SplashScreenType property, do not set the other properties.</exception>
    void ISplashScreenService.ShowSplashScreen(string documentType)
    {
        if (this.SplashScreenType != null)
        {
            if ((!string.IsNullOrEmpty(documentType) || (base.ViewTemplate != null)) || (base.ViewLocator != null))
            {
                throw new InvalidOperationException(
                    "Cannot use ViewLocator, ViewTemplate and DocumentType if SplashScreenType is set. If you set the SplashScreenType property, do not set the other properties.");
            }
            if (typeof (Window).IsAssignableFrom(this.SplashScreenType))
            {
                if (!DXSplashScreen.IsActive)
                {
                    DXSplashScreen.Show(this.SplashScreenType);
                }
                return;
            }
        }
        if (!DXSplashScreen.IsActive)
        {
            DXSplashScreen.Show(new Func<object, Window>(CreateSplashScreenWindow),
                SplashScreenService.CreateSplashScreen,
                new object[] {this.SplashScreenWindowStyle, this.SplashScreenStartupLocation},
                new object[] {documentType, base.ViewLocator, base.ViewTemplate, this.SplashScreenType});
        }
    }

    /// <summary>
    /// Shows the splash screen by data.
    /// </summary>
    /// <param name="documentType">Type of the document.</param>
    /// <param name="dataContext">The data context.</param>
    public void ShowSplashScreenByData(string documentType, object dataContext)
    {
        ((ISplashScreenService) this).ShowSplashScreen(documentType);
        ((SplashScreenData) _splashScreenViewModel).DataContext = dataContext;
    }

    /// <summary>
    /// Handles the <see cref="E:AssociatedObjectLoaded" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
    {
        base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
        if (this._splashScreenIsShownOnLoading)
        {
            this._splashScreenIsShownOnLoading = false;
            ((ISplashScreenService) this).HideSplashScreen();
        }
    }

    /// <summary>
    /// Called when [attached].
    /// </summary>
    protected override void OnAttached()
    {
        base.OnAttached();
        base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnAssociatedObjectLoaded);
        if (this.ShowSplashScreenOnLoading && !DXSplashScreen.IsActive)
        {
            this._splashScreenIsShownOnLoading = true;
            this.ShowSplashScreen();
        }
    }

    /// <summary>
    /// Called when [detaching].
    /// </summary>
    protected override void OnDetaching()
    {
        base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
        base.OnDetaching();
    }

    /// <summary>
    /// Called when [splash screen window style changed].
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private void OnSplashScreenWindowStyleChanged(Style oldValue, Style newValue)
    {
        if (newValue != null)
        {
            newValue.Seal();
        }
    }

    /// <summary>
    /// Called when [view template changed].
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected override void OnViewTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
    {
        base.OnViewTemplateChanged(oldValue, newValue);
        if (newValue != null)
        {
            newValue.Seal();
        }
    }

    /// <summary>
    /// Called when [view template selector changed].
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <exception cref="System.InvalidOperationException">ViewTemplateSelector is not supported by DXSplashScreenService</exception>
    protected override void OnViewTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
    {
        throw new InvalidOperationException("ViewTemplateSelector is not supported by DXSplashScreenService");
    }

    #endregion
}

/// <summary>
/// Class SplashScreenData.
/// </summary>
public class SplashScreenData : SplashScreenViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SplashScreenData"/> class.
    /// </summary>
    public SplashScreenData()
    {
        IsIndeterminate = true;
    }

    /// <summary>
    /// Gets or sets the data context.
    /// </summary>
    /// <value>The data context.</value>
    public object DataContext { get; set; }
}