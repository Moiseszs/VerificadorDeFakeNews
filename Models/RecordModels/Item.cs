using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RecordModels
{
    public record class Item(
        string title = null,
        string htmlTitle = null,
        string link = null,
        string displayLink = null,
        string snippet = null,
        string formattedUrl = null
        );
}
