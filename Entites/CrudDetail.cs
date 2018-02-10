using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class CrudDetail:IEntity
    {
        public string Id { get; set; }
        public string CrudId { get; set; }
        public string DetailName { get; set; }
        public List<CrudDetailDetail> crudDetailDetail { get; set; }
    }
}
