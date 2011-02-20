using System;
using System.Web.UI.WebControls;

namespace Evil.Web.Binders
{
    /// <summary>
    /// Sets the default sort field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultSortAttribute : Attribute
    {
        public SortDirection SortDirection { get; private set; }

        public DefaultSortAttribute(SortDirection sortDirection)
        {
            SortDirection = sortDirection;
        }
    }
}