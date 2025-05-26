using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Stats
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection;
        private PropertyDescriptor _sortProperty;

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _isSorted;
        protected override ListSortDirection SortDirectionCore => _sortDirection;
        protected override PropertyDescriptor SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var items = this.Items as List<T>;
            if (items == null) return;

            var comparer = new PropertyComparer<T>(prop.Name, direction);
            items.Sort(comparer);

            _sortDirection = direction;
            _sortProperty = prop;
            _isSorted = true;

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        public SortableBindingList(IEnumerable<T> collection) : base(collection.ToList()) { }
        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }
    }

    // Clase comparadora necesaria
    public class PropertyComparer<T> : IComparer<T>
    {
        private readonly PropertyDescriptor _property;
        private readonly ListSortDirection _direction;

        public PropertyComparer(string propertyName, ListSortDirection direction)
        {
            _property = TypeDescriptor.GetProperties(typeof(T)).Find(propertyName, false);
            _direction = direction;
        }

        public int Compare(T x, T y)
        {
            var valueX = _property.GetValue(x);
            var valueY = _property.GetValue(y);

            if (valueX == null && valueY == null) return 0;
            if (valueX == null) return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (valueY == null) return _direction == ListSortDirection.Ascending ? 1 : -1;

            var comparable = valueX as IComparable;
            if (comparable != null)
            {
                return _direction == ListSortDirection.Ascending
                    ? comparable.CompareTo(valueY)
                    : comparable.CompareTo(valueY) * -1;
            }

            return 0;
        }
    }
}
