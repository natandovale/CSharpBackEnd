using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroducaoDapper.Model
{
    class Career
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IList<CareerItem> Items { get; set; }

        public Career()
        {
            Items = new List<CareerItem>();
        }
    }
}
