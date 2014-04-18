using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Mvvm;
using DevExpress.Xpf.Mvvm.Native;
using DevExpress.Xpf.Mvvm.UI;

public class SplashScreenService : ViewServiceBase, ISplashScreenService
{
    #region

    // Fields
    private bool _splashScreenIsShownOnLoading;
    private static volatile ISupportSplashScreen _splashScreenViewModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public DataTemplateSelector ViewTemplateSelector { get; set; }

    #endregion

    #region

    // Properties
    bool ISplashScreenService.IsSplashScreenActive
    {
        get { return DXSplashScreen.IsActive; }
    }

    public static readonly DependencyProperty ShowSplashScreenOnLoadingProperty;
    public bool ShowSplashScreenOnLoading
    {
        get { return (bool)base.GetValue(ShowSplashScreenOnLoadingProperty); }
        set { base.SetValue(ShowSplashScreenOnLoadingProperty, value); }
    }

    public static readonly DependencyProperty SplashScreenStartupLocationProperty;
    public WindowStartupLocation SplashScreenStartupLocation
    {
        get { return (WindowStartupLocation)base.GetValue(SplashScreenStartupLocationProperty); }
        set { base.SetValue(SplashScreenStartupLocationProperty, value); }
    }

    public static readonly DependencyProperty SplashScreenTypeProperty = DependencyProperty.Register(
        "SplashScreenType", typeof(Type), typeof(SplashScreenService), new PropertyMetadata(null));
    public Type SplashScreenType
    {
        get { return (Type)base.GetValue(SplashScreenTypeProperty); }
        set { base.SetValue(SplashScreenTypeProperty, value); }
    }

    public static readonly DependencyProperty SplashScreenWindowStyleProperty;
    public Style SplashScreenWindowStyle
    {
        get { return (Style)base.GetValue(SplashScreenWindowStyleProperty); }
        set { base.SetValue(SplashScreenWindowStyleProperty, value); }
    }

    #endregion

    #region Constructor

    static SplashScreenService()
    {
        SplashScreenWindowStyleProperty = DependencyProperty.Register("SplashScreenWindowStyle", typeof(Style),
            typeof(SplashScreenService),
            new PropertyMetadata(null,
                delegate(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {
                    ((SplashScreenService)d).OnSplashScreenWindowStyleChanged((Style)e.OldValue, (Style)e.NewValue);
                }));
        SplashScreenStartupLocationProperty = DependencyProperty.Register("SplashScreenStartupLocation",
            typeof(WindowStartupLocation), typeof(SplashScreenService),
            new PropertyMetadata(WindowStartupLocation.CenterScreen));
        ShowSplashScreenOnLoadingProperty = DependencyProperty.Register("ShowSplashScreenOnLoading", typeof(bool),
            typeof(SplashScreenService), new PropertyMetadata(false));
        _splashScreenViewModel = null;
    }

    #endregion

    #region

    // Methods
    private static object CreateSplashScreen(object parameter)
    {
        object[] objArray = (object[])parameter;
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

    private static Window CreateSplashScreenWindow(object parameter)
    {
        object[] objArray = (object[])parameter;
        Style style = (Style)objArray[0];
        WindowStartupLocation location = (WindowStartupLocation)objArray[1];
        if (style != null)
        {
            return new Window { Style = style, WindowStartupLocation = location };
        }
        Window window = DXSplashScreen.DefaultSplashScreenWindowCreator(null);
        window.WindowStartupLocation = location;
        WindowFadeAnimationBehavior.SetEnableAnimation(window, true);
        return window;
    }

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

    void ISplashScreenService.SetSplashScreenProgress(double progress, double maxProgress)
    {
        if ((this.SplashScreenType != null) && typeof(Window).IsAssignableFrom(this.SplashScreenType))
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

    void ISplashScreenService.SetSplashScreenState(object state)
    {
        if ((this.SplashScreenType != null) && typeof(Window).IsAssignableFrom(this.SplashScreenType))
        {
            throw new InvalidOperationException("This method is not supported when SplashScreenType is used.");
        }
        if (!DXSplashScreen.IsActive)
        {
            throw new InvalidOperationException("Show splash screen before calling this method.");
        }
        _splashScreenViewModel.State = state;
    }

    void ISplashScreenService.ShowSplashScreen(string documentType)
    {
        if (this.SplashScreenType != null)
        {
            if ((!string.IsNullOrEmpty(documentType) || (base.ViewTemplate != null)) || (base.ViewLocator != null))
            {
                throw new InvalidOperationException(
                    "Cannot use ViewLocator, ViewTemplate and DocumentType if SplashScreenType is set. If you set the SplashScreenType property, do not set the other properties.");
            }
            if (typeof(Window).IsAssignableFrom(this.SplashScreenType))
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
                new object[] { this.SplashScreenWindowStyle, this.SplashScreenStartupLocation },
                new object[] { documentType, base.ViewLocator, base.ViewTemplate, this.SplashScreenType });
        }
    }

    public void ShowSplashScreenByData(string documentType, object datacontext)
    {
        ((ISplashScreenService)this).ShowSplashScreen(documentType);
        ((SplashScreenData)_splashScreenViewModel).DataContext = datacontext;
    }

    private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
    {
        base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
        if (this._splashScreenIsShownOnLoading)
        {
            this._splashScreenIsShownOnLoading = false;
            ((ISplashScreenService)this).HideSplashScreen();
        }
    }

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

    protected override void OnDetaching()
    {
        base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
        base.OnDetaching();
    }

    private void OnSplashScreenWindowStyleChanged(Style oldValue, Style newValue)
    {
        if (newValue != null)
        {
            newValue.Seal();
        }
    }

    protected override void OnViewTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
    {
        base.OnViewTemplateChanged(oldValue, newValue);
        if (newValue != null)
        {
            newValue.Seal();
        }
    }

    protected override void OnViewTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
    {
        throw new InvalidOperationException("ViewTemplateSelector is not supported by DXSplashScreenService");
    }

    #endregion
}

public class SplashScreenData : SplashScreenViewModel
{
    public SplashScreenData()
    {
        IsIndeterminate = true;
    }

    public object DataContext { get; set; }
}