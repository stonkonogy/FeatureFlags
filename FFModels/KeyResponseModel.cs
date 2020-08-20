using System;
using System.Collections.Generic;
using System.Text;

namespace FFModels
{
    public class KeyResponseModel
    {
        public string key { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public string contentType { get; set; }
        public string eTag { get; set; }
        public string lastModified { get; set; }
        public bool locked { get; set; }
    }
}
