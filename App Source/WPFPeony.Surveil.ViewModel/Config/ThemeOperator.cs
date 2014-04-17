using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class UIThemeOperator : BindableBase
    {
        #region Bingding Property

        private ICollectionView _themesCollection;

        public ICollectionView ThemesCollection
        {
            get
            {
                if (_themesCollection == null)
                {
                    _themesCollection = CollectionViewSource.GetDefaultView(Theme.Themes.Where(t =>
                        !Equals(t, Theme.TouchlineDark)).Select(t => new UITheme(t)).ToArray());
                    _themesCollection.GroupDescriptions.Add(new PropertyGroupDescription("Theme.Category"));
                }
                return _themesCollection;
            }
        }

        private List<UITheme> _uiThemes;

        public List<UITheme> UIThemes
        {
            get
            {
                if (_uiThemes == null)
                {
                    _uiThemes = Theme.Themes.Where(t =>
                        !Equals(t, Theme.TouchlineDark)).Select(t => new UITheme(t)).ToList();

                    if (string.IsNullOrEmpty(ThemeManager.ApplicationThemeName))
                        CurrentTheme = _uiThemes[0];
                    else
                        foreach (var uiTheme in _uiThemes.Where(uiTheme =>
                            ThemeManager.ApplicationThemeName == uiTheme.Theme.Name))
                        {
                            CurrentTheme = uiTheme;
                        }
                }

                return _uiThemes;
            }
        }

        private UITheme _currentTheme;

        public UITheme CurrentTheme
        {
            get { return _currentTheme; }
            set { SetProperty(ref _currentTheme, value, () => CurrentTheme, OnCurrentChange); }
        }

        #endregion

        #region Private Method

        private void OnCurrentChange()
        {
            if (ThemeManager.ActualApplicationThemeName != _currentTheme.Theme.Name)
                ThemeManager.ApplicationThemeName = _currentTheme.Theme.Name;
        }

        #endregion
    }

    public class UITheme : BindableBase
    {
        public Theme Theme { get; private set; }

        public UITheme(Theme theme)
        {
            if (theme == null)
                throw new ArgumentNullException();
            Theme = theme;
        }
    }
}