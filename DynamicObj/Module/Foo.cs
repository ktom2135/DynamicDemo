using DynamicObj.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObj.Share.Module
{

    public class Foo : Obj
    {
        public Foo()
        {
        }

        public Foo(Obj obj)
            : base(obj)
        {
        }

        public string Address
        {
            get
            {
                return getValue("Address");
            }
            set
            {
                setValie("Address", value);
            }
        }

        public DateTime Birthday_Title
        {
            get
            {
                var strValue = getValue("Birthday");

                DateTime result = DateTime.FromOADate(Convert.ToDouble(strValue));
                return result;
            }
            set
            {
                string strValue = value.ToOADate().ToString();
                setValie("Birthday", strValue);
            }
        }
    }

}
