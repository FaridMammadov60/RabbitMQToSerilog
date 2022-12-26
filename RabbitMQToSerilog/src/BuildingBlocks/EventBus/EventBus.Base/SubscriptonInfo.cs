using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Base
{
    public class SubscriptonInfo
    {
        public Type HandlerType { get; set; }

        public SubscriptonInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptonInfo Typed(Type handlerType)
        {
            return new SubscriptonInfo(handlerType);
        }
    }
}
