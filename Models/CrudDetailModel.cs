using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class CrudDetailModel
    {
        public string Id { get; set; }
        public string CrudId { get; set; }
        public string DetailName { get; set; }
        public List<CrudDetailDetailModel> crudDetailDetail { get; set; }

    }
}
