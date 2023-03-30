using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess {
    public class Topic {
        //public int Id { get; set; }
        public EnumTopicType TopicType { get; set; }
    }

   
}
