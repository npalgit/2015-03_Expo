using System;
using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public abstract class ViewModelContextNodeMiddleTerminal<
        TThis, TThisContext> : ViewModelContextNodeMiddle<
                                   TThis,
                                   TThisContext,
                                   IContextNodeParentTerminal<TThis, TThisContext>,
                                   object,
                                   IContextNodeChildTerminal<TThis, TThisContext>,
                                   object>
        where TThis : class, IContextNodeMiddleTerminal<TThis, TThisContext>
    {
        protected override IEnumerable<object> GetChildContexts(TThisContext thisContext)
        {
            yield break;
        }
    }
}