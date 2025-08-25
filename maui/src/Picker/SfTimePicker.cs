﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Syncfusion.Maui.Toolkit.Themes;

namespace Syncfusion.Maui.Toolkit.Picker
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SfTimePicker"/> class that represents a control, used to select the time with in specified list of times.
    /// </summary>
    public class SfTimePicker : PickerBase, IParentThemeElement, IThemeElement
    {
        #region Fields

        /// <summary>
        /// Holds the hour column information.
        /// </summary>
        PickerColumn _hourColumn;

        /// <summary>
        /// Holds the minute column information.
        /// </summary>
        PickerColumn _minuteColumn;

        /// <summary>
        /// Holds the second column information.
        /// </summary>
        PickerColumn _secondColumn;

		/// <summary>
		/// Holds the 1/10th second column information.
		/// </summary>
		PickerColumn _millisecondColumn;

		/// <summary>
		/// Holds the meridiem column information.
		/// </summary>
		PickerColumn _meridiemColumn;

        /// <summary>
        /// Holds the picker column collection.
        /// </summary>
        ObservableCollection<PickerColumn> _columns;

        #endregion

        #region Bindable Properties

        /// <summary>
        /// Identifies the <see cref="HeaderView"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="HeaderView"/> dependency property.
        /// </value>
        public static readonly BindableProperty HeaderViewProperty =
            BindableProperty.Create(
                nameof(HeaderView),
                typeof(PickerHeaderView),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => new PickerHeaderView(),
                propertyChanged: OnHeaderViewChanged);

        /// <summary>
        /// Identifies the <see cref="ColumnHeaderView"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="ColumnHeaderView"/> dependency property.
        /// </value>
        public static readonly BindableProperty ColumnHeaderViewProperty =
           BindableProperty.Create(
               nameof(ColumnHeaderView),
               typeof(TimePickerColumnHeaderView),
               typeof(SfTimePicker),
               defaultValueCreator: bindable => new TimePickerColumnHeaderView(),
               propertyChanged: OnColumnHeaderViewChanged);

        /// <summary>
        /// Identifies the <see cref="SelectedTime"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SelectedTime"/> dependency property.
        /// </value>
        public static readonly BindableProperty SelectedTimeProperty =
            BindableProperty.Create(
                nameof(SelectedTime),
                typeof(TimeSpan?),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => GetDefaultTimeSpan(),
                propertyChanged: (x,y, z) => OnSelectedTimePropertyChanged(x,y,z));

        /// <summary>
        /// Identifies the <see cref="HourInterval"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="HourInterval"/> dependency property.
        /// </value>
        public static readonly BindableProperty HourIntervalProperty =
            BindableProperty.Create(
                nameof(HourInterval),
                typeof(int),
                typeof(SfTimePicker),
                1,
                propertyChanged: OnHourIntervalPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="MinuteInterval"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="MinuteInterval"/> dependency property.
        /// </value>
        public static readonly BindableProperty MinuteIntervalProperty =
            BindableProperty.Create(
                nameof(MinuteInterval),
                typeof(int),
                typeof(SfTimePicker),
                1,
                propertyChanged: OnMinuteIntervalPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="SecondInterval"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SecondInterval"/> dependency property.
        /// </value>
        public static readonly BindableProperty SecondIntervalProperty =
            BindableProperty.Create(
                nameof(SecondInterval),
                typeof(int),
                typeof(SfTimePicker),
                1,
                propertyChanged: OnSecondIntervalPropertyChanged);

		/// <summary>
		/// Identifies the <see cref="MillisecondInterval"/> dependency property.
		/// </summary>
		/// <value>
		/// The identifier for <see cref="MillisecondInterval"/> dependency property.
		/// </value>
		public static readonly BindableProperty MillisecondIntervalProperty =
			BindableProperty.Create(
				nameof(MillisecondInterval),
				typeof(int),
				typeof(SfTimePicker),
				1,
				propertyChanged: OnMillisecondIntervalPropertyChanged);

		/// <summary>
		/// Identifies the <see cref="Format"/> dependency property.
		/// </summary>
		/// <value>
		/// The identifier for <see cref="Format"/> dependency property.
		/// </value>
		public static readonly BindableProperty FormatProperty =
            BindableProperty.Create(
                nameof(Format),
                typeof(PickerTimeFormat),
                typeof(SfTimePicker),
                PickerTimeFormat.HH_mm_ss,
                propertyChanged: OnFormatPropertyChanged);

        /// <summary>
        /// Identifies the <see cref="SelectionChangedCommand"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SelectionChangedCommand"/> dependency property.
        /// </value>
        public static readonly BindableProperty SelectionChangedCommandProperty =
            BindableProperty.Create(
                nameof(SelectionChangedCommand),
                typeof(ICommand),
                typeof(SfTimePicker),
                null);

        /// <summary>
        /// Identifies the <see cref="MinimumTime"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="MinimumTime"/> dependency property.
        /// </value>
        public static readonly BindableProperty MinimumTimeProperty =
            BindableProperty.Create(
                nameof(MinimumTime),
                typeof(TimeSpan),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => TimeSpan.Zero,
                propertyChanged: OnMinimumTimePropertyChanged);

        /// <summary>
        /// Identifies the <see cref="MaximumTime"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="MaximumTime"/> dependency property.
        /// </value>
        public static readonly BindableProperty MaximumTimeProperty =
            BindableProperty.Create(
                nameof(MaximumTime),
                typeof(TimeSpan),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => new TimeSpan(23, 59, 59),
                propertyChanged: OnMaximumTimePropertyChanged);

        /// <summary>
        /// Identifies the <see cref="BlackoutTimes"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="BlackoutTimes"/> dependency property.
        /// </value>
        public static readonly BindableProperty BlackoutTimesProperty =
            BindableProperty.Create(
                nameof(BlackoutTimes),
                typeof(ObservableCollection<TimeSpan>),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => new ObservableCollection<TimeSpan>(),
                propertyChanged: OnBlackOutTimesPropertyChanged);

        #endregion

        #region Internal Bindable Properties

        /// <summary>
        /// Identifies the <see cref="TimePickerBackground"/> bindable property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="TimePickerBackground"/> bindable property.
        /// </value>
        internal static readonly BindableProperty TimePickerBackgroundProperty =
            BindableProperty.Create(
                nameof(TimePickerBackground),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#EEE8F4"),
                propertyChanged: OnTimePickerBackgroundChanged);

        /// <summary>
        /// Identifies the <see cref="HeaderBackground"/> dependency property.
        /// </summary>
        /// <value>
        /// Identifies the <see cref="HeaderBackground"/> bindable property.
        /// </value>
        internal static readonly BindableProperty HeaderBackgroundProperty =
            BindableProperty.Create(
                nameof(HeaderBackground),
                typeof(Brush),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => new SolidColorBrush(Color.FromArgb("#F7F2FB")),
                propertyChanged: OnHeaderBackgroundChanged);

        /// <summary>
        /// Identifies the <see cref="FooterBackground"/> dependency property.
        /// </summary>
        /// <value>
        /// Identifies the <see cref="FooterBackground"/> bindable property.
        /// </value>
        internal static readonly BindableProperty FooterBackgroundProperty =
            BindableProperty.Create(
                nameof(FooterBackground),
                typeof(Brush),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Brush.Transparent,
                propertyChanged: OnFooterBackgroundChanged);

        /// <summary>
        /// Identifies the <see cref="SelectionBackground"/> dependency property.
        /// </summary>
        /// <value>
        /// Identifies the <see cref="SelectionBackground"/> bindable property.
        /// </value>
        internal static readonly BindableProperty SelectionBackgroundProperty =
            BindableProperty.Create(
                nameof(SelectionBackground),
                typeof(Brush),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => new SolidColorBrush(Color.FromArgb("#6750A4")),
                propertyChanged: OnSelectionBackgroundChanged);

        /// <summary>
        /// Identifies the <see cref="SelectionStrokeColor"/> dependency property.
        /// </summary>
        /// <value>
        /// Identifies the <see cref="SelectionStrokeColor"/> bindable property.
        /// </value>
        internal static readonly BindableProperty SelectionStrokeColorProperty =
            BindableProperty.Create(
                nameof(SelectionStrokeColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Colors.Transparent,
                propertyChanged: OnSelectionStrokeColorChanged);

        /// <summary>
        /// Identifies the <see cref="SelectionCornerRadius"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SelectionCornerRadius"/> dependency property.
        /// </value>
        internal static readonly BindableProperty SelectionCornerRadiusProperty =
            BindableProperty.Create(
                nameof(SelectionCornerRadius),
                typeof(CornerRadius),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => new CornerRadius(20),
                propertyChanged: OnSelectionCornerRadiusChanged);

        /// <summary>
        /// Identifies the <see cref="HeaderDividerColor"/> dependency property.
        /// </summary>
        /// <value>
        /// Identifies the <see cref="HeaderDividerColor"/> bindable property.
        /// </value>
        internal static readonly BindableProperty HeaderDividerColorProperty =
            BindableProperty.Create(
                nameof(HeaderDividerColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#CAC4D0"),
                propertyChanged: OnHeaderDividerColorChanged);

        /// <summary>
        /// Identifies the <see cref="FooterDividerColor"/> dependency property.
        /// </summary>
        /// <value>
        /// Identifies the <see cref="FooterDividerColor"/> bindable property.
        /// </value>
        internal static readonly BindableProperty FooterDividerColorProperty =
            BindableProperty.Create(
                nameof(FooterDividerColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#CAC4D0"),
                propertyChanged: OnFooterDividerColorChanged);

        /// <summary>
        /// Identifies the <see cref="HeaderTextColor"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="HeaderTextColor"/> dependency property.
        /// </value>
        internal static readonly BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(
                nameof(HeaderTextColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#49454F"),
                propertyChanged: OnHeaderTextColorChanged);

        /// <summary>
        /// Identifies the <see cref="HeaderFontSize"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="HeaderFontSize"/> dependency property.
        /// </value>
        internal static readonly BindableProperty HeaderFontSizeProperty =
            BindableProperty.Create(
                nameof(HeaderFontSize),
                typeof(double),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => 16d,
                propertyChanged: OnHeaderFontSizeChanged);

        /// <summary>
        /// Identifies the <see cref="FooterTextColor"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="FooterTextColor"/> dependency property.
        /// </value>
        internal static readonly BindableProperty FooterTextColorProperty =
            BindableProperty.Create(
                nameof(FooterTextColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#6750A4"),
                propertyChanged: OnFooterTextColorChanged);

        /// <summary>
        /// Identifies the <see cref="FooterFontSize"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="FooterFontSize"/> dependency property.
        /// </value>
        internal static readonly BindableProperty FooterFontSizeProperty =
            BindableProperty.Create(
                nameof(FooterFontSize),
                typeof(double),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => 14d,
                propertyChanged: OnFooterFontSizeChanged);

        /// <summary>
        /// Identifies the <see cref="SelectedTextColor"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SelectedTextColor"/> dependency property.
        /// </value>
        internal static readonly BindableProperty SelectedTextColorProperty =
            BindableProperty.Create(
                nameof(SelectedTextColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Colors.White,
                propertyChanged: OnSelectedTextColorChanged);

        /// <summary>
        /// Identifies the <see cref="SelectionTextColor"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SelectionTextColor"/> dependency property.
        /// </value>
        internal static readonly BindableProperty SelectionTextColorProperty =
            BindableProperty.Create(
                nameof(SelectionTextColor),
                typeof(Color),
                typeof(SfPicker),
                defaultValueCreator: bindable => Color.FromArgb("#6750A4"),
                propertyChanged: OnSelectedTextColorChanged);

        /// <summary>
        /// Identifies the <see cref="SelectedFontSize"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="SelectedFontSize"/> dependency property.
        /// </value>
        internal static readonly BindableProperty SelectedFontSizeProperty =
            BindableProperty.Create(
                nameof(SelectedFontSize),
                typeof(double),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => 16d,
                propertyChanged: OnSelectedFontSizeChanged);

        /// <summary>
        /// Identifies the <see cref="NormalTextColor"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="NormalTextColor"/> dependency property.
        /// </value>
        internal static readonly BindableProperty NormalTextColorProperty =
            BindableProperty.Create(
                nameof(NormalTextColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#1C1B1F"),
                propertyChanged: OnNormalTextColorChanged);

        /// <summary>
        /// Identifies the <see cref="NormalFontSize"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="NormalFontSize"/> dependency property.
        /// </value>
        internal static readonly BindableProperty NormalFontSizeProperty =
            BindableProperty.Create(
                nameof(NormalFontSize),
                typeof(double),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => 16d,
                propertyChanged: OnNormalFontSizeChanged);

        /// <summary>
        /// Identifies the <see cref="DisabledTextColor"/> dependency property.
        /// </summary>
        /// <value>
        /// The identifier for <see cref="DisabledTextColor"/> dependency property.
        /// </value>
        internal static readonly BindableProperty DisabledTextColorProperty =
            BindableProperty.Create(
                nameof(DisabledTextColor),
                typeof(Color),
                typeof(SfTimePicker),
                defaultValueCreator: bindable => Color.FromArgb("#611C1B1F"),
                propertyChanged: OnDisabledTextColorChanged);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SfTimePicker"/> class.
        /// </summary>
        public SfTimePicker()
        {
            _hourColumn = new PickerColumn();
            _minuteColumn = new PickerColumn();
            _secondColumn = new PickerColumn();
			_millisecondColumn = new PickerColumn();
			_meridiemColumn = new PickerColumn();
            _columns = new ObservableCollection<PickerColumn>();
            Initialize();
            GeneratePickerColumns();
            BaseColumns = _columns;
            SelectionIndexChanged += OnPickerSelectionIndexChanged;
            BlackoutTimes.CollectionChanged += OnBlackoutTimes_CollectionChanged;
            BackgroundColor = TimePickerBackground;
            Dispatcher.Dispatch(() =>
            {
                InitializeTheme();
            });
            ColumnHeaderView.Parent = this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of header view. This property can be used to customize the header in SfTimePicker.
        /// </summary>
        /// <example>
        /// The following example demonstrates how to customize the header view of SfTimePicker.
        /// <code>
        /// <![CDATA[
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.HeaderView = new PickerHeaderView
        /// {
        ///     Text = "Select Time",
        ///     TextStyle = new PickerTextStyle
        ///     {
        ///         TextColor = Colors.Blue,
        ///         FontSize = 18,
        ///         FontAttributes = FontAttributes.Bold
        ///     },
        ///     Background = new SolidColorBrush(Colors.LightGray)
        /// };
        /// ]]>
        /// </code>
        /// </example>
        public PickerHeaderView HeaderView
        {
            get { return (PickerHeaderView)GetValue(HeaderViewProperty); }
            set { SetValue(HeaderViewProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of column header view. This property can be used to customize the header column in SfTimePicker.
        /// </summary>
        /// <example>
        /// The following example demonstrates how to customize the column header view of SfTimePicker.
        /// <code>
        /// <![CDATA[
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.ColumnHeaderView = new TimePickerColumnHeaderView
        /// {
        ///     Background = new SolidColorBrush(Colors.LightBlue),
        ///     Height = 40,
        ///     DividerColor = Colors.Gray,
        ///     TextStyle = new PickerTextStyle
        ///     {
        ///         TextColor = Colors.DarkBlue,
        ///         FontSize = 16
        ///     },
        ///     HourHeaderText = "Hour",
        ///     MinuteHeaderText = "Minute",
        ///     SecondHeaderText = "Second",
        ///     MeridiemHeaderText = "AM/PM"
        /// };
        /// ]]>
        /// </code>
        /// </example>
        public TimePickerColumnHeaderView ColumnHeaderView
        {
            get { return (TimePickerColumnHeaderView)GetValue(ColumnHeaderViewProperty); }
            set { SetValue(ColumnHeaderViewProperty, value); }
        }

        /// <summary>
        /// Gets or sets the time picker selection time in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.SelectedTime"/> is "TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)".</value>
        /// <example>
        /// The following examples demonstrate how to set the selected time in SfTimePicker.
        /// # [XAML](#tab/tabid-1)
        /// <code language="xaml">
        /// <![CDATA[
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                      SelectedTime="14:30:00" />
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-2)
        /// <code language="C#">
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.SelectedTime = new TimeSpan(14, 30, 0);
        /// </code>
        /// </example>
        public TimeSpan? SelectedTime
        {
            get { return (TimeSpan?)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the hour interval in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.HourInterval"/> is 1.</value>
        /// <example>
        /// The following examples demonstrate how to set the hour interval in SfTimePicker.
        /// # [XAML](#tab/tabid-3)
        /// <code language="xaml">
        /// <![CDATA[
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                      HourInterval="2" />
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-4)
        /// <code language="C#">
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.HourInterval = 2;
        /// </code>
        /// </example>
        public int HourInterval
        {
            get { return (int)GetValue(HourIntervalProperty); }
            set { SetValue(HourIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minute interval in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.MinuteInterval"/> is 1.</value>
        /// <example>
        /// The following examples demonstrate how to set the minute interval in SfTimePicker.
        /// # [XAML](#tab/tabid-5)
        /// <code language="xaml">
        /// <![CDATA[
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                      MinuteInterval="2" />
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-6)
        /// <code language="C#">
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.MinuteInterval = 2;
        /// </code>
        /// </example>
        public int MinuteInterval
        {
            get { return (int)GetValue(MinuteIntervalProperty); }
            set { SetValue(MinuteIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets the second interval in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.SecondInterval"/> is 1.</value>
        /// <example>
        /// The following examples demonstrate how to set the second interval in SfTimePicker.
        /// # [XAML](#tab/tabid-7)
        /// <code language="xaml">
        /// <![CDATA[
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                       SecondInterval="2" />
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-8)
        /// <code language="C#">
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.SecondInterval = 2;
        /// </code>
        /// </example>
        public int SecondInterval
        {
            get { return (int)GetValue(SecondIntervalProperty); }
            set { SetValue(SecondIntervalProperty, value); }
        }

		/// <summary>
		/// Gets or sets the millisecond interval in SfTimePicker.
		/// </summary>
		/// <value>The default value of <see cref="SfTimePicker.SecondInterval"/> is 1.</value>
		/// <example>
		/// The following examples demonstrate how to set the millisecond interval in SfTimePicker.
		/// # [XAML](#tab/tabid-7)
		/// <code language="xaml">
		/// <![CDATA[
		/// <Picker:SfTimePicker x:Name="TimePicker"
		///                       MillisecondInterval="100" />
		/// ]]>
		/// </code>
		/// # [C#](#tab/tabid-8)
		/// <code language="C#">
		/// SfTimePicker timePicker = new SfTimePicker();
		/// timePicker.MillisecondInterval = 100;
		/// </code>
		/// </example>
		public int MillisecondInterval
		{
			get { return (int)GetValue(MillisecondIntervalProperty); }
			set { SetValue(MillisecondIntervalProperty, value); }
		}

		/// <summary>
		/// Gets or sets the picker date format in SfTimePicker.
		/// </summary>
		/// <value>The default value of <see cref="SfTimePicker.Format"/> is <see cref="PickerTimeFormat.HH_mm_ss"/>.</value>
		/// <example>
		/// The following examples demonstrate how to set the time format in SfTimePicker.
		/// # [XAML](#tab/tabid-9)
		/// <code language="xaml">
		/// <![CDATA[
		/// <Picker:SfTimePicker x:Name="TimePicker"
		///                      Format="HH_mm" />
		/// ]]>
		/// </code>
		/// # [C#](#tab/tabid-10)
		/// <code language="C#">
		/// SfTimePicker timePicker = new SfTimePicker();
		/// timePicker.Format = PickerTimeFormat.HH_mm;
		/// </code>
		/// </example>
		public PickerTimeFormat Format
        {
            get { return (PickerTimeFormat)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selection changed command in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.SelectionChangedCommand"/> is null.</value>
        /// <example>
        /// The following example demonstrates how to set the selection changed command in SfTimePicker.
        /// # [XAML](#tab/tabid-11)
        /// <code Lang="XAML"><![CDATA[
        /// <ContentPage.BindingContext>
        ///    <local:ViewModel/>
        /// </ContentPage.BindingContext>
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                      SelectionChangedCommand="{Binding SelectionCommand}">
        /// </Picker:SfTimePicker>
        /// ]]></code>
        /// # [C#](#tab/tabid-12)
        /// <code Lang="C#"><![CDATA[
        /// public class ViewModel : INotifyPropertyChanged
        /// {
        ///    private Command selectionCommand;
        ///    public ICommand SelectionCommand {
        ///        get
        ///        {
        ///            return selectionCommand;
        ///        }
        ///        set
        ///        {
        ///            if (selectionCommand != value)
        ///            {
        ///                selectionCommand = value;
        ///                OnPropertyChanged(nameof(SelectionCommand));
        ///            }
        ///        }
        ///    }
        ///    public ViewModel()
        ///    {
        ///      SelectionCommand = new Command(SelectionChanged);
        ///    }
        ///    private void SelectionChanged()
        ///    {
        ///    }
        ///  }
        /// ]]></code>
        /// </example>
        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minimum time in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.MinimumTime"/> is <see cref="TimeSpan.Zero"/>.</value> 
        /// <example>
        /// The following examples demonstrate how to set the minimum time in SfTimePicker.
        /// # [XAML](#tab/tabid-13)
        /// <code language="xaml">
        /// <![CDATA[
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                      MinimumTime="09:00:00" />
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-14)
        /// <code language="C#">
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.MinimumTime = new TimeSpan(9, 0, 0);
        /// </code>
        /// </example>
        public TimeSpan MinimumTime
        {
            get { return (TimeSpan)GetValue(MinimumTimeProperty); }
            set { SetValue(MinimumTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum time in SfTimePicker.
        /// </summary>
        /// <value>The default value of <see cref="SfTimePicker.MaximumTime"/> is "TimeSpan(23, 59, 59)".</value>
        /// <example>
        /// The following examples demonstrate how to set the maximum time in SfTimePicker.
        /// # [XAML](#tab/tabid-15)
        /// <code language="xaml">
        /// <![CDATA[
        /// <Picker:SfTimePicker x:Name="TimePicker"
        ///                      MaximumTime="17:00:00" />
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-16)
        /// <code language="C#">
        /// SfTimePicker timePicker = new SfTimePicker();
        /// timePicker.MaximumTime = new TimeSpan(17, 0, 0);
        /// </code>
        /// </example>
        public TimeSpan MaximumTime
        {
            get { return (TimeSpan)GetValue(MaximumTimeProperty); }
            set { SetValue(MaximumTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the BlackoutTimes in SfTimePicker.
        /// </summary>
        /// <remarks>The selection view will not be applicable when setting blackout times.</remarks>
        /// <example>
        /// The following examples demonstrate how to set the blackout times in SfTimePicker.
        /// # [XAML](#tab/tabid-17)
        /// <code language="xaml">
        /// <![CDATA[
        /// <picker:SfTimePicker x:Name="picker">
        ///     <picker:SfTimePicker.BlackoutTimes>
        ///         <x:TimeSpan>12:28:00</x:TimeSpan>
        ///         <x:TimeSpan>12:26:00</x:TimeSpan>
        ///         <x:TimeSpan>12:24:00</x:TimeSpan>
        ///         <x:TimeSpan>12:22:00</x:TimeSpan>
        ///         <x:TimeSpan>12:37:00</x:TimeSpan>
        ///         <x:TimeSpan>12:35:00</x:TimeSpan>
        ///         <x:TimeSpan>12:33:00</x:TimeSpan>
        ///         <x:TimeSpan>12:32:00</x:TimeSpan>
        ///     </picker:SfTimePicker.BlackoutTimes>
        /// </picker:SfTimePicker>
        /// ]]>
        /// </code>
        /// # [C#](#tab/tabid-18)
        /// <code language="C#">
        /// SfTimePicker picker = new SfTimePicker();
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 28, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 26, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 24, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 22, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 37, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 35, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 33, 0));
        /// picker.BlackoutTimes.Add(new TimeSpan(12, 32, 0));
        /// </code>
        /// </example>
        public ObservableCollection<TimeSpan> BlackoutTimes
        {
            get { return (ObservableCollection<TimeSpan>)GetValue(BlackoutTimesProperty); }
            set { SetValue(BlackoutTimesProperty, value); }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the background color of the time picker.
        /// </summary>
        internal Color TimePickerBackground
        {
            get { return (Color)GetValue(TimePickerBackgroundProperty); }
            set { SetValue(TimePickerBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background of the header view in time picker.
        /// </summary>
        internal Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background of the footer view in SfTimePicker.
        /// </summary>
        internal Brush FooterBackground
        {
            get { return (Brush)GetValue(FooterBackgroundProperty); }
            set { SetValue(FooterBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background of the selection view in SfTimePicker.
        /// </summary>
        internal Brush SelectionBackground
        {
            get { return (Brush)GetValue(SelectionBackgroundProperty); }
            set { SetValue(SelectionBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the stroke color of the selection view in SfTimePicker.
        /// </summary>
        internal Color SelectionStrokeColor
        {
            get { return (Color)GetValue(SelectionStrokeColorProperty); }
            set { SetValue(SelectionStrokeColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the corner radius of the selection view in SfTimePicker.
        /// </summary>
        internal CornerRadius SelectionCornerRadius
        {
            get { return (CornerRadius)GetValue(SelectionCornerRadiusProperty); }
            set { SetValue(SelectionCornerRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the header separator line in time picker.
        /// </summary>
        internal Color HeaderDividerColor
        {
            get { return (Color)GetValue(HeaderDividerColorProperty); }
            set { SetValue(HeaderDividerColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background of the footer separator line background in SfTimePicker.
        /// </summary>
        internal Color FooterDividerColor
        {
            get { return (Color)GetValue(FooterDividerColorProperty); }
            set { SetValue(FooterDividerColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the header text color of the text style.
        /// </summary>
        internal Color HeaderTextColor
        {
            get { return (Color)GetValue(HeaderTextColorProperty); }
            set { SetValue(HeaderTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the header font size of the text style.
        /// </summary>
        internal double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the footer text color of the text style.
        /// </summary>
        internal Color FooterTextColor
        {
            get { return (Color)GetValue(FooterTextColorProperty); }
            set { SetValue(FooterTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the footer font size of the text style.
        /// </summary>
        internal double FooterFontSize
        {
            get { return (double)GetValue(FooterFontSizeProperty); }
            set { SetValue(FooterFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selection text color of the text style.
        /// </summary>
        /// <remarks>
        /// This color applicable for default text display mode.
        /// </remarks>
        internal Color SelectedTextColor
        {
            get { return (Color)GetValue(SelectedTextColorProperty); }
            set { SetValue(SelectedTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selection text color of the text style.
        /// </summary>
        /// <remarks>
        /// This color is used for Fade, Shrink and FadeAndShrink mode.
        /// </remarks>
        internal Color SelectionTextColor
        {
            get { return (Color)GetValue(SelectionTextColorProperty); }
            set { SetValue(SelectionTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selection font size of the text style.
        /// </summary>
        internal double SelectedFontSize
        {
            get { return (double)GetValue(SelectedFontSizeProperty); }
            set { SetValue(SelectedFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the normal text color of the text style.
        /// </summary>
        internal Color NormalTextColor
        {
            get { return (Color)GetValue(NormalTextColorProperty); }
            set { SetValue(NormalTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the normal font size of the text style.
        /// </summary>
        internal double NormalFontSize
        {
            get { return (double)GetValue(NormalFontSizeProperty); }
            set { SetValue(NormalFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the disabled text color of the text style.
        /// </summary>
        internal Color DisabledTextColor
        {
            get { return (Color)GetValue(DisabledTextColorProperty); }
            set { SetValue(DisabledTextColorProperty, value); }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Method to reset the picker columns based on the updated format.
        /// </summary>
        internal void UpdateFormat()
        {
            _hourColumn = new PickerColumn();
            _minuteColumn = new PickerColumn();
            _secondColumn = new PickerColumn();
			_millisecondColumn = new PickerColumn();
			_meridiemColumn = new PickerColumn();
            GeneratePickerColumns();
            BaseColumns = _columns;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method trigged whenever the base panel selection is changed.
        /// </summary>
        /// <param name="sender">Base picker instance value.</param>
        /// <param name="e">Selection changed event arguments.</param>
        void OnPickerSelectionIndexChanged(object? sender, PickerSelectionChangedEventArgs e)
        {
            string hourFormat;
            List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out hourFormat, Format);
            int changedColumnValue = formatStringOrder[e.ColumnIndex];
            TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
            TimeSpan previousTime = new TimeSpan(_previousSelectedDateTime.Hour, _previousSelectedDateTime.Minute, _previousSelectedDateTime.Second);
            TimeSpan currentTime = SelectedTime ?? previousTime;
            TimeSpan? previousSelectedTime = TimePickerHelper.GetValidSelectedTime(currentTime, MinimumTime, maxTime);
            DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
            DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());
            DateTime selectedTime = Convert.ToDateTime(previousSelectedTime.ToString());
            if (previousSelectedTime == null)
            {
                return;
            }

            switch (changedColumnValue)
            {
                case 0:
                    {
                        UpdateHourColumn(e, hourFormat, previousSelectedTime, selectedTime, minimumTime, maximumTime);
                    }

                    break;
                case 1:
                    {
						UpdateMinuteColumn(e, hourFormat, previousSelectedTime, selectedTime, minimumTime, maximumTime);

						int minutes = 0;
                        if (_minuteColumn.ItemsSource != null && _minuteColumn.ItemsSource is ObservableCollection<string> minuteCollection && minuteCollection.Count > e.NewValue)
                        {
                            //// Get the minute value based on the selected index changes value.
                            minutes = int.Parse(minuteCollection[e.NewValue]);
                        }

                        //SetSelectedTime(new TimeSpan(0, previousSelectedTime.Value.Hours, minutes, previousSelectedTime.Value.Seconds, previousSelectedTime.Value.Milliseconds));
                    }

                    break;
                case 2:
                    {
						UpdateSecondColumn(e, hourFormat, previousSelectedTime, selectedTime, minimumTime, maximumTime);

						int seconds = 0;
                        if (_secondColumn.ItemsSource != null && _secondColumn.ItemsSource is ObservableCollection<string> secondCollection && secondCollection.Count > e.NewValue)
                        {
                            //// Get the seconds value based on the selected index changes value.
                            seconds = int.Parse(secondCollection[e.NewValue]);
                        }

                        //SetSelectedTime(new TimeSpan(0, previousSelectedTime.Value.Hours, previousSelectedTime.Value.Minutes, seconds, previousSelectedTime.Value.Milliseconds));
                    }

                    break;
				case 3:
					{
						int milliseconds = 0;
						if (_millisecondColumn.ItemsSource != null && _millisecondColumn.ItemsSource is ObservableCollection<string> millisecondCollection && millisecondCollection.Count > e.NewValue)
						{
							//// Get the milliseconds value based on the selected index changes value.
							milliseconds = int.Parse(millisecondCollection[e.NewValue]);
						}

						SetSelectedTime(new TimeSpan(0,previousSelectedTime.Value.Hours, previousSelectedTime.Value.Minutes, previousSelectedTime.Value.Seconds, milliseconds));
					}

					break;
				case 4:
                    {
                        //UpdateMeridiemColumn(e, hourFormat, previousSelectedTime, selectedTime, minimumTime, maximumTime);
                    }

                    break;
            }
        }

        /// <summary>
        /// Method to update the hour column based on the selected time value.
        /// </summary>
        /// <param name="e">Selection changed event arguments.</param>
        /// <param name="hourFormat">The hour format.</param>
        /// <param name="previousSelectedTime">The previous selected time.</param>
        /// <param name="selectedTime">The selected time.</param>
        /// <param name="minimumTime">The minimum time.</param>
        /// <param name="maximumTime">The maximum time.</param>
        void UpdateHourColumn(PickerSelectionChangedEventArgs e, string hourFormat, TimeSpan? previousSelectedTime, DateTime selectedTime, DateTime minimumTime, DateTime maximumTime)
        {
            if (previousSelectedTime == null)
            {
                return;
            }

            int hour = 0;
            if (_hourColumn.ItemsSource != null && _hourColumn.ItemsSource is ObservableCollection<string> hourCollection && hourCollection.Count > e.NewValue)
            {
                //// Get the hour value based on the selected index changes value.
                hour = int.Parse(hourCollection[e.NewValue]);
            }

            if (hourFormat == "h" || hourFormat == "hh")
            {
                hour = hour == 12 ? 0 : hour;
                if (previousSelectedTime.Value.Hours >= 12)
                {
                    hour += 12;
                }
            }

            ObservableCollection<string> minutes = TimePickerHelper.GetMinutes(MinuteInterval, hour, selectedTime, minimumTime, maximumTime);
            ObservableCollection<string> previousMinutes = _minuteColumn.ItemsSource is ObservableCollection<string> previousMinuteCollection ? previousMinuteCollection : new ObservableCollection<string>();
            if (!PickerHelper.IsCollectionEquals(minutes, previousMinutes))
            {
                _minuteColumn.ItemsSource = minutes;
            }

			int minute = 0;
			if (_minuteColumn.ItemsSource != null && _minuteColumn.ItemsSource is ObservableCollection<string> minuteCollection && minuteCollection.Count > previousSelectedTime.Value.Minutes)
			{
				minute = previousSelectedTime.Value.Minutes;
			}

			ObservableCollection<string> seconds = TimePickerHelper.GetSeconds(SecondInterval, hour, minute, selectedTime, minimumTime, maximumTime);
			ObservableCollection<string> previousSeconds = _secondColumn.ItemsSource is ObservableCollection<string> previousSecondCollection ? previousSecondCollection : new ObservableCollection<string>();
			if (!PickerHelper.IsCollectionEquals(seconds, previousSeconds))
			{
				_secondColumn.ItemsSource = seconds;
			}

			int second = 0;
			if (_secondColumn.ItemsSource != null && _secondColumn.ItemsSource is ObservableCollection<string> secondCollection && secondCollection.Count > previousSelectedTime.Value.Seconds)
			{
				second = previousSelectedTime.Value.Seconds;
			}

			ObservableCollection<string> milliseconds = TimePickerHelper.GetMilliseconds(MillisecondInterval, hour, minute, second, selectedTime, minimumTime, maximumTime);
			ObservableCollection<string> previousMilliseconds = _millisecondColumn.ItemsSource is ObservableCollection<string> previousMillisecondCollection ? previousMillisecondCollection : new ObservableCollection<string>();
			if (!PickerHelper.IsCollectionEquals(milliseconds, previousMilliseconds))
			{
				_millisecondColumn.ItemsSource = milliseconds;
			}

			SetSelectedTime(new TimeSpan(0,hour, minute, second, previousSelectedTime.Value.Milliseconds));
        }

		/// <summary>
		/// Method to update the minute column based on the selected time value.
		/// </summary>
		/// <param name="e">Selection changed event arguments.</param>
		/// <param name="hourFormat">The hour format.</param>
		/// <param name="previousSelectedTime">The previous selected time.</param>
		/// <param name="selectedTime">The selected time.</param>
		/// <param name="minimumTime">The minimum time.</param>
		/// <param name="maximumTime">The maximum time.</param>
		void UpdateMinuteColumn(PickerSelectionChangedEventArgs e, string hourFormat, TimeSpan? previousSelectedTime, DateTime selectedTime, DateTime minimumTime, DateTime maximumTime)
		{
			if (previousSelectedTime == null)
			{
				return;
			}

			int hour = 0;

			hour = previousSelectedTime.Value.Hours;

			int minute = 0;
			if (_minuteColumn.ItemsSource != null && _minuteColumn.ItemsSource is ObservableCollection<string> minuteCollection && minuteCollection.Count > e.NewValue)
			{
				//// Get the hour value based on the selected index changes value.
				minute = int.Parse(minuteCollection[e.NewValue]);
			}

			ObservableCollection<string> seconds = TimePickerHelper.GetSeconds(SecondInterval, hour, minute, selectedTime, minimumTime, maximumTime);
			ObservableCollection<string> previousSeconds = _secondColumn.ItemsSource is ObservableCollection<string> previousSecondCollection ? previousSecondCollection : new ObservableCollection<string>();
			if (!PickerHelper.IsCollectionEquals(seconds, previousSeconds))
			{
				_secondColumn.ItemsSource = seconds;
			}

			int second = 0;
			if (_secondColumn.ItemsSource != null && _secondColumn.ItemsSource is ObservableCollection<string> secondCollection && secondCollection.Count > previousSelectedTime.Value.Seconds)
			{
				second = previousSelectedTime.Value.Seconds;
			}

			ObservableCollection<string> milliseconds = TimePickerHelper.GetMilliseconds(MillisecondInterval, hour, minute, second, selectedTime, minimumTime, maximumTime);
			ObservableCollection<string> previousMilliseconds = _millisecondColumn.ItemsSource is ObservableCollection<string> previousMillisecondCollection ? previousMillisecondCollection : new ObservableCollection<string>();
			if (!PickerHelper.IsCollectionEquals(milliseconds, previousMilliseconds))
			{
				_millisecondColumn.ItemsSource = milliseconds;
			}

			int millisecond = 0;
			if (_millisecondColumn.ItemsSource != null && _millisecondColumn.ItemsSource is ObservableCollection<string> millisecondCollection && millisecondCollection.Count > (previousSelectedTime.Value.Milliseconds / this.MillisecondInterval))
			{
				millisecond = previousSelectedTime.Value.Milliseconds;
			}

			SetSelectedTime(new TimeSpan(0,hour, minute, second, millisecond));
		}

		/// <summary>
		/// Method to update the second column based on the selected time value.
		/// </summary>
		/// <param name="e">Selection changed event arguments.</param>
		/// <param name="hourFormat">The hour format.</param>
		/// <param name="previousSelectedTime">The previous selected time.</param>
		/// <param name="selectedTime">The selected time.</param>
		/// <param name="minimumTime">The minimum time.</param>
		/// <param name="maximumTime">The maximum time.</param>
		void UpdateSecondColumn(PickerSelectionChangedEventArgs e, string hourFormat, TimeSpan? previousSelectedTime, DateTime selectedTime, DateTime minimumTime, DateTime maximumTime)
		{
			if (previousSelectedTime == null)
			{
				return;
			}

			int hour = 0;

			hour = previousSelectedTime.Value.Hours;

			int minute = 0;

			minute = previousSelectedTime.Value.Minutes;
			//if (_minuteColumn.ItemsSource != null && _minuteColumn.ItemsSource is ObservableCollection<string> minuteCollection && minuteCollection.Count > e.NewValue)
			//{
			//	//// Get the hour value based on the selected index changes value.
			//	minute = int.Parse(minuteCollection[e.NewValue]);
			//}

			int second = 0;
			if (_secondColumn.ItemsSource != null && _secondColumn.ItemsSource is ObservableCollection<string> secondCollection && secondCollection.Count > e.NewValue)
			{
				//// Get the hour value based on the selected index changes value.
				second = int.Parse(secondCollection[e.NewValue]);
			}

			ObservableCollection<string> milliseconds = TimePickerHelper.GetMilliseconds(MillisecondInterval, hour, minute, second, selectedTime, minimumTime, maximumTime);
			ObservableCollection<string> previousMilliseconds = _millisecondColumn.ItemsSource is ObservableCollection<string> previousMillisecondCollection ? previousMillisecondCollection : new ObservableCollection<string>();
			if (!PickerHelper.IsCollectionEquals(milliseconds, previousMilliseconds))
			{
				_millisecondColumn.ItemsSource = milliseconds;
			}

			int millisecond = 0;
			if (_millisecondColumn.ItemsSource != null && _millisecondColumn.ItemsSource is ObservableCollection<string> millisecondCollection && millisecondCollection.Count > (previousSelectedTime.Value.Milliseconds / this.MillisecondInterval))
			{
				millisecond = previousSelectedTime.Value.Milliseconds;
			}

			SetSelectedTime(new TimeSpan(0, hour, minute, second, millisecond));
		}



		/// <summary>
		/// Method to update the meridiem column based on the selected time value.
		/// </summary>
		/// <param name="e">Selection changed event arguments.</param>
		/// <param name="hourFormat">The hour format.</param>
		/// <param name="previousSelectedTime">The previous selected time.</param>
		/// <param name="selectedTime">The selected time.</param>
		/// <param name="minimumTime">The minimum time.</param>
		/// <param name="maximumTime">The maximum time.</param>
		void UpdateMeridiemColumn(PickerSelectionChangedEventArgs e, string hourFormat, TimeSpan? previousSelectedTime, DateTime selectedTime, DateTime minimumTime, DateTime maximumTime)
        {
            if (previousSelectedTime == null)
            {
                return;
            }

            ObservableCollection<string> meridiemCollection = new ObservableCollection<string>();
            if (_meridiemColumn.ItemsSource != null && _meridiemColumn.ItemsSource is ObservableCollection<string> meridiems)
            {
                meridiemCollection = meridiems;
            }

            if (meridiemCollection.Count <= e.NewValue)
            {
                return;
            }

            bool isAMSelected = TimePickerHelper.IsAMText(meridiemCollection, e.NewValue);
            int neededHour = isAMSelected ? 0 : 12;

            TimeSpan selectedDate = new TimeSpan((previousSelectedTime.Value.Hours % 12) + neededHour, previousSelectedTime.Value.Minutes, previousSelectedTime.Value.Seconds);
            DateTime selectedDates = Convert.ToDateTime(selectedDate.ToString());

            ObservableCollection<string> hours = TimePickerHelper.GetHours(hourFormat, HourInterval, selectedDates, minimumTime, maximumTime);
            ObservableCollection<string> previousHour = _hourColumn.ItemsSource is ObservableCollection<string> previousHourCollection ? previousHourCollection : new ObservableCollection<string>();
            if (!PickerHelper.IsCollectionEquals(hours, previousHour))
            {
                _hourColumn.ItemsSource = hours;
            }

            int? hourIndex = TimePickerHelper.GetHourIndex(hourFormat, hours, previousSelectedTime.Value.Hours);
            if (hourIndex.HasValue)
            {
                int hour = int.Parse(hours[hourIndex.Value]);
                hour = (hour % 12) + neededHour;

                ObservableCollection<string> minutes = TimePickerHelper.GetMinutes(MinuteInterval, hour, selectedTime, minimumTime, maximumTime);
                ObservableCollection<string> previousMinutes = _minuteColumn.ItemsSource is ObservableCollection<string> previousMinuteCollection ? previousMinuteCollection : new ObservableCollection<string>();
                if (!PickerHelper.IsCollectionEquals(minutes, previousMinutes))
                {
                    _minuteColumn.ItemsSource = minutes;
                }

                int minuteIndex = TimePickerHelper.GetMinuteOrSecondIndex(minutes, previousSelectedTime.Value.Minutes);
                //// Get the minute value based on the selected index changes value.
                int minute = int.Parse(minutes[minuteIndex]);

                SetSelectedTime(new TimeSpan(hour, minute, previousSelectedTime.Value.Seconds));
            }
        }

        /// <summary>
        /// Method to set the Selected Time value.
        /// </summary>
        /// <param name="selectedTime">The selected time.</param>
        void SetSelectedTime(TimeSpan selectedTime)
        {
            if (!TimePickerHelper.IsSameTimeSpan(selectedTime, SelectedTime))
            {
                SelectedTime = selectedTime;
            }
        }

        /// <summary>
        /// Method invokes on column header property changed.
        /// </summary>
        /// <param name="sender">Column header view value.</param>
        /// <param name="e">Property changed arguments.</param>
        void OnColumnHeaderPropertyChanged(object? sender, PickerPropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimePickerColumnHeaderView.Background))
            {
                BaseColumnHeaderView.Background = ColumnHeaderView.Background;
            }
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.Height))
            {
                BaseColumnHeaderView.Height = ColumnHeaderView.Height;
            }
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.DividerColor))
            {
                BaseColumnHeaderView.DividerColor = ColumnHeaderView.DividerColor;
            }
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.TextStyle))
            {
                SetInheritedBindingContext(ColumnHeaderView.TextStyle, BindingContext);
                BaseColumnHeaderView.TextStyle = ColumnHeaderView.TextStyle;
            }
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.HourHeaderText))
            {
                _hourColumn.HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.HourHeaderText);
            }
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.MinuteHeaderText))
            {
                _minuteColumn.HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MinuteHeaderText);
            }
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.SecondHeaderText))
            {
                _secondColumn.HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.SecondHeaderText);
			}
			else if (e.PropertyName == nameof(TimePickerColumnHeaderView.MillisecondHeaderText))
			{
				_millisecondColumn.HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MillisecondHeaderText);
			}
            else if (e.PropertyName == nameof(TimePickerColumnHeaderView.MeridiemHeaderText))
            {
                _meridiemColumn.HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MeridiemHeaderText);
            }
        }

        /// <summary>
        /// Method to generate the hour, minute, second and meridiem columns based on the selected time value.
        /// </summary>
        void GeneratePickerColumns()
        {
            string hourFormat;
            List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out hourFormat, Format);
            TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
            TimeSpan? validSelectedTime = TimePickerHelper.GetValidSelectedTime(SelectedTime, MinimumTime, maxTime);
            DateTime currentSelectedTime = validSelectedTime != null ? Convert.ToDateTime(validSelectedTime.ToString()) : DateTime.Now;
            ObservableCollection<PickerColumn> pickerColumns = new ObservableCollection<PickerColumn>();
            foreach (int index in formatStringOrder)
            {
                switch (index)
                {
                    case 0:
                        _hourColumn = GenerateHourColumn(hourFormat, validSelectedTime, currentSelectedTime);
                        _hourColumn.SelectedItem = SelectedTime != null ? PickerHelper.GetSelectedItemDefaultValue(_hourColumn) : null;
                        pickerColumns.Add(_hourColumn);
                        break;
                    case 1:
                        _minuteColumn = GenerateMinuteColumn(validSelectedTime, currentSelectedTime);
                        _minuteColumn.SelectedItem = SelectedTime != null ? PickerHelper.GetSelectedItemDefaultValue(_minuteColumn) : null;
                        pickerColumns.Add(_minuteColumn);
                        break;
                    case 2:
                        _secondColumn = GenerateSecondColumn(validSelectedTime, currentSelectedTime);
                        _secondColumn.SelectedItem = SelectedTime != null ? PickerHelper.GetSelectedItemDefaultValue(_secondColumn) : null;
                        pickerColumns.Add(_secondColumn);
                        break;
					case 3:
						_millisecondColumn = GenerateMillisecondColumn(validSelectedTime, currentSelectedTime);
						_millisecondColumn.SelectedItem = SelectedTime != null ? PickerHelper.GetSelectedItemDefaultValue(_millisecondColumn) : null;
						pickerColumns.Add(_millisecondColumn);
						break;
					case 4:
                        _meridiemColumn = GenerateMeridiemColumn(validSelectedTime, currentSelectedTime);
                        _meridiemColumn.SelectedItem = SelectedTime != null ? PickerHelper.GetSelectedItemDefaultValue(_meridiemColumn) : null;
                        pickerColumns.Add(_meridiemColumn);
                        break;
                }
            }

            _columns = pickerColumns;
        }

        /// <summary>
        /// Method to generate the hour column with items source and selected index based on format.
        /// </summary>
        /// <param name="format">The hour format.</param>
        /// <param name="selectedTime">The selected time.</param>
        /// <param name="selectedDate">The current date.</param>
        /// <returns>Returns hour column details.</returns>
        PickerColumn GenerateHourColumn(string format, TimeSpan? selectedTime, DateTime? selectedDate)
        {
            TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
            DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
            DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());

            ObservableCollection<string> hours = TimePickerHelper.GetHours(format, HourInterval, selectedDate, minimumTime, maximumTime);

            int? hourIndex = selectedTime != null ? TimePickerHelper.GetHourIndex(format, hours, selectedTime.Value.Hours) : _previousSelectedDateTime.Hour;

            return new PickerColumn()
            {
                ItemsSource = hours,
                SelectedIndex = hourIndex != null ? (int)hourIndex : _previousSelectedDateTime.Hour,
                HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.HourHeaderText),
            };
        }

        /// <summary>
        /// Method to generate the minute column with items source and selected index based on format.
        /// </summary>
        /// <param name="selectedTime">The selected time.</param>
        /// <param name="selectedDate">The current date.</param>
        /// <returns>Returns minute column details.</returns>
        PickerColumn GenerateMinuteColumn(TimeSpan? selectedTime, DateTime? selectedDate)
        {
            TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
            DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
            DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());
            int selectedHour = selectedTime?.Hours ?? _previousSelectedDateTime.Hour;
            int selectedMinute = selectedTime?.Minutes ?? _previousSelectedDateTime.Minute;

            ObservableCollection<string> minutes = TimePickerHelper.GetMinutes(MinuteInterval, selectedHour, selectedDate, minimumTime, maximumTime);
            return new PickerColumn
            {
                ItemsSource = minutes,
                SelectedIndex = TimePickerHelper.GetMinuteOrSecondIndex(minutes, selectedMinute),
                HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MinuteHeaderText),
            };
        }

        /// <summary>
        /// Method to generate the second column with items source and selected index based on format.
        /// </summary>
        /// <returns>Returns second column details.</returns>
        PickerColumn GenerateSecondColumn(TimeSpan? selectedTime, DateTime? selectedDate)
        {
			TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
			DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
			DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());
			int selectedHour = SelectedTime?.Hours ?? _previousSelectedDateTime.Hour;
            int selectedMinute = SelectedTime?.Minutes ?? _previousSelectedDateTime.Minute;
            int selectedSecond = SelectedTime?.Seconds ?? _previousSelectedDateTime.Second;

            ObservableCollection<string> seconds = TimePickerHelper.GetSeconds(SecondInterval, selectedHour, selectedMinute, selectedDate, minimumTime, maximumTime);
            return new PickerColumn()
            {
                ItemsSource = seconds,
                SelectedIndex = TimePickerHelper.GetMinuteOrSecondIndex(seconds, selectedSecond),
                HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.SecondHeaderText),
            };
        }

		/// <summary>
		/// Method to generate the millisecond column with items source and selected index based on format.
		/// </summary>
		/// <returns>Returns second column details.</returns>
		PickerColumn GenerateMillisecondColumn(TimeSpan? selectedTime, DateTime? selectedDate)
		{
			TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
			DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
			DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());
			int selectedHour = SelectedTime?.Hours ?? _previousSelectedDateTime.Hour;
			int selectedMinute = SelectedTime?.Minutes ?? _previousSelectedDateTime.Minute;
			int selectedSecond = SelectedTime?.Seconds ?? _previousSelectedDateTime.Second;
			int selectedMillisecond = SelectedTime?.Milliseconds ?? _previousSelectedDateTime.Millisecond;

			ObservableCollection<string> milliseconds = TimePickerHelper.GetMilliseconds(MillisecondInterval, selectedHour, selectedMinute, selectedSecond, selectedDate, minimumTime, maximumTime);
			return new PickerColumn()
			{
				ItemsSource = milliseconds,
				SelectedIndex = TimePickerHelper.GetMillisecondIndex(milliseconds, selectedMillisecond),
				HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MillisecondHeaderText),
			};
		}

		/// <summary>
		/// Method to generate the meridiem column with items source and selected index based on format.
		/// </summary>
		/// <param name="selectedTime">The selected time.</param>
		/// <param name="selectedDate">The current date.</param>
		/// <returns>Returns meridiem column details.</returns>
		PickerColumn GenerateMeridiemColumn(TimeSpan? selectedTime, DateTime? selectedDate)
        {
            TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
            DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
            DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());
            int selectedHour = selectedTime?.Hours ?? _previousSelectedDateTime.Hour;
            ObservableCollection<string> meridiems = TimePickerHelper.GetMeridiem(minimumTime, maximumTime, selectedDate);
            return new PickerColumn()
            {
                ItemsSource = meridiems,
                SelectedIndex = selectedHour >= 12 ? 1 : 0,
                HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MeridiemHeaderText),
            };
        }

        /// <summary>
        /// Method to update the selected index value for all the picker column based on the selected time value.
        /// </summary>
        void UpdateSelectedIndex()
        {
            if (SelectedTime == null)
            {
                return;
            }

            string hourFormat;
            TimePickerHelper.GetFormatStringOrder(out hourFormat, Format);
            if (_hourColumn.ItemsSource != null && _hourColumn.ItemsSource is ObservableCollection<string> hourCollection && hourCollection.Count > 0)
            {
                int? index = TimePickerHelper.GetHourIndex(hourFormat, hourCollection, SelectedTime.Value.Hours);
                if (index == null)
                {
                    return;
                }

                if (_hourColumn.SelectedIndex != index)
                {
                    _hourColumn.SelectedIndex = (int)index;
                }
            }

            if (_minuteColumn.ItemsSource != null && _minuteColumn.ItemsSource is ObservableCollection<string> minuteCollection && minuteCollection.Count > 0)
            {
                int index = TimePickerHelper.GetMinuteOrSecondIndex(minuteCollection, SelectedTime.Value.Minutes);
                if (_minuteColumn.SelectedIndex != index)
                {
                    _minuteColumn.SelectedIndex = index;
                }
            }

            if (_secondColumn.ItemsSource != null && _secondColumn.ItemsSource is ObservableCollection<string> secondCollection && secondCollection.Count > 0)
            {
                int index = TimePickerHelper.GetMinuteOrSecondIndex(secondCollection, SelectedTime.Value.Seconds);
                if (_secondColumn.SelectedIndex != index)
                {
                    _secondColumn.SelectedIndex = index;
                }
            }

			if (_millisecondColumn.ItemsSource != null && _millisecondColumn.ItemsSource is ObservableCollection<string> millisecondCollection && millisecondCollection.Count > 0)
			{
				int index = TimePickerHelper.GetMillisecondIndex(millisecondCollection, SelectedTime.Value.Milliseconds);
				if (_millisecondColumn.SelectedIndex != index)
				{
					_millisecondColumn.SelectedIndex = index;
				}
			}

			if (_meridiemColumn.ItemsSource != null && _meridiemColumn.ItemsSource is ObservableCollection<string> meridiemCollection && meridiemCollection.Count > 0)
            {
                int index = SelectedTime.Value.Hours >= 12 ? 1 : 0;
                if (_meridiemColumn.SelectedIndex != index)
                {
                    _meridiemColumn.SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// Method to update the minimum and maximum time value for all the picker column based on the time value.
        /// </summary>
        void UpdateMinimumMaximumTime(object oldValue, object newValue)
        {
            TimeSpan? oldTime = (TimeSpan)oldValue;
            TimeSpan? newTime = (TimeSpan)newValue;
            TimeSpan maxTime = TimePickerHelper.GetValidMaxTime(MinimumTime, MaximumTime);
            TimeSpan? validSelectedTime = TimePickerHelper.GetValidSelectedTime(SelectedTime, MinimumTime, maxTime);
            DateTime minimumTime = Convert.ToDateTime(MinimumTime.ToString());
            DateTime maximumTime = Convert.ToDateTime(maxTime.ToString());
            DateTime currentSelectedTime = Convert.ToDateTime(validSelectedTime.ToString());

            string hourFormat;
            List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out hourFormat, Format);
            int index = formatStringOrder.IndexOf(0);
            if (index != -1 && oldTime.Value.Hours != newTime.Value.Hours)
            {
                _hourColumn = GenerateHourColumn(hourFormat, SelectedTime, currentSelectedTime);
                int hourIndex = index;
                _columns[hourIndex] = _hourColumn;
            }

            index = formatStringOrder.IndexOf(1);
            if (index != -1 && (currentSelectedTime.Hour == oldTime.Value.Hours || currentSelectedTime.Hour == newTime.Value.Hours))
            {
                ObservableCollection<string> minutes = TimePickerHelper.GetMinutes(MinuteInterval, currentSelectedTime.Hour, currentSelectedTime, minimumTime, maximumTime);
                ObservableCollection<string> previousMinutes = _minuteColumn.ItemsSource is ObservableCollection<string> previousMinuteCollection ? previousMinuteCollection : new ObservableCollection<string>();
                if (!PickerHelper.IsCollectionEquals(minutes, previousMinutes))
                {
                    _minuteColumn = new PickerColumn()
                    {
                        ItemsSource = minutes,
                        SelectedIndex = TimePickerHelper.GetMinuteOrSecondIndex(minutes, currentSelectedTime.Minute),
                        HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MinuteHeaderText),
                    };
                    _columns[index] = _minuteColumn;
                }
            }

            index = formatStringOrder.IndexOf(2);
            if (index != -1 && ((currentSelectedTime.Hour == oldTime.Value.Hours && currentSelectedTime.Minute == oldTime.Value.Minutes) || (currentSelectedTime.Hour == newTime.Value.Hours && currentSelectedTime.Minute == newTime.Value.Minutes)))
            {
                ObservableCollection<string> seconds = TimePickerHelper.GetSeconds(SecondInterval, currentSelectedTime.Hour, currentSelectedTime.Minute, currentSelectedTime, minimumTime, maximumTime);
                ObservableCollection<string> previousSeconds = _secondColumn.ItemsSource is ObservableCollection<string> previousSecondCollection ? previousSecondCollection : new ObservableCollection<string>();
                if (!PickerHelper.IsCollectionEquals(seconds, previousSeconds))
                {
                    _secondColumn = new PickerColumn()
                    {
                        ItemsSource = seconds,
                        SelectedIndex = TimePickerHelper.GetMinuteOrSecondIndex(seconds, currentSelectedTime.Second),
                        HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.SecondHeaderText),
                    };
                    _columns[index] = _secondColumn;
                }
            }

			index = formatStringOrder.IndexOf(3);
			if (index != -1 && ((currentSelectedTime.Hour == oldTime.Value.Hours && currentSelectedTime.Minute == oldTime.Value.Minutes && currentSelectedTime.Second == oldTime.Value.Seconds) || (currentSelectedTime.Hour == newTime.Value.Hours && currentSelectedTime.Minute == newTime.Value.Minutes && currentSelectedTime.Second == newTime.Value.Seconds)))
			{
				ObservableCollection<string> milliseconds = TimePickerHelper.GetMilliseconds(MillisecondInterval, currentSelectedTime.Hour, currentSelectedTime.Minute, currentSelectedTime.Second, currentSelectedTime, minimumTime, maximumTime);
				ObservableCollection<string> previousMilliseconds = _millisecondColumn.ItemsSource is ObservableCollection<string> previousMillisecondCollection ? previousMillisecondCollection : new ObservableCollection<string>();
				if (!PickerHelper.IsCollectionEquals(milliseconds, previousMilliseconds))
				{
					_millisecondColumn = new PickerColumn()
					{
						ItemsSource = milliseconds,
						SelectedIndex = TimePickerHelper.GetMillisecondIndex(milliseconds, currentSelectedTime.Millisecond),
						HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MillisecondHeaderText),
					};
					_columns[index] = _millisecondColumn;
				}
			}

			index = formatStringOrder.IndexOf(4);
            if (index != -1 && ((int)(currentSelectedTime.Hour / 12) == (int)(oldTime.Value.Hours / 12) || (int)(currentSelectedTime.Hour / 12) == (int)(newTime.Value.Hours / 12)))
            {
                ObservableCollection<string> meridiems = TimePickerHelper.GetMeridiem(minimumTime, maximumTime, currentSelectedTime);
                ObservableCollection<string> previousCollection = _meridiemColumn.ItemsSource is ObservableCollection<string> previousMeridiemCollection ? previousMeridiemCollection : new ObservableCollection<string>();
                if (!PickerHelper.IsCollectionEquals(meridiems, previousCollection))
                {
                    _meridiemColumn = new PickerColumn()
                    {
                        ItemsSource = meridiems,
                        SelectedIndex = currentSelectedTime.Hour >= 12 ? meridiems.Count > 1 ? 1 : 0 : 0,
                        HeaderText = SfPickerResources.GetLocalizedString(ColumnHeaderView.MeridiemHeaderText),
                    };
                    _columns[index] = _meridiemColumn;
                }
            }
        }

        /// <summary>
        /// Method trigged when the black out time collection gets changed.
        /// </summary>
        /// <param name="sender">time picker instance</param>
        /// <param name="e">collection changed event arguments</param>
        void OnBlackoutTimes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (SelectedTime != null)
            {
                TimeSpan currentTime = SelectedTime.Value;
                while (BlackoutTimes.Any(blackOutTime => TimePickerHelper.IsBlackoutTime(blackOutTime, currentTime)))
                {
                    currentTime = currentTime.Add(TimeSpan.FromMinutes(1));
                }

                if (SelectedTime != currentTime)
                {
                    SelectedTime = currentTime;
                }
            }
        }

        /// <summary>
        /// Need to update the parent for the new value.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        void SetParent(Element? oldValue, Element? newValue)
        {
            if (oldValue != null)
            {
                oldValue.Parent = null;
            }

            if (newValue != null)
            {
                newValue.Parent = this;
            }
        }

        /// <summary>
        /// Method to initialize the theme and to set dynamic resources.
        /// </summary>
        void InitializeTheme()
        {
            ThemeElement.InitializeThemeResources(this, "SfTimePickerTheme");
            SetDynamicResource(TimePickerBackgroundProperty, "SfTimePickerNormalBackground");

            SetDynamicResource(HeaderBackgroundProperty, "SfTimePickerNormalHeaderBackground");
            SetDynamicResource(HeaderDividerColorProperty, "SfTimePickerNormalHeaderDividerColor");
            SetDynamicResource(HeaderTextColorProperty, "SfTimePickerNormalHeaderTextColor");
            SetDynamicResource(HeaderFontSizeProperty, "SfTimePickerNormalHeaderFontSize");

            SetDynamicResource(FooterBackgroundProperty, "SfTimePickerNormalFooterBackground");
            SetDynamicResource(FooterDividerColorProperty, "SfTimePickerNormalFooterDividerColor");
            SetDynamicResource(FooterTextColorProperty, "SfTimePickerNormalFooterTextColor");
            SetDynamicResource(FooterFontSizeProperty, "SfTimePickerNormalFooterFontSize");

            SetDynamicResource(SelectionBackgroundProperty, "SfTimePickerSelectionBackground");
            SetDynamicResource(SelectionStrokeColorProperty, "SfTimePickerSelectionStroke");
            SetDynamicResource(SelectionCornerRadiusProperty, "SfTimePickerSelectionCornerRadius");
            SetDynamicResource(SelectedTextColorProperty, "SfTimePickerSelectedTextColor");
            SetDynamicResource(SelectionTextColorProperty, "SfPickerSelectionTextColor");
            SetDynamicResource(SelectedFontSizeProperty, "SfTimePickerSelectedFontSize");

            SetDynamicResource(NormalTextColorProperty, "SfTimePickerNormalTextColor");
            SetDynamicResource(NormalFontSizeProperty, "SfTimePickerNormalFontSize");

            SetDynamicResource(DisabledTextColorProperty, "SfTimePickerDisabledTextColor");
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Method to wire the events.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            BaseColumns = _columns;
            BaseHeaderView = HeaderView;
            if (ColumnHeaderView != null)
            {
                SetInheritedBindingContext(ColumnHeaderView, BindingContext);
                ColumnHeaderView.PickerPropertyChanged += OnColumnHeaderPropertyChanged;
                BaseColumnHeaderView = new PickerColumnHeaderView()
                {
                    Background = ColumnHeaderView.Background,
                    DividerColor = ColumnHeaderView.DividerColor,
                    Height = ColumnHeaderView.Height,
                    TextStyle = ColumnHeaderView.TextStyle,
                };
            }
        }

        /// <summary>
        /// Method triggers when the property binding context changed.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (HeaderView != null)
            {
                SetInheritedBindingContext(HeaderView, BindingContext);
                if (HeaderView.TextStyle != null)
                {
                    SetInheritedBindingContext(HeaderView.TextStyle, BindingContext);
                }

                if (HeaderView.SelectionTextStyle != null)
                {
                    SetInheritedBindingContext(HeaderView.SelectionTextStyle, BindingContext);
                }
            }

            if (ColumnHeaderView != null)
            {
                SetInheritedBindingContext(ColumnHeaderView, BindingContext);
                if (ColumnHeaderView.TextStyle != null)
                {
                    SetInheritedBindingContext(ColumnHeaderView.TextStyle, BindingContext);
                }
            }

            if (FooterView != null)
            {
                SetInheritedBindingContext(FooterView, BindingContext);
                if (FooterView.TextStyle != null)
                {
                    SetInheritedBindingContext(FooterView.TextStyle, BindingContext);
                }
            }

            if (SelectedTextStyle != null)
            {
                SetInheritedBindingContext(SelectedTextStyle, BindingContext);
            }

            if (TextStyle != null)
            {
                SetInheritedBindingContext(TextStyle, BindingContext);
            }

            if (SelectionView != null)
            {
                SetInheritedBindingContext(SelectionView, BindingContext);
            }
        }

        /// <summary>
        /// Method triggers when the time picker popup closed.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPopupClosed(EventArgs e)
        {
            InvokeClosedEvent(this, e);
        }

        /// <summary>
        /// Method triggers when the time picker popup closing.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPopupClosing(CancelEventArgs e)
        {
            InvokeClosingEvent(this, e);
        }

        /// <summary>
        /// Method triggers when the time picker popup opened.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPopupOpened(EventArgs e)
        {
            InvokeOpenedEvent(this, e);
        }

        /// <summary>
        /// Method triggers when the time picker ok button clicked.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnOkButtonClicked(EventArgs e)
        {
            InvokeOkButtonClickedEvent(this, e);
            if (AcceptCommand != null && AcceptCommand.CanExecute(e))
            {
                AcceptCommand.Execute(e);
            }
        }

        /// <summary>
        /// Method triggers when the time picker cancel button clicked.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnCancelButtonClicked(EventArgs e)
        {
            InvokeCancelButtonClickedEvent(this, e);
            if (DeclineCommand != null && DeclineCommand.CanExecute(e))
            {
                DeclineCommand.Execute(e);
            }
        }

        #endregion

        #region Property Changed Methods

        /// <summary>
        /// Method invokes on picker header view changed.
        /// </summary>
        /// <param name="bindable">The picker object.</param>
        /// <param name="oldValue">Old value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        static void OnHeaderViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.HeaderView.Parent = picker;
            picker.BaseHeaderView = picker.HeaderView;
            if (bindable is SfTimePicker timePicker)
            {
                timePicker.SetParent(oldValue as Element, newValue as Element);
            }
        }

        /// <summary>
        /// Method invokes on picker column header view changed.
        /// </summary>
        /// <param name="bindable">The picker object.</param>
        /// <param name="oldValue">Old value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        static void OnColumnHeaderViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            if (oldValue is TimePickerColumnHeaderView oldStyle)
            {
                oldStyle.PickerPropertyChanged -= picker.OnColumnHeaderPropertyChanged;
                oldStyle.BindingContext = null;
                oldStyle.Parent = null;
            }

            if (newValue is TimePickerColumnHeaderView newStyle)
            {
                newStyle.Parent = picker;
                SetInheritedBindingContext(newStyle, picker.BindingContext);
                newStyle.PickerPropertyChanged += picker.OnColumnHeaderPropertyChanged;
                picker.BaseColumnHeaderView = new PickerColumnHeaderView()
                {
                    Background = newStyle.Background,
                    DividerColor = newStyle.DividerColor,
                    Height = newStyle.Height,
                    TextStyle = newStyle.TextStyle,
                };

                picker._hourColumn.HeaderText = SfPickerResources.GetLocalizedString(picker.ColumnHeaderView.HourHeaderText);
                picker._minuteColumn.HeaderText = SfPickerResources.GetLocalizedString(picker.ColumnHeaderView.MinuteHeaderText);
                picker._secondColumn.HeaderText = SfPickerResources.GetLocalizedString(picker.ColumnHeaderView.SecondHeaderText);
				picker._millisecondColumn.HeaderText = SfPickerResources.GetLocalizedString(picker.ColumnHeaderView.MillisecondHeaderText);
				picker._meridiemColumn.HeaderText = SfPickerResources.GetLocalizedString(picker.ColumnHeaderView.MeridiemHeaderText);
            }
        }

        /// <summary>
        /// Invokes on selection time property changed.
        /// </summary>
        /// <param name="bindable">The SfTimePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSelectedTimePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            TimeSpan? previousSelectedTime = null;
            TimeSpan? currentSelectedTime = null;
            if (oldValue is TimeSpan oldSelectedTime)
            {
                previousSelectedTime = oldSelectedTime;
                //// Prevents Selection changed event from triggering if old value is black out time.
                if (picker.BlackoutTimes.Any(blackOutTime => TimePickerHelper.IsBlackoutTime(blackOutTime, previousSelectedTime)))
                {
                    PickerContainer? pickerContainer = picker.GetPickerContainerValue();
                    pickerContainer?.UpdateScrollViewDraw();
                    picker.UpdateSelectedIndex();
                    //// Skip the update and event call by checking if the time is blackout value and within current hour.
                    if (oldSelectedTime.Hours == picker._previousSelectedDateTime.Hour)
                    {
                        return;
                    }

                    previousSelectedTime = picker._previousSelectedDateTime.TimeOfDay;
                }

                picker._previousSelectedDateTime = DateTime.Today.Add(oldSelectedTime);
            }

            if (newValue is TimeSpan newSelectedTime)
            {
                //// Prevents Selection changed event from triggering if new value is black out time.
                if (picker.BlackoutTimes.Any(blackOutTime => TimePickerHelper.IsBlackoutTime(blackOutTime, newSelectedTime)))
                {
                    return;
                }

                currentSelectedTime = newSelectedTime;
            }

            //// Skip the update and event call while the same time updated with different time value.
            if (TimePickerHelper.IsSameTimeSpan(previousSelectedTime, currentSelectedTime))
            {
                return;
            }

            if (newValue == null)
            {
                picker._hourColumn.SelectedItem = null;
                picker._minuteColumn.SelectedItem = null;
                picker._secondColumn.SelectedItem = null;
				picker._millisecondColumn.SelectedItem = null;
				picker._meridiemColumn.SelectedItem = null;
                picker.SelectionChanged?.Invoke(picker, new TimePickerSelectionChangedEventArgs() { OldValue = previousSelectedTime, NewValue = currentSelectedTime });
                return;
            }
            else
            {
                picker._hourColumn.SelectedItem = PickerHelper.GetSelectedItemDefaultValue(picker._hourColumn);
                picker._minuteColumn.SelectedItem = PickerHelper.GetSelectedItemDefaultValue(picker._minuteColumn);
                picker._secondColumn.SelectedItem = PickerHelper.GetSelectedItemDefaultValue(picker._secondColumn);
				picker._millisecondColumn.SelectedItem = PickerHelper.GetSelectedItemDefaultValue(picker._millisecondColumn);
				picker._meridiemColumn.SelectedItem = PickerHelper.GetSelectedItemDefaultValue(picker._meridiemColumn);
                PickerContainer? pickerContainer = picker.GetPickerContainerValue();
                pickerContainer?.UpdateScrollViewDraw();
                pickerContainer?.InvalidateDrawable();
            }

            var timePickerSelectionChangedEventArgs = new TimePickerSelectionChangedEventArgs() { OldValue = previousSelectedTime, NewValue = currentSelectedTime };
            if (picker.SelectionChanged != null)
            {
                picker.SelectionChanged?.Invoke(picker, timePickerSelectionChangedEventArgs);
            }

            if (picker.SelectionChangedCommand != null && picker.SelectionChangedCommand.CanExecute(timePickerSelectionChangedEventArgs))
            {
                picker.SelectionChangedCommand.Execute(timePickerSelectionChangedEventArgs);
            }

            picker.UpdateSelectedIndex();
        }

        /// <summary>
        /// Method invokes on format property changed.
        /// </summary>
        /// <param name="bindable">The SfTimePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnFormatPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.UpdateFormat();
        }

        /// <summary>
        /// Method invokes on hour interval property changed.
        /// </summary>
        /// <param name="bindable">The SfTimePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnHourIntervalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null || (int)newValue <= 0)
            {
                return;
            }

            string hourFormat;
            //// Get the format string order and check the index.
            List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out hourFormat, picker.Format);
            int hourIndex = formatStringOrder.IndexOf(0);
            if (hourIndex == -1)
            {
                return;
            }

            picker._hourColumn = picker.GenerateHourColumn(hourFormat, picker.SelectedTime, Convert.ToDateTime(picker.SelectedTime.ToString()));
            //// Replace the hour column with hour interval.
            picker._columns[hourIndex] = picker._hourColumn;
        }

        /// <summary>
        /// Method invokes on minute interval property changed.
        /// </summary>
        /// <param name="bindable">The SfTimePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnMinuteIntervalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null || (int)newValue <= 0)
            {
                return;
            }

            //// Get the format string order and check the index.
            List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out _, picker.Format);
            int minuteIndex = formatStringOrder.IndexOf(1);
            if (minuteIndex == -1)
            {
                return;
            }

            picker._minuteColumn = picker.GenerateMinuteColumn(picker.SelectedTime, Convert.ToDateTime(picker.SelectedTime.ToString()));
            //// Replace the minute column with minute interval.
            picker._columns[minuteIndex] = picker._minuteColumn;
        }

        /// <summary>
        /// Method invokes on second interval property changed.
        /// </summary>
        /// <param name="bindable">The SfTimePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSecondIntervalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null || (int)newValue <= 0)
            {
                return;
            }

            //// Get the format string order and check the index.
            List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out _, picker.Format);
            int secondIndex = formatStringOrder.IndexOf(2);
            if (secondIndex == -1)
            {
                return;
            }

            picker._secondColumn = picker.GenerateSecondColumn(null, selectedDate: Convert.ToDateTime(picker.SelectedTime.ToString()));
            //// Replace the second column with second interval.
            picker._columns[secondIndex] = picker._secondColumn;
        }

		/// <summary>
		/// Method invokes on millisecond interval property changed.
		/// </summary>
		/// <param name="bindable">The SfTimePicker object.</param>
		/// <param name="oldValue">Property old value.</param>
		/// <param name="newValue">Property new value.</param>
		static void OnMillisecondIntervalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			SfTimePicker? picker = bindable as SfTimePicker;
			if (picker == null || (int)newValue <= 0)
			{
				return;
			}

			//// Get the format string order and check the index.
			List<int> formatStringOrder = TimePickerHelper.GetFormatStringOrder(out _, picker.Format);
			int millisecondIndex = formatStringOrder.IndexOf(3);
			if (millisecondIndex == -1)
			{
				return;
			}

			picker._millisecondColumn = picker.GenerateMillisecondColumn(null, selectedDate: Convert.ToDateTime(picker.SelectedTime.ToString()));
			//// Replace the second column with second interval.
			picker._columns[millisecondIndex] = picker._millisecondColumn;
		}

		/// <summary>
		/// Method to get the default selected time span value for SfTimePicker.
		/// </summary>
		/// <returns>Returns the default selected time.</returns>
		static TimeSpan GetDefaultTimeSpan()
        {
            DateTime today = DateTime.Now;
            return new TimeSpan(today.Hour, today.Minute, today.Second, today.Millisecond);
        }

        /// <summary>
        /// Method invokes on minimum time property changed.
        /// </summary>
        /// <param name="bindable">The SfDatePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnMinimumTimePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? timepicker = bindable as SfTimePicker;
            if (timepicker == null)
            {
                return;
            }

            TimeSpan minTime = (TimeSpan)newValue;
            if (minTime.Days != 0)
            {
                timepicker.MinimumTime = (TimeSpan)oldValue;
                return;
            }

            timepicker.UpdateMinimumMaximumTime(oldValue, newValue);
            timepicker.UpdateSelectedIndex();
        }

        /// <summary>
        /// Method invokes on maximum time property changed.
        /// </summary>
        /// <param name="bindable">The SfDatePicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnMaximumTimePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? timepicker = bindable as SfTimePicker;
            if (timepicker == null)
            {
                return;
            }

            TimeSpan maxTime = (TimeSpan)newValue;
            if (maxTime.Days != 0)
            {
                timepicker.MaximumTime = (TimeSpan)oldValue;
                return;
            }

            timepicker.UpdateMinimumMaximumTime(oldValue, newValue);
            timepicker.UpdateSelectedIndex();
        }

        /// <summary>
        /// Method invokes on blackout times property changed.
        /// </summary>
        /// <param name="bindable">The sftimepicker object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnBlackOutTimesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? timepicker = bindable as SfTimePicker;
            if (timepicker == null)
            {
                return;
            }

            //// Unwires collection changed from old and wires on new collection.
            ((ObservableCollection<TimeSpan>)oldValue).CollectionChanged -= timepicker.OnBlackoutTimes_CollectionChanged;
            ((ObservableCollection<TimeSpan>)newValue).CollectionChanged += timepicker.OnBlackoutTimes_CollectionChanged;

            if (timepicker.SelectedTime != null)
            {
                TimeSpan currentTime = timepicker.SelectedTime.Value;
                while (timepicker.BlackoutTimes.Any(blackOutTime => TimePickerHelper.IsBlackoutTime(blackOutTime, currentTime)))
                {
                    currentTime = currentTime.Add(TimeSpan.FromMinutes(1));
                }

                if (timepicker.SelectedTime != currentTime)
                {
                    timepicker.SelectedTime = currentTime;
                }
            }

            //// Gets picker container value to update the view.
            PickerContainer? pickerContainer = timepicker.GetPickerContainerValue();

            pickerContainer?.UpdateScrollViewDraw();
            pickerContainer?.UpdatePickerSelectionView();
        }

        #endregion

        #region Internal Property Changed Methods

        /// <summary>
        /// called when <see cref="TimePickerBackground"/> property changed.
        /// </summary>
        /// <param name="bindable">The bindable object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        static void OnTimePickerBackgroundChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.BackgroundColor = picker.TimePickerBackground;
        }

        /// <summary>
        /// Method invokes on the picker header background changed.
        /// </summary>
        /// <param name="bindable">The header settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnHeaderBackgroundChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.HeaderView.Background = picker.HeaderBackground;
        }

        /// <summary>
        /// Method invokes on the picker footer background changed.
        /// </summary>
        /// <param name="bindable">The footer settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnFooterBackgroundChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.FooterView.Background = picker.FooterBackground;
        }

        /// <summary>
        /// Method invokes on the picker selection background changed.
        /// </summary>
        /// <param name="bindable">The selection settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSelectionBackgroundChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.SelectionView.Background = picker.SelectionBackground;
        }

        /// <summary>
        /// Method invokes on the picker selection stroke color changed.
        /// </summary>
        /// <param name="bindable">The selection settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSelectionStrokeColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.SelectionView.Stroke = picker.SelectionStrokeColor;
        }

        /// <summary>
        /// Method invokes on the picker selection corner radius changed.
        /// </summary>
        /// <param name="bindable">The selection settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSelectionCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.SelectionView.CornerRadius = picker.SelectionCornerRadius;
        }

        /// <summary>
        /// Method invokes on the picker header separator line background changed.
        /// </summary>
        /// <param name="bindable">The header settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnHeaderDividerColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.HeaderView.DividerColor = picker.HeaderDividerColor;
        }

        /// <summary>
        /// Method invokes on the picker footer separator line background changed.
        /// </summary>
        /// <param name="bindable">The footer settings object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnFooterDividerColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.FooterView.DividerColor = picker.FooterDividerColor;
        }

        /// <summary>
        /// Method invokes on the picker header text color changed.
        /// </summary>
        /// <param name="bindable">The header text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnHeaderTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.HeaderView.TextStyle.TextColor = picker.HeaderTextColor;
        }

        /// <summary>
        /// Method invokes on the picker header font size changed.
        /// </summary>
        /// <param name="bindable">The header text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnHeaderFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.HeaderView.TextStyle.FontSize = picker.HeaderFontSize;
        }

        /// <summary>
        /// Method invokes on the picker footer text color changed.
        /// </summary>
        /// <param name="bindable">The footer text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnFooterTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.FooterView.TextStyle.TextColor = picker.FooterTextColor;
        }

        /// <summary>
        /// Method invokes on the picker footer font size changed.
        /// </summary>
        /// <param name="bindable">The footer text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnFooterFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.FooterView.TextStyle.FontSize = picker.FooterFontSize;
        }

        /// <summary>
        /// Method invokes on the picker selection text color changed.
        /// </summary>
        /// <param name="bindable">The selection text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSelectedTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.SelectedTextStyle.TextColor = picker.TextDisplayMode == PickerTextDisplayMode.Default ? picker.SelectedTextColor : picker.SelectionTextColor;
        }

        /// <summary>
        /// Method invokes on the picker selection font size changed.
        /// </summary>
        /// <param name="bindable">The selection text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnSelectedFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.SelectedTextStyle.FontSize = picker.SelectedFontSize;
        }

        /// <summary>
        /// Method invokes on the picker normal text color changed.
        /// </summary>
        /// <param name="bindable">The text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnNormalTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.TextStyle.TextColor = picker.NormalTextColor;
        }

        /// <summary>
        /// Method invokes on the picker normal font size changed.
        /// </summary>
        /// <param name="bindable">The text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnNormalFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.TextStyle.FontSize = picker.NormalFontSize;
        }

        /// <summary>
        /// Method invokes on the picker disabled text color changed.
        /// </summary>
        /// <param name="bindable">The text style object.</param>
        /// <param name="oldValue">Property old value.</param>
        /// <param name="newValue">Property new value.</param>
        static void OnDisabledTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SfTimePicker? picker = bindable as SfTimePicker;
            if (picker == null)
            {
                return;
            }

            picker.DisabledTextStyle.TextColor = picker.DisabledTextColor;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// This method is declared only in IParentThemeElement
        /// and you need to implement this method only in main control.
        /// </summary>
        /// <returns>ResourceDictionary</returns>
        ResourceDictionary IParentThemeElement.GetThemeDictionary()
        {
            return new SfTimePickerStyles();
        }

        /// <summary>
        /// This method will be called when a theme dictionary
        /// that contains the value for your control key is merged in application.
        /// </summary>
        /// <param name="oldTheme">The old value.</param>
        /// <param name="newTheme">The new value.</param>
        void IThemeElement.OnCommonThemeChanged(string oldTheme, string newTheme)
        {
        }

        /// <summary>
        /// This method will be called when users merge a theme dictionary
        /// that contains value for “SyncfusionTheme” dynamic resource key.
        /// </summary>
        /// <param name="oldTheme">Old theme.</param>
        /// <param name="newTheme">New theme.</param>
        void IThemeElement.OnControlThemeChanged(string oldTheme, string newTheme)
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs after the selected time is changed on SfTimePicker.
        /// </summary>
        public event EventHandler<TimePickerSelectionChangedEventArgs>? SelectionChanged;

        #endregion
    }
}