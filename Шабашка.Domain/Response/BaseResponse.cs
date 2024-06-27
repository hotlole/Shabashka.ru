using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Шабашка.Domain.Enum;

namespace Шабашка.Domain.Response
{
    public class BaseResponse
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public object Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
