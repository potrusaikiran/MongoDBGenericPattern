using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class CrudDetailDetailModel
    {
        public string Id { get; set; }
        public string CrudDetailId { get; set; }
        public string Name { get; set; }
        public List<SubDetailModel> subDetail { get; set; }

    }
}
