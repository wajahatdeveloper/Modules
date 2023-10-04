    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class RunFirst : Attribute
    {
        public RunFirst()
        {
            /* noop */
        }
    }