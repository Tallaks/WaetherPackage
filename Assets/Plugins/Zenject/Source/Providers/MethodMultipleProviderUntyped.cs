using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
  [NoReflectionBaking]
  public class MethodMultipleProviderUntyped : IProvider
  {
    private readonly DiContainer _container;
    private readonly Func<InjectContext, IEnumerable<object>> _method;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    public MethodMultipleProviderUntyped(
      Func<InjectContext, IEnumerable<object>> method,
      DiContainer container)
    {
      _container = container;
      _method = method;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return context.MemberType;
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsEmpty(args);
      Assert.IsNotNull(context);

      injectAction = null;
      if (_container.IsValidating && !TypeAnalyzer.ShouldAllowDuringValidation(context.MemberType))
      {
        buffer.Add(new ValidationMarker(context.MemberType));
      }
      else
      {
        IEnumerable<object> result = _method(context);

        if (result == null)
          throw Assert.CreateException(
            "Method '{0}' returned null when list was expected. Object graph:\n {1}",
            _method.ToDebugString(), context.GetObjectGraphString());

        foreach (object obj in result)
          buffer.Add(obj);
      }
    }
  }
}