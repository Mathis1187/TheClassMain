namespace TheClassMain
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="NavButton" />
    /// </summary>
    public class NavButton : ListBoxItem
    {
        /// <summary>
        /// Initializes static members of the <see cref="NavButton"/> class.
        /// </summary>
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        /// <summary>
        /// Gets or sets the Navlink
        /// </summary>
        public Uri Navlink
        {
            get { return (Uri)GetValue(NavlinkProperty); }
            set { SetValue(NavlinkProperty, value); }
        }

        /// <summary>
        /// Defines the NavlinkProperty
        /// </summary>
        public static readonly DependencyProperty NavlinkProperty = DependencyProperty.Register("Navlink", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Defines the IconProperty
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(NavButton), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the Label
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Defines the LabelProperty
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(NavButton), new PropertyMetadata(null));
    }
}
