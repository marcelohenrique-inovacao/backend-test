using System.Collections.Generic;

namespace backendtest.Shared.Communication
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Erros = new ResponseErrorMessages();
        }
         
        public int Status { get; set; }
        public ResponseErrorMessages Erros { get; set; }
    }

    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; }
    }
}