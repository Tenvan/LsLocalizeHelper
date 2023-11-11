using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace LsLocalizeHelperLib.Helper;

public class SortingHelper
{

  #region Constructors

  public SortingHelper(DataGrid? grid) { this.Grid = grid; }

  #endregion

  #region Properties

  public DataGrid? Grid { get; }

  public List<SortDescription> Sortings { get; } = new();

  #endregion

  #region Methods

  public void ColumnClicked(string column, bool hasControlKey, bool hasShiftKey)
  {
    if (hasControlKey)
    {
      // Just add a new sort description with ascending order
      var newSortDescription
        = this.SwitchSort(new SortDescription(propertyName: column, direction: ListSortDirection.Ascending));

      this.SetSort(newSortDescription: newSortDescription, column: column);
    }
    else
    {
      // Determine the new sort direction 
      // If Shift is pressed, reverse the sort direction, otherwise use ascending
      var newSortDescription
        = this.SwitchSort(new SortDescription(propertyName: column, direction: ListSortDirection.Ascending));

      // Clear the old sort description
      this.Sortings.Clear();
      this.SetSort(newSortDescription: newSortDescription, column: column);
    }
  }

  private void SetSort(SortDescription? newSortDescription, string column)
  {
    var oldDescription = this.Sortings.FirstOrDefault(s => s.PropertyName == column);
    this.Sortings.Remove(oldDescription);

    if (newSortDescription != null)
    {
      this.Sortings.Add(newSortDescription.Value);
    }
  }

  private SortDescription? SwitchSort(SortDescription sortDescription)
  {
    // Fetch the old sort description for the clicked column
    var hasSortDescription = this.Sortings.Any(sd => sd.PropertyName == sortDescription.PropertyName);

    var oldSortDescription = hasSortDescription
                               ? this.Sortings.FirstOrDefault(sd => sd.PropertyName == sortDescription.PropertyName)
                               : (SortDescription?)null;

    SortDescription newSortDirection = new()
    {
      PropertyName = sortDescription.PropertyName,
    };

    if (!oldSortDescription.HasValue)
    {
      newSortDirection.Direction = ListSortDirection.Ascending;
    }
    else if (oldSortDescription.Value.Direction == ListSortDirection.Ascending)
    {
      newSortDirection.Direction = ListSortDirection.Descending;
    }
    else
    {
      return null;
    }

    return newSortDirection;
  }

  public void SyncToGrid()
  {
    // Add the new sort description
    this.Grid.Items.SortDescriptions.Clear();

    foreach (var dataGridColumn in this.Grid.Columns)
    {
      dataGridColumn.SortDirection = null;
    }

    foreach (var sortDescription in this.Sortings)
    {
      this.Grid.Items.SortDescriptions.Add(sortDescription);
      var col = this.Grid.Columns.First(c => c.SortMemberPath == sortDescription.PropertyName);
      col.SortDirection = sortDescription.Direction;
    }

    // Apply the sort descriptions to the view
    CollectionViewSource.GetDefaultView(this.Grid.Items).Refresh();
  }

  #endregion

}
