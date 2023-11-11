using System.ComponentModel;

using LsLocalizeHelperLib.Helper;

namespace LsHelperUnitTests.Tests;

public class SortingHelperTests
{

  #region Methods

  [Fact]
  public void Clicks1Cols1WithControlShouldSortAsc()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
  }

  [Fact]
  public void Clicks1Cols1WithoutAnd1WithControlShouldSortAscTwo()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(2);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
    fixure.Sortings[1].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[1].PropertyName.Should().Be("Feld2");
  }

  [Fact]
  public void Clicks1Cols1WithoutAnd2WithControlShouldSortAscDescTwo()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(2);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
    fixure.Sortings[1].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[1].PropertyName.Should().Be("Feld2");
  }

  [Fact]
  public void Clicks1Cols1WithoutAnd3WithControlShouldSortAscFirst()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
  }

  [Fact]
  public void Clicks1Cols1WithoutControlShouldSortAsc()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
  }

  [Fact]
  public void Clicks2Cols1WithoutControlShouldSortDesc()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
  }

  [Fact]
  public void Clicks2Cols2WithControlAnd1WithoutControlShouldSortAscLast()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld3", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld3");
  }

  [Fact]
  public void Clicks2Cols2WithoutControlShouldSortAscLast()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld2");
  }

  [Fact]
  public void Clicks3Cols1SortingShouldSortAbort()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(0);
  }

  [Fact]
  public void Clicks3Cols3WithControlShouldSortAscAll()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld3", hasControlKey: true, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(3);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
    fixure.Sortings[1].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[1].PropertyName.Should().Be("Feld2");
    fixure.Sortings[2].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[2].PropertyName.Should().Be("Feld3");
  }

  [Fact]
  public void Clicks3Cols3WithoutControlShouldSortAscLast()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld3", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld3");
  }

  [Fact]
  public void Clicks4Cols2WithControlShouldSortDescTwo()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld1", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(2);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
    fixure.Sortings[1].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[1].PropertyName.Should().Be("Feld2");
  }

  [Fact]
  public void Clicks4Cols2WithoutControlShouldAscLast()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld1", hasControlKey: false, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: false, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(1);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Ascending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld2");
  }

  [Fact]
  public void Clicks6Cols3WithControlShouldSortDescAll()
  {
    var fixure = new SortingHelper(null);

    fixure.ColumnClicked(column: "Feld1", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld3", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld1", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld2", hasControlKey: true, hasShiftKey: false);
    fixure.ColumnClicked(column: "Feld3", hasControlKey: true, hasShiftKey: false);

    fixure.Sortings.Count.Should().Be(3);

    fixure.Sortings[0].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[0].PropertyName.Should().Be("Feld1");
    fixure.Sortings[1].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[1].PropertyName.Should().Be("Feld2");
    fixure.Sortings[2].Direction.Should().Be(ListSortDirection.Descending);
    fixure.Sortings[2].PropertyName.Should().Be("Feld3");
  }

  #endregion

}
