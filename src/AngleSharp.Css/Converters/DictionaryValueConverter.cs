﻿namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class DictionaryValueConverter<T> : IValueConverter
    {
        private readonly IDictionary<String, T> _values;

        public DictionaryValueConverter(IDictionary<String, T> values)
        {
            _values = values;
        }

        public ICssValue Convert(StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();
            var mode = default(T);

            if (ident != null && _values.TryGetValue(ident, out mode))
            {
                return new EnumeratedValue(ident.ToLowerInvariant(), mode);
            }

            source.BackTo(pos);
            return null;
        }

        private sealed class EnumeratedValue : ICssValue
        {
            private readonly String _key;
            private readonly T _data;

            public EnumeratedValue(String key, T data)
            {
                _key = key;
                _data = data;
            }

            public String CssText
            {
                get { return _key; }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
