using ConTabs.Exceptions;
using System;
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

		/// <summary>
		/// Adds a new column to the table of computed values.
		/// </summary>
		/// <typeparam name="T">Parameter Type</typeparam>
		/// <typeparam name="F">Output Type</typeparam>
		/// <param name="expression">The expression used to compute values</param>
		/// <param name="name">The name of the new column</param>
		/// <param name="parameter">The parameter to operate on</param>
		public void AddGeneratedColumn<T, F>(Func<T, F> expression, string columnName, Column parameter)
		{
			var vals = new List<object>();

			for (int i = 0; i < parameter.Values.Count; i++)
				vals.Add(expression((T)parameter.Values[i]));

			this.Add(new Column(typeof(F), columnName) { Values = vals });
		}

		/// <summary>
		/// Adds a new column to the table of computed values.
		/// </summary>
		/// <typeparam name="T">Parameter Type</typeparam>
		/// <typeparam name="F">Output Type</typeparam>
		/// <param name="expression">The expression used to compute values</param>
		/// <param name="name">The name of the new column</param>
		/// <param name="firstOperand">The first operand within the given expression</param>
		/// <param name="secondOperand">The second operand within the given expression</param>
		public void AddGeneratedColumn<T, F>(Func<T, T, F> expression, string columnName, Column firstOperand, Column secondOperand)
		{
			var vals = new List<object>();

			for (int i = 0; i < firstOperand.Values.Count; i++)
				vals.Add(expression((T)firstOperand.Values[i], (T)secondOperand.Values[i]));

			this.Add(new Column(typeof(F), columnName) { Values = vals });
		}
	}
}