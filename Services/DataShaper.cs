using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] properties { get; set; }
        public DataShaper() 
        {
            // Çekilen propertylerin public ve yenilenebilir olması
            properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        }
        private IEnumerable <PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldsString)) {
                //removeEmptyEntries boş olanları fields nesnesinden kaldırıyor
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries); 
                foreach ( var field in fields)
                {//Nesnenin adına eşit olanı yakala, Trim boşluk kontrolü, StringCmp büyük küçük fark etmeksizin
                    var property = properties.FirstOrDefault(x => x.Name.Equals(field.Trim(), 
                        StringComparison.InvariantCultureIgnoreCase));
                    if (property is null) continue; //nullsa foreach'a devam et, diğer propu oku
                    requiredFields.Add(property); //null değilse gelen parametreyi ekle

                }
            } else requiredFields = properties.ToList();
            
            return requiredFields;
        }

        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapeObject = new ExpandoObject();
            foreach( var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapeObject.TryAdd(property.Name, objectPropertyValue);
            }
            return shapeObject;
        }

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapeData = new List<ExpandoObject>();
            foreach( var entity in entities)
            {
                var shapeObject = FetchDataForEntity(entity, requiredProperties);
                shapeData.Add(shapeObject);
            }
            return shapeData;
        }
        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDataForEntity(entity, requiredProperties);
        }

        public IEnumerable<ExpandoObject> ShapeDataList(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchData(entities, requiredProperties);
        }
    }
}
