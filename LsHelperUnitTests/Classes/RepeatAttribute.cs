using System.Reflection;

using Xunit.Sdk;

namespace LsHelperUnitTests.Classes;

public class RepeatAttribute : DataAttribute
{

  private readonly int _count;

  public RepeatAttribute(int count)
  {
    if (count < 1) { throw new ArgumentOutOfRangeException(paramName: nameof(count), message: "Repeat count must be greater than 0."); }

    this._count = count;
  }

  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return Enumerable.Repeat(element: new object[0], count: this._count);
  }

}
