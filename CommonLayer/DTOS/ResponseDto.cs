using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DTOS
{
    public class ResponseDto
    {
        public bool Status { get; set; }
        public List<string> Message { get; set; }
        public List<string> Errors { get; set; }
        public dynamic Result { get; set; }

    }
}
