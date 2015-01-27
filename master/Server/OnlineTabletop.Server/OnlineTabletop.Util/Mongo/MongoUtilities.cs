using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Util.Mongo
{
    public static class MongoUtilities
    {
        public static string GetCollectionFromType(Type type)
        {
            if (type != null)
            {
                return type.Name.ToLowerInvariant();
            }
            return String.Empty;
        }
    }
}
