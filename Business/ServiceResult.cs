using System.Collections.Generic;

namespace WebApplication_Notes.Business
{
    public class ServiceResult<T>
    {
        public bool HasError { get; set; }
        public bool NotFound { get; set; }
        public List<ErrorItem> Errors { get; set; } = new List<ErrorItem>();
        public T Data { get; set; }

        public void AddError(string key, string errorMessage)
        {
            Errors.Add(new ErrorItem { Key = key, Value = errorMessage });
            HasError = true;
        }
    }

    public class ErrorItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
