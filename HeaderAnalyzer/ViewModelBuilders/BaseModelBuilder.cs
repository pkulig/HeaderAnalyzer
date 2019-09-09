using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeaderAnalyzer.ViewModelBuilders
{
    public abstract class BaseModelBuilder<T> 
        where T : class
    {
        public abstract Task<T> Build();
    }
}
