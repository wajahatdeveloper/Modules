    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class RunLast : Attribute
    {
        public RunLast()
        {
            /* noop */
        }
    }