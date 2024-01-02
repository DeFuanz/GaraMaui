using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    //Store objects that need to be passed between pages
    public class NavigationDataService : INavigationDataService
    {
        private readonly Dictionary<string, object> _navigationData = [];

        public void SetData(string key, object data)
        {
            if (!_navigationData.TryAdd(key, data))
            {
                _navigationData[key] = data;
            }
        }

        public object GetData(string key)
        {
            if (_navigationData.TryGetValue(key, out var data))
            {
                return data;
            }
            return null!;
        }
    }
}
