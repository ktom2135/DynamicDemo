using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObj.Share.Module
{
    public class Obj
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        private List<ObjProperty> _objProperties { get; set; }

        public List<ObjProperty> GetObjProperties()
        {
            return _objProperties;
        }

        public void AddObjProperty(ObjProperty objProperty)
        {
            if (_objProperties == null)
            {
                _objProperties = new List<ObjProperty>();
            }

            _objProperties.Add(objProperty);
        }

        public void SetObjProperties(IEnumerable<ObjProperty> objProperties)
        {
            this._objProperties = objProperties.ToList();
        }

        public Obj()
        {
            this._objProperties = new List<ObjProperty>();
        }

        public Obj(Obj obj)
        {
            this.Id = obj.Id;
            this.Name = obj.Name;

            this._objProperties = obj._objProperties;
        }

        protected void setValie(string key, string value)
        {
            if (_objProperties == null)
            {
                _objProperties = new List<ObjProperty>();
            }

            if (_objProperties == null) _objProperties = new List<ObjProperty>();

            var foundObj = _objProperties.Find(obj => obj.PropertyKey == key);

            if (foundObj == null)
            {
                foundObj = new ObjProperty(key, value);
                _objProperties.Add(foundObj);
            }
            else
            {
                foundObj.PropertyValue = value;
            }
        }

        protected string getValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("property key can not be null or empty");
            }

            string result = null;

            if (_objProperties == null) _objProperties = new List<ObjProperty>();

            var property = _objProperties.Find(obj => obj.PropertyKey == key);

            if (property != null && property.PropertyValue != null)
            {
                result = property.PropertyValue;
            }

            return result;
        }
    }

    public class ObjProperty
    {
        public int Id { get; set; }

        public string PropertyKey { set; get; }

        public string PropertyValue { get; set; }

        public string ObjId { get; set; }

        public ObjProperty() { }

        public ObjProperty(string key, string value)
        {
            PropertyKey = key;
            PropertyValue = value;
        }
    }
}
