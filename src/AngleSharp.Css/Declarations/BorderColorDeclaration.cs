namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using static ValueConverters;

    static class BorderColorDeclaration
    {
        public static String Name = PropertyNames.BorderColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderColorAggregator();

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopColor,
            PropertyNames.BorderRightColor,
            PropertyNames.BorderBottomColor,
            PropertyNames.BorderLeftColor,
        };

        sealed class BorderColorAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(CurrentColorConverter.Periodic(), AssignInitial());

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                if (values.Length > 0)
                {
                    return new CssPeriodicValue(values);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssPeriodicValue periodic)
                {
                    return periodic.ToArray();
                }

                return null;
            }
        }
    }
}
