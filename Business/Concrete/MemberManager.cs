using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MemberManager:IMemberService
    {
        IMemberDal _memberDal;

        public MemberManager(IMemberDal memberDal)
        {
            _memberDal = memberDal;
        }

        public List<OperationClaim> GetClaims(Member member)
        {
            return _memberDal.GetClaims(member);
        }

        public void Add(Member member)
        {
            _memberDal.Add(member);
        }

        public Member GetByMail(string email)
        {
            return _memberDal.Get(m => m.Email == email);
        }
    }
}
