using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konkeror.app.Services
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }
        public List<ValidationMessage> ValidationMessages { get; set; }
    }
}
