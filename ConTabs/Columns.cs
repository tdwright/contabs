using ConTabs.Exceptions;
using System.Collections.Generic;

namespace ConTabs
{
	/// <summary>
	/// The collection of columns within the table
	/// </summary>
	public class Columns : List<Column>
	{
		/// <summary>
		/// Add a range of columns table
		/// </summary>
		/// <param name="columns">A List of columns</param>
		public Columns(List<Column> columns)
		{
			AddRange(columns);
		}

		/// <summary>
		/// Returns the column in the given index
		/// </summary>
		/// <param name="index">The target column's index</param>
		/// <returns>The selected column</returns>
		new public Column this[int index]
		{
			get
			{
				try
				{
					return base[index];
				}
				catch
				{
					throw new ColumnNotFoundException(index);
				}
			}
		}

		/// <summary>
		/// Returns the column with the given name
		/// </summary>
		/// <param name="name">The target column's name</param>
		/// <returns>The selected column</returns>
		public Column this[string name]
		{
			get
			{
				return FindColByName(name);
			}
		}

		/// <summary>
		/// Move a column to a new position
		/// </summary>
		/// <param name="index">The target column's current position</param>
		/// <param name="newPosition">The target position</param>
		public void MoveColumn(int index, int newPosition)
		{
			MoveColumn(this[index], newPosition);
		}

		/// <summary>
		/// Move a column to a new position
		/// </summary>
		/// <param name="name">The target column's name</param>
		/// <param name="newPosition">The target position</param>
		public void MoveColumn(string name, int newPosition)
		{
			MoveColumn(this[name], newPosition);
		}

		private void MoveColumn(Column col, int newPos)
		{
			this.Remove(col);
			this.Insert(newPos, col);
		}

		private Column FindColByName(string name)
		{
			Column backup = null;
			foreach (var col in this)
			{
				if (col.PropertyName == name) return col;
				if (col.ColumnName == name) backup = col;
			}
			if (backup != null) return backup;
			throw new ColumnNotFoundException(name);
		}
	}
}