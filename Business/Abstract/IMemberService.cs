using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMemberService
    {
        List<OperationClaim> GetClaims(Member member);
        void Add(Member member);
        Member GetByMail(string email);
    }
}
