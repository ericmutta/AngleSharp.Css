﻿namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    public struct BorderImageSlice : IFormattable
    {
        private readonly Length _bottom;
        private readonly Length _left;
        private readonly Length _right;
        private readonly Length _top;
        private readonly Boolean _filled;

        public BorderImageSlice(Length top, Length right, Length bottom, Length left, Boolean filled)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
            _filled = filled;
        }

        public override String ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture);
        }

        public String ToString(String format, IFormatProvider formatProvider)
        {
            var sb = StringBuilderPool.Obtain();
            sb.Append(_top.ToString(format, formatProvider));
            sb.Append(' ');
            sb.Append(_right.ToString(format, formatProvider));
            sb.Append(' ');
            sb.Append(_bottom.ToString(format, formatProvider));
            sb.Append(' ');
            sb.Append(_left.ToString(format, formatProvider));

            if (_filled)
            {
                sb.Append(' ').Append(CssKeywords.Fill);
            }

            return sb.ToPool();
        }
    }
}
