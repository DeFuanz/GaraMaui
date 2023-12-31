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
        private readonly Dictionary<string, object> _navigationData = new Dictionary<string, object>();

        public void SetData(string key, object data)
        {
            if (_navigationData.ContainsKey(key))
            {
                _navigationData[key] = data;
            }
            else
            {
                _navigationData.Add(key, data);
            }
        }

        public object GetData(string key)
        {
            if (_navigationData.TryGetValue(key, out var data))
            {
                return data;
            }
            return null;
        }
    }
}
