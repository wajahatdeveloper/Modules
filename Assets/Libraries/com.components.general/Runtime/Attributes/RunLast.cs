    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RunLast : Attribute
    {
        public RunLast()
        {
            /* noop */
        }
    }