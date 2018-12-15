using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConTabs.Exceptions;
using System.Diagnostics;

namespace ConTabs
{
    [DebuggerDisplay("Table with {Columns.Count} available columns")]
    public sealed partial class Table<T> where T : class
    {
        /// <summary>
        /// The spacing around each element within the cell
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// The collection of columns within the table
        /// </summary>
        public Columns Columns { get; }

        /// <summary>
        /// The horizontal alignment of the column titles
        /// </summary>
        public Alignment HeaderAlignment { get; set; }

        private Alignment _columnAlignment;

        /// <summary>
        /// The horizontal alignment of the cell values
        /// </summary>
        public Alignment ColumnAlignment
        {
            get => _columnAlignment;
            set
            {
                _columnAlignment = value;
                Columns?.ForEach(c => c.Alignment = _columnAlignment);
            }
        }

        private List<Column> ColsShown => Columns.Where(c => !c.Hide).ToList();

        /// <summary>
        /// The table's display properties
        /// </summary>
        public Style TableStyle { get; set; }

        private IEnumerable<T> _data;

        private IEnumerable<T> Data
        {
            get => _data;
            set
            {
                _data = value;
                foreach (var col in Columns)
                {
                    col.Values = _data.ToList().Select(d =>
                    {
                        var t = typeof(T);
                        var p = t.GetRuntimeProperty(col.PropertyName);
                        return p.GetValue(d);
                    }).ToList();
                }
            }
        }

        /// <summary>
        /// Creates an empty table
        /// </summary>
        /// <returns>An empty table</returns>
        public static Table<T> Create()
        {
            return new Table<T>();
        }

        /// <summary>
        /// Creates a new table
        /// </summary>
        /// <param name="source">The collection of objects to place into the table</param>
        /// <returns>A new table</returns>
        public static Table<T> Create(IEnumerable<T> source)
        {
            return new Table<T>()
            {
                Data = source
            };
        }

        /// <summary>
        /// The private driver that creates a new table instance
        /// </summary>
        private Table()
        {
            Padding = new Padding();
            TableStyle = Style.Default;
            HeaderAlignment = Alignment.Default;
            ColumnAlignment = Alignment.Default;

            var props = GetDeclaredAndInheritedProperties(typeof(T).GetTypeInfo());
            var cols = props
                .Where(p => p.GetMethod.IsPublic)
                .Select(p => new Column(p))
                .ToList();

            Columns = new Columns(cols);

            if (!Columns.Any()) throw new PublicPropertiesNotFoundException();
        }

        /// <summary>
        /// Formats the table for output
        /// </summary>
        /// <exception cref="EmptyTableException"></exception>
        /// <returns>The table as a string</returns>
        public override string ToString()
        {
            if (ColsShown != null && ColsShown.Count == 0)
                throw new EmptyTableException(this.GetType());
                
            return OutputBuilder<T>.BuildOutput(this, TableStyle);
        }

        /// <summary>
        /// Gets the current properties of the table
        /// </summary>
        /// <param name="typeInfo">Specific properties to be returned</param>
        /// <returns>The current properties</returns>
        private static IEnumerable<PropertyInfo> GetDeclaredAndInheritedProperties(TypeInfo typeInfo)
        {
            // Loop down the inheritance chain finding all properties
            while (typeInfo != null)
            {
                foreach (var prop in typeInfo.DeclaredProperties)
                {
                    yield return prop;
                }

                typeInfo = typeInfo.BaseType?.GetTypeInfo();
            }
        }
    }
}