using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class MemberMappingBL : IMemberMappingBL
    {
        #region global fields

        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IPersonBL _personBL;
        #endregion

        #region Constructors
        public MemberMappingBL(SqlServerApplicationDbContext context,
            IPersonBL personBL)
        {
            _context = context;
            _personBL = personBL;
        }
        #endregion

        #region Methods

        #region Read

        public async Task<SaveResult> SaveEntityList(MemberViewModel model, Member parentEntity)
        {
            RegisterViewModel registerModel = new RegisterViewModel {
                MemberIds = model.MemberIds,
                SessionUserId = model.SessionUserId,
                Id = parentEntity.Id
            };

            return await SaveEntityList(registerModel, parentEntity);

 
        }

        public async Task<SaveResult> SaveEntityList(RegisterViewModel model, Member parentEntity)
        {
            SaveResult saveResult = new SaveResult();

            var currentEntities = parentEntity.MemberMappings;
            model.Id = parentEntity.Id;
            if (model.MemberIds == null)
            {

                if (currentEntities.Any())
                {
                    var relationMemberIds = currentEntities.ToList().ToList().Select(b => b.Id);

                    var toDeleteList = _context.MemberMapping.Where(a => relationMemberIds.Contains(a.Id));

                    _context.MemberMapping.RemoveRange(toDeleteList);
                    await _context.SaveChangesAsync();
                    saveResult.IsSuccess = true;
                }
                else
                {
                    saveResult.IsSuccess = true;
                }
            }
            else
            {

                var members = _context.Member.Where(a => model.MemberIds.Contains(a.Id));

                if (currentEntities.Any())
                {

                    saveResult.IsSuccess = await AddMemberMappingWherePreviousExists(model, currentEntities, members);

                    if (saveResult.IsSuccess)
                    {
                        saveResult = await ManageExistingMemberMapping(model, currentEntities, members);
                    }
                }
                else
                {

                    saveResult.IsSuccess = await AddEntity(model, currentEntities, members);
                }
            }


            return saveResult;
        }

        private async Task<SaveResult> ManageExistingMemberMapping(RegisterViewModel model,
         ICollection<MemberMapping> currentEntityList,
         IQueryable<Member> roleAppPage)
        {
            SaveResult saveResult = new SaveResult
            {
                IsSuccess = true
            };

            List<MemberMapping> assignedMemberMappings = new List<MemberMapping>();

            foreach (var record in currentEntityList.ToList())
            {
                if (saveResult.IsSuccess)
                {
                    if (!model.MemberIds.Any(a => a == record.RelationMemberId))
                    {
                        var deleteMemberMapping = await _context.MemberMapping.FindAsync(record.Id);

                        if (saveResult.IsSuccess)
                        {
                            _context.Remove(deleteMemberMapping);
                            await _context.SaveChangesAsync();
                            saveResult.IsSuccess = true;
                        }
                    }
                    else
                    {
                        saveResult = await EditEntityAsync(model, record);
                    }
                }
            }
            return saveResult;
        }

        private async Task<bool> AddMemberMappingWherePreviousExists(RegisterViewModel model,
            ICollection<MemberMapping> MemberMappings,
            IQueryable<Member> members)
        {
            bool isSaveSuccess = true;
            List<MemberMapping> assignedMemberMappings = new List<MemberMapping>();
            foreach (var recordId in model.MemberIds)
            {

                var currentRolePermission = members.Where(a => a.Id == recordId).FirstOrDefault();
                if (currentRolePermission != null)
                {

                    if (!MemberMappings.Any(a => a.RelationMemberId == recordId))
                    {
                        var MemberMappingRepo = new MemberMapping();
                        assignedMemberMappings.Add(MemberMappingRepo.ToEntity(recordId, model.Id, model.SessionUserId));
                    }
                }


            }

            if (assignedMemberMappings.Count > 0)
            {
                await _context.MemberMapping.AddRangeAsync(assignedMemberMappings);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }


        private async Task<SaveResult> EditEntityAsync(RegisterViewModel model, MemberMapping record)
        {
            var saveResult = new SaveResult();
            var editMemberMapping = await _context.MemberMapping.FindAsync(record.Id);
            editMemberMapping.ToEntity(record.RelationMemberId, model.Id, model.SessionUserId);

            _context.Update(editMemberMapping);
            await _context.SaveChangesAsync();

            saveResult.IsSuccess = true;
            return saveResult;

        }

        private async Task<bool> AddEntity(RegisterViewModel model,
               ICollection<MemberMapping> MemberMappings,
               IQueryable<Member> parentEntityList)
        {

            bool isSaveSuccess = true;
            List<MemberMapping> MemberMappingList = new List<MemberMapping>();
            foreach (var record in model.MemberIds)
            {
                var currentParentEntity = parentEntityList.Any(a => a.Id == record);
                if (currentParentEntity)
                {
                    MemberMapping referralEntity = new MemberMapping();
                    MemberMappingList.Add(referralEntity.ToEntity(record, model.Id, model.SessionUserId));
                }
            }

            if (MemberMappingList.Count > 0)
            {
                await _context.MemberMapping.AddRangeAsync(MemberMappingList);
                await _context.SaveChangesAsync();
                isSaveSuccess = true;
            }

            return isSaveSuccess;
        }
        #endregion

        #endregion

    }
}