using DataAccess;
using System;

namespace Transform
{
    public static class MemberMappingTransform
    {
        public static MemberMapping ToEntity(this MemberMapping entity, int referralId, int containerId, string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.MemberId = containerId;
                entity.RelationMemberId = referralId;
                entity.CreatedUserId = sessionUserID;
            }
            return entity;
        }
    }
}
