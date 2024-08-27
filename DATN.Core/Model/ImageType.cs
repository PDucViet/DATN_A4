using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class ImageType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Image> Images{ get; set; }  

    }
}
