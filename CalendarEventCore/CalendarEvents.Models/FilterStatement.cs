using System;

namespace CalendarEvents.Models
{
    /// <summary>
    /// Defines how a property should be filtered.
    /// </summary>
    public class FilterStatement<TEntity> where TEntity : IBaseModel
    {
        /// <summary>
        /// Name of the property.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Express the interaction between the property and the constant value 
        /// defined in this filter statement.
        /// </summary>
        public FilterOperation Operation { get; set; }
        /// <summary>
        /// Constant value that will interact with the property defined in this filter statement.
        /// </summary>
        public object Value { get; set; }
        //TODO: Update the IsValid of Value is truly valid value type of TEntity.
        public bool IsValid
        {
            get
            {
                //Type propertyType = typeof(TEntity).GetProperty(this.PropertyName).PropertyType;

                return typeof(TEntity).GetProperty(PropertyName) != null && 
                    //Value.GetType() == propertyType &&
                    Operation != FilterOperation.Undefined;
            }
        }
        
    }
}
