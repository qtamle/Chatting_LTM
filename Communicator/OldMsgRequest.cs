using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator
{
    public class OldMsgRequest
    {
        public string sender {  get; set; }
        public string receiver { get; set; }
        public bool is_group_msg { get; set; }
    }
}
