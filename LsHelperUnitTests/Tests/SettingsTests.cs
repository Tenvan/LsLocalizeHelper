using Xunit.Abstractions;

namespace LsHelperUnitTests.Tests;

public class SettingsTests
{

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public SettingsTests(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldLoadSettings()
  {
    
  }

  #endregion

}
