using LsLocalizeHelperLib.Enums;
using LsLocalizeHelperLib.Services;

using Xunit.Abstractions;

namespace LsHelperUnitTests.Tests;

public class DataTests : LsHelperTestsBase
{

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public DataTests(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldLoadData()
  {
    this.LoadTestData();

    LsWorkingDataService.TranslatedItems.Count()
                        .Should()
                        .BeGreaterThan(0);

    LsWorkingDataService.OriginCurrentItems.Count()
                        .Should()
                        .BeGreaterThan(0);

    LsWorkingDataService.OriginPreviousItems.Count()
                        .Should()
                        .BeGreaterThan(0);
  }

  [Fact]
  public void ShouldValidateLoadedData()
  {
    this.LoadTestData();

    var any = LsWorkingDataService.TranslatedItems.Any(t => t.Flag == DatSetFlag.LsTagError);

    any.Should()
       .BeTrue();
  }

  #endregion

}
