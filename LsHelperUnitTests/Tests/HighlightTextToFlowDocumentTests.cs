using System.Windows.Markup;

using LsLocalizeHelperLib.Helper;

using Xunit.Abstractions;

namespace LsHelperUnitTests;

public class HighlightTextToFlowDocumentTests
{

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public HighlightTextToFlowDocumentTests(ITestOutputHelper testOutputHelper)
  {
    this.testOutputHelper = testOutputHelper;
  }

  #endregion

  #region Methods

  [Fact]
  public void HighlightTextToFlowDocumentTest1()
  {
    var source = "Test Text Simple";
    var target = DocumentHelper.HighlightTextToFlowDocument(text: source, searchReg: null);

    var test = XamlWriter.Save(target);
    this.testOutputHelper.WriteLine(test);

    target.Should()
          .NotBeNull();

    target.Blocks.Count.Should()
          .Be(1);
  }

  [Fact]
  public void HighlightTextToFlowDocumentTest2()
  {
    var source = "Test Text Simple Line1\nLine2";
    var target = DocumentHelper.HighlightTextToFlowDocument(text: source, searchReg: null);

    var test = XamlWriter.Save(target);
    this.testOutputHelper.WriteLine(test);

    target.Should()
          .NotBeNull();

    target.Blocks.Count.Should()
          .Be(1);
  }

  [Fact]
  public void HighlightTextToFlowDocumentTest3()
  {
    var source = "Test Line 1\nLine 2\nLine 3\n";
    var target = DocumentHelper.HighlightTextToFlowDocument(text: source, searchReg: null);

    var test = XamlWriter.Save(target);
    this.testOutputHelper.WriteLine(test);

    target.Should()
          .NotBeNull();

    target.Blocks.Count.Should()
          .Be(1);
  }

  [Fact]
  public void HighlightTextToFlowDocumentTest4()
  {
    var source = "Test Line 1\nLine 2\nLine 3\n";
    var search = "Line";
    var target = DocumentHelper.HighlightTextToFlowDocument(text: source, searchReg: search);

    var test = XamlWriter.Save(target);
    this.testOutputHelper.WriteLine(test);

    target.Should()
          .NotBeNull();

    target.Blocks.Count.Should()
          .Be(1);
  }

  [Fact]
  public void HighlightTextToFlowDocumentTest5()
  {
    const string source = "Debug [555]: Line 1\n    Debug [2]: Line 2\n    Debug [2]: Line 3\n    Debug [2]: Line 4\n    Debug [2]: Line 5";

    var target = DocumentHelper.HighlightTextToFlowDocument(text: source, searchReg: null);

    var test = XamlWriter.Save(target);
    this.testOutputHelper.WriteLine(test);

    target.Should()
          .NotBeNull();

    target.Blocks.Count.Should()
          .Be(1);
  }

  [Fact]
  public void HighlightTextToFlowDocumentTest6()
  {
    const string source
      = "Debug [555]: Line 1\n    Debug [2]: Line 2\n    Debug [2]: Line 3\n    Debug [2]: Line 4\n    Debug [2]: Line 5";

    const string search = "Line";
    var target = DocumentHelper.HighlightTextToFlowDocument(text: source, searchReg: search);

    var test = XamlWriter.Save(target);
    this.testOutputHelper.WriteLine(test);

    target.Should()
          .NotBeNull();

    target.Blocks.Count.Should()
          .Be(1);
  }

  #endregion

}
