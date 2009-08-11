using System;
using System.Linq.Expressions;

namespace SpecExpress
{
    public class CompiledFunctionExpression<T,TResult>
    {
        private Expression<Func<T,TResult>> _expression;
        Func<T,TResult> _func;

        public CompiledFunctionExpression(Expression<Func<T, TResult>> expression)
        {
            _expression = expression;
            _func = _expression.Compile();
        }

        public TResult Invoke(T parm)
        {
            return _func(parm);
        }
    }
}