using BusinessLogic.Interface;
using System.Collections.Generic;
using System.Linq; 

namespace BusinessLogic.Implementation
{
    public class MessageTokenBL : IMessageTokenBL
    {
        #region Global Fields


        #endregion

        #region Constructors
        public MessageTokenBL()
        {

        }

        #endregion

        #region Methods

        #region Read
        public virtual string[] GetListOfAllowedTokens()
        {


            var allowedTokens = new List<string>
            {
                 "%Recipient%",
                 "%MessageBody%",
                 "%Url%"
            };
            allowedTokens.AddRange(allowedTokens);

            return allowedTokens.Distinct().ToArray();
        }

        #endregion

        #endregion


    }
}
