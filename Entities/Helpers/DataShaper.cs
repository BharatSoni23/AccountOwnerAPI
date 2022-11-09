using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helpers
{
    public class DataShaper<T>:IDataShaper<T>
    {
        //One public property in this class - Properties.
        //It's an array of PropertyInfo's that we are going to pull out of the input type.
        public PropertyInfo[] Properties { get; set; }



        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public| BindingFlags.Instance);
        
        }


        //we have the implementation of our two public methods ShapeData:
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);

            //The GetRequiredProperties method is a method that does the magic.
            //It parses the input string and returns just the properties we need to return to the controller:

            return FetchData(entities, requiredProperties);

        }

        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDataForEntity(entity, requiredProperties);
        }
        //ShapeData look similar and reply on the GETREQUIREDPROPERTIES method to parse the inpur string that contains the fileds we want to fetch.
        





        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
        /*
        If the fieldsString is not empty, we split it and check if the fields match the properties in our entity.
        If they do, we add them to the list of required properties.
         On the other hand, if the fieldsString is empty, we consider all properties are required
             */
            var requiredProperties =new List<PropertyInfo>();
            
            if(!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields=fieldsString.Split(',',StringSplitOptions.RemoveEmptyEntries);

                foreach(var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi =>pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                
                    if(property==null)
                        continue;
                    requiredProperties.Add(property);
                }

            }
            else
            {
                requiredProperties=Properties.ToList();
            }
            return requiredProperties;

        }

        /*
         FetchData and FetchDataForEntity are the private methods,
         to extract the values from these required properties we’ve prepared:
         */

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities,IEnumerable<PropertyInfo> requiredProperties)
        {
            /*
             The FetchData method is just an implementation for multiple objects.
            It utilizes the FetchDataForEntity method we’ve just implemented:
             */
            var shapedData =new List<ExpandoObject>(); 

            foreach(var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }

        //FetchDataForEntity does it for a single entity:
        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject=new ExpandoObject();

            foreach(var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name,objectPropertyValue);

            }
            return shapedObject;

        }
    }
}
