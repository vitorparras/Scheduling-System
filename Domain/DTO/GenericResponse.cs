using System.Text.Json.Serialization;

namespace Domain.DTO
{
    public class GenericResponse<TData>
    {
        public GenericResponse(IEnumerable<Erros>? erros)
        {
            Erros = erros;
            Success = false;
        }
        
        public GenericResponse(TData data)
        {
            Data = data;
            Success = true;
        }
        
        public GenericResponse(string messagemErro, bool success)
        {
            Erros = [new() { Message = messagemErro }];
            Success = success;
        }

        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<Erros>? Erros { get; set; }
    }

    public class Erros
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }
    }
}
