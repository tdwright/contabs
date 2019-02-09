using ConTabs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if(columns.Any(c=>c.InitialPosition.HasValue))
            {
                TryToSetInitialPositions();
            }
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
            Remove(col);
            Insert(newPos, col);
        }

        private void TryToSetInitialPositions()
        {
            var movesToTry = this
                .Where(c => c.InitialPosition.HasValue)
                .Select(c => new { name = c.PropertyName, pos = c.InitialPosition.Value })
                .OrderBy(a=>a.pos);

            foreach (var moveToTry in movesToTry)
            {
                var pos = (moveToTry.pos >= Count) ? Count - 1 : moveToTry.pos;
                MoveColumn(moveToTry.name, pos);
            }
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
        /// <typeparam name="TInput">Parameter Type</typeparam>
        /// <typeparam name="TOutput">Output Type</typeparam>
        /// <param name="expression">The expression used to compute values</param>
        /// <param name="columnName">The name of the new column</param>
        /// <param name="column">The parameter to operate on</param>
        public void AddGeneratedColumn<TInput, TOutput>(Func<TInput, TOutput> expression, string columnName, Column column)
        {
            if (column.SourceType != typeof(TInput))
                throw new TypeMismatchException(column.ColumnName, typeof(TInput), column.SourceType);

            var results = new List<object>();

            for (var i = 0; i < column.Values.Count; i++)
                results.Add(expression((TInput)column.Values[i]));

            Add(new Column(typeof(TOutput), columnName) { Values = results });
        }

        /// <summary>
        /// Adds a new column to the table of computed values.
        /// </summary>
        /// <typeparam name="TInput">Parameter Type</typeparam>
        /// <typeparam name="TOutput">Output Type</typeparam>
        /// <param name="expression">The expression used to compute values</param>
        /// <param name="columnName">The name of the new column</param>
        /// <param name="column1">The first operand within the given expression</param>
        /// <param name="column2">The second operand within the given expression</param>
        public void AddGeneratedColumn<TInput, TOutput>(Func<TInput, TInput, TOutput> expression, string columnName, Column column1, Column column2)
        {
            AddGeneratedColumn<TInput, TInput, TOutput>(expression, columnName, column1, column2);
        }

        /// <summary>
        /// Adds a new column to the table of computed values.
        /// </summary>
        /// <typeparam name="TInput1">Parameter Type</typeparam>
        /// <typeparam name="TInput2">Parameter Type</typeparam>
        /// <typeparam name="TOutput">Output Type</typeparam>
        /// <param name="expression">The expression used to compute values</param>
        /// <param name="columnName">The name of the new column</param>
        /// <param name="column1">The first operand within the given expression</param>
        /// <param name="column2">The second operand within the given expression</param>
        public void AddGeneratedColumn<TInput1, TInput2, TOutput>(Func<TInput1, TInput2, TOutput> expression, string columnName, Column column1, Column column2)
        {
            if (column1.SourceType != typeof(TInput1))
                throw new TypeMismatchException(column1.ColumnName, typeof(TInput1), column1.SourceType);
            if (column2.SourceType != typeof(TInput2))
                throw new TypeMismatchException(column2.ColumnName, typeof(TInput2), column2.SourceType);

            var results = new List<object>();

            for (var i = 0; i < column1.Values.Count; i++)
                results.Add(expression((TInput1)column1.Values[i], (TInput2)column2.Values[i]));

            Add(new Column(typeof(TOutput), columnName) { Values = results });
        }

        /// <summary>
        /// Adds a new column to the table of computed values.
        /// </summary>
        /// <typeparam name="TOutput">Output Type</typeparam>
        /// <param name="expression">The expression used to compute values, which must accept a list of objects</param>
        /// <param name="columnName">The name of the new column</param>
        /// <param name="columns">A list of the operands to use within the given expression</param>
        public void AddGeneratedColumnFromRange<TOutput>(Func<List<object>, TOutput> expression, string columnName, List<Column> columns)
        {
            var results = new List<object>();

            for (var i = 0; i < columns[0].Values.Count; i++)
            {
                var operands = new List<object>();

                foreach (var col in columns)
                    operands.Add(col.Values[i]);

                results.Add(expression(operands));
            }

            Add(new Column(typeof(TOutput), columnName) { Values = results });
        }
    }
}