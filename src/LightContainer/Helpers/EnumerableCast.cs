using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightContainer.Helpers
{
    /// <summary>
    /// Functions for casting Enumerables for refelection.
    /// </summary>
    static class EnumerableCast
    {
        #region Private Static Fields

        private static readonly MethodInfo _castMethod = typeof(Enumerable)
            .GetTypeInfo()
            .GetMethod("Cast", BindingFlags.Static | BindingFlags.Public);

        #endregion

        #region Functions

        /// <summary>
        /// Casts a IEnumerable collection of objects into generic  IEnumerable<T/> for a specific type.
        /// </summary>
        /// <param name="type">Type for IEnumerable<T/></param>
        /// <param name="objects">IEnumerable of objects.</param>
        /// <returns>Object that is internally a IEnumerable<T/></returns>
        public static object Cast(Type type, IEnumerable<object> objects)
        {
            var genericCastMethod = _castMethod.MakeGenericMethod(type);
            return genericCastMethod.Invoke(null, new object[] { objects });
        }

        #endregion
    }
}
