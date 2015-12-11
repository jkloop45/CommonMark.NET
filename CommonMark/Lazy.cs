﻿namespace CommonMark
{
#if v2_0 || v3_5
    class Lazy<T>
    {
        private readonly Func<T> valueFactory;
        private readonly bool isThreadSafe;
        private readonly object _lock = new object();
        private T value;

        public Lazy(Func<T> valueFactory)
            : this(valueFactory, true)
        {
        }

        public Lazy(Func<T> valueFactory, bool isThreadSafe)
        {
            this.valueFactory = valueFactory;
            this.isThreadSafe = isThreadSafe;
        }

        public T Value
        {
            get
            {
                if (value == null)
                {
                    if (!isThreadSafe)
                    {
                        return value = valueFactory();
                    }
                    lock (_lock)
                    {
                        if (value == null)
                        {
                            value = valueFactory();
                        }
                    }
                }
                return value;
            }
        }

        public bool IsValueCreated
        {
            get { return value != null; }
        }
    }
#endif
}
