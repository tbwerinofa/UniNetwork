using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject.Component
{
    public enum SqlErrNo
    {
        UQ = 2627,//Unique Key
        FK = 547,// Foreign Key
        DK = 2601  // Duplicated key row error
    };

    public enum ControlEnum
    {
        MemberNo
    }

}
