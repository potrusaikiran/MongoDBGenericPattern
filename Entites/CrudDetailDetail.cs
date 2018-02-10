using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class CrudDetailDetail:IEntity
    {
        public string Id { get; set; }
        public string CrudDetailId { get; set; }
        public string Name { get; set; }
        public List<SubDetail> subDetail { get; set; }
    }
}
