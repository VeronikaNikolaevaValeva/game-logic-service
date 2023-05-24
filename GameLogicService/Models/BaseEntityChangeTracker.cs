using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace GameLogicService.Models
{
    /// <summary>
    /// Base class that will track the original values of a class for later reference
    /// 
    /// Intended use for untracked entity models to allow for smaller and more precise database update requests.
    /// </summary>
    public abstract class BaseEntityChangeTracker 
    {
        [NotMapped]
        private Dictionary<string, object?> originalValues = new Dictionary<string, object?>();

        // Called after creating object and populating properties
        public void OnMaterialized()
        {
            // Set initial values in a dict to compare for changes later
            // Go through all properties excluding properties explicitly set to not be mapped as these can create a cartesian explosion inside the OriginalValues
            foreach (PropertyInfo property in this.GetType().GetProperties().Where(pi => !pi.GetCustomAttributes(typeof(NotMappedAttribute), true).Any()))
            {
                originalValues.Add(property.Name, property.GetValue(this));
            }
        }

        public void AddOriginalValue(string propName, object? value)
        {
            originalValues.Add(propName, value);
        }

        /// <returns>Array containing the names (strings) of all properties changed</returns>
        public string[] GetChangedProperties()
        {
            // Get all properties, excluding properties that are explicitly not mapped
            var properties = this.GetType().GetProperties()
                .Where(pi => !pi.GetCustomAttributes(typeof(NotMappedAttribute), true).Any())
                .Select(p => p.Name).ToArray();

            // if there are no OriginalValues than the model was not materialized from a database or contains no properties, thus all fields should be updated
            if (originalValues.Count < 1)
            {
                return properties;
            }

            // compare current values with intial values and return what properties have changed
            return properties.Where(p => !Equals(this.GetType().GetProperty(p)?.GetValue(this), originalValues[p])).ToArray();
        }
    }
}
