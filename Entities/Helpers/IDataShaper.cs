using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helpers
{
    public interface IDataShaper<T>
    {
        //The IDataShaper defines two methods that should be implemented,
        //one for the single entity,
        //and one for the collection of entities.
        //Both are named ShapeData but they have different signatures.
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
        ExpandoObject ShapeData(T entity, string fieldsString);
    }

}
