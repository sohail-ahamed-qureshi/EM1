using CommonLayer;
using CommonLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        UserResponse RegisterUser(UserReq RegData);
        ResponseDto Login(LoginReq loginReq);
        ResponseDto UpdateUser(UpdateReq updateReq);
        ResponseDto DeleteUser();
        ResponseDto ActivateDeactivateUser(long userId);
    }
}
