using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress.Web
{
    public interface IPageSpecification 
    {
        Type PageSpecification { get; }
    }

    //public interface IPageSpecification2<T> where T : Specification
    //{
    //    T PageSpecification2 { get; } 
    //}

}
