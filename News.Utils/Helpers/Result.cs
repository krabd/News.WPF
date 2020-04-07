using System;

namespace News.Utils.Helpers
{
    public struct Result<TResult, TModel> where TResult : Enum
    {
        public TResult Value { get; }

        public TModel Model { get; }

        public string Message { get; }

        public Result(TResult value, TModel model = default, string message = "")
        {
            Value = value;
            Message = message;
            Model = model;
        }
    }

    public enum Status
    {
        Ok,
        Fail
    }
}
