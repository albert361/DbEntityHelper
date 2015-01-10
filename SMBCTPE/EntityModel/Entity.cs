using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace DbEntityHelper.EntityModel
{
    /// <summary>
    /// The base type of all entities
    /// <para>
    /// Note that all entity class should inherit this class
    /// </para>
    /// </summary>
    public class Entity : INotifyPropertyChanged
    {
        /// <summary>
        /// Get the Property Names and Values in pair
        /// </summary>
        /// <returns>An enuerator for the pair</returns>
        public virtual IEnumerable<KeyValuePair<String, Object>> GetSqlFieldsAndValuesPair()
        {
            PropertyInfo[] ps = this.GetType().GetProperties();
            
            foreach (PropertyInfo p in ps)
            {
                yield return new KeyValuePair<String, Object>(p.Name, p.GetValue(this, null));
            }
        }

        /// <summary>
        /// PropertyChangedEvent
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        /// <summary>
        /// PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected virtual void OnPropertyChanged(object propertyName)
        {
            if (PropertyChanged != null)
            {
                string pName = propertyName as string;
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
            }
        }
    }
}
