using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.Component
{
    public class SaveResult
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string DateValue { get; set; }
        public bool IsSuccess2 { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }

        public string Discriminator { get; set; }
    }
}
