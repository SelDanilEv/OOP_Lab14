using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Configuration;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Xml;
using System.Xml.Linq;

namespace Lab14
{
    [DataContract]
    [Serializable]
    public sealed partial class Button : Figure
    {
        public Button(string name, string form, string color, string border, int border_size) : base(name, form, color, border, border_size) { }
        public Button():base() { }
        public void GetMainInfo()
        {
            Console.WriteLine("Название: " + Name + "\nФорма: " + Form);
        }
        public override string ToString()
        {
            return ToConsoleFigure() +'\n';
        }
        public override int GetHashCode()
        {
            int sum = 137+Name.GetHashCode();
            return sum;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            if (obj.GetHashCode() != GetHashCode())
                return false;
            return true;
        }
    }
}
