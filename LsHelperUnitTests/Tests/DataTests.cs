using LsHelperUnitTests.Classes;

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

  public DataTests(ITestOutputHelper testOutputHelper)
  {
    this.testOutputHelper = testOutputHelper;
    LsWorkingDataService.Clear();
  }

  #endregion

  #region Methods

  [Fact]
  [Repeat(5)]
  public void ShouldLoadData()
  {
    this.LoadTestData();

    LsWorkingDataService.TranslateItems.Count()
                        .Should()
                        .Be(15);

    LsWorkingDataService.OriginCurrentItems.Count()
                        .Should()
                        .Be(14);

    LsWorkingDataService.OriginPreviousItems.Count()
                        .Should()
                        .Be(14);
  }

  [Fact]
  [Repeat(5)]
  public void ShouldValidateLoadedData()
  {
    this.LoadTestData();

    var any = LsWorkingDataService.TranslateItems.Any(t => t.Flag == DatSetFlag.LsTagError);

    any.Should()
       .BeTrue();
  }

  #endregion

}
