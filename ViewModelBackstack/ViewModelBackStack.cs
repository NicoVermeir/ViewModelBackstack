using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ViewModelBackstack
{
    public static class ViewModelBackStack
    {
        private static Dictionary<string, string> _viewModelStack;

        public static void Add(string key, object value)
        {
            if (_viewModelStack == null)
                _viewModelStack = new Dictionary<string, string>();

            _viewModelStack.Add(key, JsonConvert.SerializeObject(value));
        }

        public static object Take<T>(string key)
        {
            string toReturn = _viewModelStack[key];
            Delete(key);

            return JsonConvert.DeserializeObject<T>(toReturn);
        }

        public static bool TryTake<T>(string key, out T value)
            where T : class
        {
            try
            {
                value = JsonConvert.DeserializeObject<T>(_viewModelStack[key]);
                Delete(key);

                return true;
            }
            catch (Exception)
            {
                value = null;
                return false;
            }
        }

        public static bool ContainsKey(string key)
        {
            if (_viewModelStack == null)
                return false;

            return _viewModelStack.ContainsKey(key);
        }

        public static void Delete(string key)
        {
            _viewModelStack.Remove(key);
        }

        public static void Replace(string key, object newValue)
        {
            _viewModelStack[key] = JsonConvert.SerializeObject(newValue);
        }

        public static bool CanGoBack()
        {
            if (_viewModelStack == null)
                return false;

            return _viewModelStack.Count > 0;
        }

        public static T GoBack<T>()
        {
            var toReturn = _viewModelStack.Last();
            _viewModelStack.Remove(toReturn.Key);

            return JsonConvert.DeserializeObject<T>(toReturn.Value);
        }
    }
}
