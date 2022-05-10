/* Copyright Kupio Limited. Registered in Scotland; SC426881.
 * All rights reserved. Not for distribution. */

namespace com.kupio.declarativeorder
{
    using System;
    using System.Text;

    [AttributeUsage(AttributeTargets.Class)]
    public class RunAfter : Attribute
    {
        private Type[] _deps; /* dependencies */

        public Type[] All
        {
            get
            {
                return _deps;
            }
        }

        public RunAfter(params Type[] deps)
        {
            this._deps = deps;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < _deps.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append(_deps[i].FullName);
            }
            return sb.Append("]").ToString();
        }

    }
}
