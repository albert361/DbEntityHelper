using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SMBCTPE.Helper;
using System.ComponentModel;
using System.Reflection;

namespace SMBCTPE.EntityModel
{
    /// <summary>
    /// Entities model for storing data entities
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
    [Serializable()]
    public class Entities<T> : System.Collections.ObjectModel.ObservableCollection<T>, IBindingList, ICancelAddNew
        where T : Entity
    {
        /// <summary>
        /// Constructor of new empty entities object
        /// </summary>
        public Entities() : base()
        {
        }

        internal Entities(IDataReader reader) : base()
        {
            Fill(reader, -1);
        }

        internal Entities(IDataReader reader, int maxRowCount) : base()
        {
            Fill(reader, maxRowCount);
        }

        internal void Fill(IDataReader reader, int maxRowCount)
        {
            base.Clear();

            if (maxRowCount == -1)
            {
                while (reader.Read())
                {
                    base.Add(DBHelper.DataReaderMapping<T>(reader));
                }
            }
            else
            {
                for (int i = 0; i < maxRowCount; i++)
                {
                    if (reader.Read())
                        base.Add(DBHelper.DataReaderMapping<T>(reader));
                    else
                        break;
                }
            }
        }

        private void Entities_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (listChanged != null)
                listChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOf(sender as T)));
        }

        /// <summary>
        /// Gets the entity to data table schema.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The DataTable object</returns>
        public static DataTable GetEntityToDataTableSchema(Type entityType)
        {
            DataTable result = new DataTableWithRowsTag() as DataTable;

            PropertyInfo[] properties = entityType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Type columnType = property.PropertyType;

                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    //columnType = property.PropertyType.GetGenericArguments()[0];
                    columnType = Nullable.GetUnderlyingType(property.PropertyType);
                }

                DataColumn column = new DataColumn(property.Name, columnType);
                result.Columns.Add(column);
            }

            return result;
        }

        /// <summary>
        /// Convert entities to datatable
        /// </summary>
        /// <typeparam name="t">Entity type</typeparam>
        /// <param name="entities">entities</param>
        /// <returns>The DataTable object</returns>
        public static DataTable EntitiesToDataTable<t>(IEnumerable<t> entities)
        {
            DataTableWithRowsTag result = GetEntityToDataTableSchema(typeof(t)) as DataTableWithRowsTag;

            PropertyInfo[] properties = typeof(t).GetProperties();

            foreach (t entity in entities)
            {
                DataRowWithTag dr = result.NewRow() as DataRowWithTag;

                foreach (PropertyInfo property in properties)
                {
                    dr[property.Name] = property.GetValue(entity, null);
                }

                dr.Tag = entity;
                result.Rows.Add(dr);
            }

            return result as DataTable;
        }

        /// <summary>
        /// The indexer of entities
        /// </summary>
        /// <param name="idx">index</param>
        /// <returns>the object T</returns>
        public new T this[int idx]
        {
            get { return base[idx]; }
        }

        /// <summary>
        /// For sorting function
        /// </summary>
        /// <param name="property">sort property</param>
        public void AddIndex(PropertyDescriptor property)
        {
            isSorted = true;
            sortProperty = property;
        }

        /// <summary>
        /// New a object T and add into entities
        /// </summary>
        /// <returns>the object T</returns>
        public object AddNew()
        {
            T t = Activator.CreateInstance<T>();
            this.Add(t);
            return t;
        }

        /// <summary>
        /// AllowEdit
        /// </summary>
        public bool AllowEdit
        {
            get { return true; }
        }

        /// <summary>
        /// AllowNew
        /// </summary>
        public bool AllowNew
        {
            get { return true; }
        }

        /// <summary>
        /// AllowRemove
        /// </summary>
        public bool AllowRemove
        {
            get { return true; }
        }

        /// <summary>
        /// ApplySort
        /// </summary>
        /// <param name="property">sort property</param>
        /// <param name="direction">direction</param>
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {

            isSorted = true;
            sortProperty = property;
            listSortDirection = direction;
            /*
            Array.Sort(this.

            ArrayList a = new ArrayList();*/
            /*
            this.Sort(new ObjectPropertyComparer(property.Name));
            if (direction == ListSortDirection.Descending) this.Reverse();*/
        }

        /// <summary>
        /// Clear all items
        /// </summary>
        protected override void ClearItems()
        {
            foreach (T t in this)
            {
                (t as INotifyPropertyChanged).PropertyChanged -= this.Entities_PropertyChanged;
            }
            base.ClearItems();
            if (listChanged != null)
                listChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        /// <summary>
        /// Find index for an item which has the key equals the given property
        /// </summary>
        /// <param name="property">property</param>
        /// <param name="key">value</param>
        /// <returns>item index</returns>
        public int Find(PropertyDescriptor property, object key)
        {
            foreach (T o in this)
            {
                object v = typeof(T).GetProperty(property.Name).GetValue(o, null);
                if (v.Equals(key))
                    return this.IndexOf(o);
            }
            return -1;
        }

        /// <summary>
        /// Insert item T to index
        /// </summary>
        /// <param name="index">where to insert</param>
        /// <param name="item">item T</param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(Entities_PropertyChanged);
            if (listChanged != null)
                listChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        /// <summary>
        /// Canel the lastest object
        /// </summary>
        /// <param name="itemIndex">item index</param>
        public virtual void CancelNew(int itemIndex)
        {
            this.RemoveAt(itemIndex);
        }

        /// <summary>
        /// EndNew
        /// </summary>
        /// <param name="itemIndex">The end item</param>
        public virtual void EndNew(int itemIndex)
        {
        }

        private bool isSorted = false;

        /// <summary>
        /// IsSorted
        /// </summary>
        public bool IsSorted
        {
            get { return isSorted; }
        }

        private ListChangedEventHandler listChanged;
        /// <summary>
        /// ListChanged event
        /// </summary>
        public event ListChangedEventHandler ListChanged
        {
            add
            {
                listChanged += value;
            }
            remove
            {
                listChanged -= value;
            }
        }

        /// <summary>
        /// OnListChanged event listener
        /// </summary>
        /// <param name="ev">argument</param>
        protected virtual void OnListChanged(ListChangedEventArgs ev)
        {
            if (listChanged != null)
            {
                listChanged(this, ev);
            }
        }

        /// <summary>
        /// clear the sorproperty
        /// </summary>
        /// <param name="property"></param>
        public void RemoveIndex(PropertyDescriptor property)
        {
            sortProperty = null;
        }

        /// <summary>
        /// Remove item of index
        /// </summary>
        /// <param name="index">where to remove</param>
        protected override void RemoveItem(int index)
        {
            (this[index] as INotifyPropertyChanged).PropertyChanged -= this.Entities_PropertyChanged;
            base.RemoveItem(index);
            if (listChanged != null)
                listChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }

        /// <summary>
        /// remove the sorting result
        /// </summary>
        public void RemoveSort()
        {
            isSorted = false;
            sortProperty = null;
        }

        /// <summary>
        /// Set an item T to index
        /// </summary>
        /// <param name="index">where to set</param>
        /// <param name="item">what to set</param>
        protected override void SetItem(int index, T item)
        {
            (this[index] as INotifyPropertyChanged).PropertyChanged -= this.Entities_PropertyChanged;
            base.SetItem(index, item);
            (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(Entities_PropertyChanged);
            if (listChanged != null)
                listChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }
        private ListSortDirection listSortDirection = ListSortDirection.Ascending;
        /// <summary>
        /// SortDirection
        /// </summary>
        public ListSortDirection SortDirection
        {
            get { return listSortDirection; }
        }

        PropertyDescriptor sortProperty = null;
        /// <summary>
        /// SortProperty
        /// </summary>
        public PropertyDescriptor SortProperty
        {
            get { return sortProperty; }
        }

        /// <summary>
        /// SupportsChangeNotification
        /// </summary>
        public bool SupportsChangeNotification
        {
            get { return true; }
        }

        /// <summary>
        /// SupportsSearching
        /// </summary>
        public bool SupportsSearching
        {
            get { return true; }
        }

        /// <summary>
        /// SupportsSorting
        /// </summary>
        public bool SupportsSorting
        {
            get { return false; }
        }
    }
}
