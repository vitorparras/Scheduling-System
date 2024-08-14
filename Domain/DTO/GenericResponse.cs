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
        public TData? Data { get; set; }
        public IEnumerable<Erros>? Erros { get; set; }
    }

    public class Erros
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
