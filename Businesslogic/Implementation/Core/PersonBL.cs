using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class PersonBL : IPersonBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected readonly IControlBL _controlBL;
        private readonly IDocumentBL _documentBL;
        #region Constructors
        public PersonBL(SqlServerApplicationDbContext context,
        IControlBL controlBL,
        IDocumentBL documentBL
            )
        {
            _context = context;
            _controlBL = controlBL;
            _documentBL = documentBL;
        }
        #endregion

        #region Methods

        #region Read
        public async Task<SaveResult> GenerateEntity(MemberStagingViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            string memberNo = await _controlBL.GetControlByType(ControlEnum.MemberNo.ToString());
            string personGuid = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            viewModel.PersonGuid = personGuid;
            var entity = new Person
            {
                FirstName = viewModel.FirstName,
                Surname = viewModel.Surname,
                ContactNo = viewModel.EmmergencyContactNo1,
                Email = viewModel.Email,
                Initials = viewModel.Initials,
                OtherName = viewModel.OtherName,
                TitleId = viewModel.TitleId,
                BirthDate = viewModel.BirthDate,
                GenderId = viewModel.GenderId,
                IDNumber = viewModel.IDNumber,
                IDTypeId = viewModel.IDTypeId,
                CountryId = viewModel.NationalityId,
                AddressId = viewModel.AddressId,
                CreatedUserId = viewModel.SessionUserId,
                PersonGuid = personGuid

            };

            var memberEntity = new Member
            {
                MemberNo = memberNo,
                Occupation = viewModel.Occupation,
                Company = viewModel.Company,
                WorkTelephone = viewModel.WorkTelephone,
                HomeTelephone = viewModel.HomeTelephone,
                EmmergencyContact1 = viewModel.EmmergencyContact1,
                EmmergencyContactNo1 = viewModel.EmmergencyContactNo1,
                EmmergencyContact2 = viewModel.EmmergencyContact2,
                EmmergencyContactNo2 = viewModel.EmmergencyContactNo2,
                CreatedUserId = viewModel.SessionUserId
            };


           // entity.Employees.Add(employeeEntity);
            entity.Members.Add(memberEntity);

            _context.Person.Add(entity);


            try
            {

                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                    viewModel.PersonId = entity.Id;

                }
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
                string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                saveResult.Message = CrudError.DeleteErrorMsg;
            }


                return saveResult;
            }

        public void UpdateEntity(MemberViewModel viewModel,Member member)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            
                member.Person.FirstName = viewModel.FirstName;
                member.Person.Surname = viewModel.Surname;
                member.Person.ContactNo = viewModel.ContactNo;
                member.Person.Email = viewModel.Email;
                member.Person.Initials = viewModel.Initials;
                member.Person.OtherName = viewModel.OtherName;
                member.Person.TitleId = viewModel.TitleId;
                member.Person.BirthDate = viewModel.BirthDate;
                member.Person.GenderId = viewModel.GenderId;
                member.Person.IDNumber = viewModel.IDNumber;
                member.Person.IDTypeId = viewModel.IDTypeId;
                member.Person.CountryId = viewModel.NationalityId;
                member.Person.UpdatedUserId = viewModel.SessionUserId;

          
           
        }


        public async Task<int> GetMemberIdAsync(int personId)
        {
            Person entity =  await _context.Person.FindAsync(personId);
            return entity.Id;
        }

        public async Task<Person> GetPersonIdAsync(string personGuid)
        {
            Person entity = await _context.Person.Include(a=>a.Members).Include(a => a.Document).Include(a=>a.Users).FirstAsync(a=> a.PersonGuid == personGuid);


            
            if (entity == null)
                throw new ArgumentNullException("Person Code not found");
            return entity; ;
        }


        public async Task<SaveResult> UploadPicture(IFormFile formFile,string personGuid)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {

                Person entity = await _context.Person.Include(a => a.Document).FirstAsync(a => a.PersonGuid == personGuid);
                var docType = await _context.DocumentType.FirstOrDefaultAsync(a => a.Discriminator == DocumentTypeDiscriminator.ProfilePic);

                if (docType == null)
                {
                    throw new ArgumentNullException(nameof(DocumentType));
                }


                if (entity.DocumentId.HasValue)
            {

            }
            else
            {
                entity.Document = new Document {
                    DocumentTypeId = docType.Id
                };
            }

            await _documentBL.UploadDocument(formFile, entity.Document, entity.CreatedUserId);

            _context.Person.Update(entity);
            

     
                await _context.SaveChangesAsync();

                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                }
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();
                string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();
            }
            catch (Exception ex)
            {

                saveResult.Message = CrudError.DeleteErrorMsg;
            }


            return saveResult;
      

        }

        public async Task<SaveResult> DeletePicture(string personGuid)
        {

            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.Person.Include(a => a.Document).FirstOrDefaultAsync(a => a.PersonGuid == personGuid);

                int documentId = entity.DocumentId??0;
                entity.DocumentId = null;
                _context.Person.Update(entity);
                await _context.SaveChangesAsync();


                await _documentBL.DeleteEntity(documentId);
                resultSet.IsSuccess = true;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();

                string msg = results == (int)SqlErrNo.FK ? ConstEntity.ForeignKeyDelMsg : CrudError.DeleteErrorMsg;
                resultSet = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                resultSet.Message = CrudError.DeleteErrorMsg;
            }
            return resultSet;

        }
        #endregion

        #endregion

    }
}