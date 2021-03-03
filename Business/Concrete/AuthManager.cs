using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IMemberService _memberService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IMemberService memberService, ITokenHelper tokenHelper)
        {
            _memberService = memberService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<Member> Register(MemberForRegisterDto memberForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var member = new Member
            {
                Email = memberForRegisterDto.Email,
                FirstName = memberForRegisterDto.FirstName,
                LastName = memberForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _memberService.Add(member);
            return new SuccessDataResult<Member>(member, Messages.MemberRegistered);
        }

        public IDataResult<Member> Login(MemberForLoginDto memberForLoginDto)
        {
            var memberToCheck = _memberService.GetByMail(memberForLoginDto.Email);
            if (memberToCheck == null)
            {
                return new ErrorDataResult<Member>(Messages.MemberNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(memberForLoginDto.Password, memberToCheck.PasswordHash, memberToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Member>(Messages.PasswordError);
            }

            return new SuccessDataResult<Member>(memberToCheck, Messages.SuccessfulLogin);
        }

        public IResult MemberExists(string email)
        {
            if (_memberService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.MemberAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(Member Member)
        {
            var claims = _memberService.GetClaims(Member);
            var accessToken = _tokenHelper.CreateToken(Member, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}