using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel.Interfaces
{
    interface IModelLoader
    {
        DocumentCollection Load(string path);
  
    }
}
