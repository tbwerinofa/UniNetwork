using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject.Component
{
    public class AWSObject
    {
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
        public string BucketName { get; set; }
        public string SessionUserId { get; set; }
    }   
}
