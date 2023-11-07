using HtmlAgilityPack;

using LsLocalizeHelperLib.Helper;

using Xunit.Abstractions;

namespace LsHelperUnitTests.Tests;

public class CheckForLsTagErrors
{

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public CheckForLsTagErrors(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldNotValidWrongCloseTag()
  {
    // var source = "Text before LSTag <LSTag Tooltip=\"[111]]\" >LSTag Tooltip Text</LSTag&> Text after LSTag";
    var source = "Text before LSTag <LSTag>LSTag Tooltip Text</LSTag Text waste>Text after LSTag";

    var htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(this.prepareHtml(source));

    htmlDoc.ParseErrors.Should()
           .HaveCount(0);

    var tags = htmlDoc.DocumentNode.SelectNodes("//lstag");

    tags.Count.Should()
        .Be(1);

    foreach (var htmlNode in tags)
    {
      this.testOutputHelper.WriteLine(htmlNode.OuterHtml);

      htmlNode.HasClosingAttributes.Should()
              .BeTrue();
    }
  }

  [Fact]
  public void ShouldValidOneTags()
  {
    // var source = "Text before LSTag <LSTag Tooltip=\"[111]]\" >LSTag Tooltip Text</LSTag&> Text after LSTag";
    var source = "Text before LSTag <LSTag>LSTag Tooltip Text</LSTag> Text after LSTag";

    var htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(this.prepareHtml(source));

    htmlDoc.ParseErrors.Should()
           .HaveCount(0);

    var tags = htmlDoc.DocumentNode.SelectNodes("//lstag");

    tags.Should()
        .NotBeNull();

    tags.Count.Should()
        .Be(1);

    foreach (var htmlNode in tags)
    {
      this.testOutputHelper.WriteLine(htmlNode.OuterHtml);

      htmlNode.HasClosingAttributes.Should()
              .BeFalse();
    }
  }

  [Theory]
  [InlineData( @"<LSTag>LSTag Tooltip Text</LSTag>", true)]
  [InlineData( @"<LSTag>LSTag Tooltip Text</ LSTag>", false)]
  [InlineData( @"<LSTag>LSTag Tooltip
Text</LSTag>", true)]
  [InlineData( @"Text before LSTag <LSTag>LSTag Tooltip Text</LSTag> Text after LSTag", true)]
  [InlineData(@"Text before LSTag <LSTag>LSTag Tooltip Text</LSTag Text waste>Text after LSTag", false)]
  [InlineData(@"Text before LSTag <LSTag>LSTag Tooltip Text 1</LSTag> Text after LSTag 1 <LSTag>LSTag Tooltip Text 2</LSTag> Text after LSTag 2", true)]
  [InlineData( @"LSTag Tooltip Text</LSTag>", false)]
  [InlineData( @"<LSTag tooltip </LSTag> >LSTag Tooltip Text</LSTag>", false)]
  public void ValidateWithHtmlHelper(string html, bool valid)
  {
    html.IsValidHtml()
                    .Should()
                    .Be(valid);
  }

  private string prepareHtml(string text) => "<html>\n<body>\n" + text + "</body>\n</html>";

  #endregion

}
