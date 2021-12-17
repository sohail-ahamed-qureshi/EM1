using CommonLayer;
using CommonLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        UserResponse RegisterUser(User RegData);
        ResponseDto LoginUser(LoginReq loginReq);
        List<string> ValidateUserExists(User userData, string operation);
        ResponseDto UpdateUserData(UpdateReq updateReq);
        ResponseDto ActivateDeactivateUser(long userId);
    }
}
